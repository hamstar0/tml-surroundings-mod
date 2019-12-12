using System;
using System.Collections.Generic;
using System.Linq;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using HamstarHelpers.Services.AnimatedTexture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components.Fireflies;
using Terraria;
using Terraria.ID;


namespace Surroundings.Scenes.Contexts.SurfaceForest {
	public partial class SurfaceForestNightScene : Scene {
		private IList<Firefly> Flies = new List<Firefly>();

		private bool FliesInitialized = false;


		////////////////

		public override SceneContext Context { get; }


		////////////////

		public override int DrawPriority { get; } = 2;

		////

		public override Vector2 FrameSize => new Vector2( Main.screenWidth, Main.screenHeight );

		public override float HorizontalTileScrollRate { get; } = 1f;

		public override float VerticalTileScrollRate { get; } = 0f;



		////////////////

		public SurfaceForestNightScene() {
			this.Context = new SceneContext(
				layer: SceneLayer.Near,
				isDay: false,
				anyOfBiome: new VanillaBiome[] { VanillaBiome.Forest },
				currentEvent: null,
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.Overworld },
				customCondition: null
			);
			this.Context.Lock();

			if( Main.rand != null && Main.npcTexture[NPCID.Firefly] != null ) {
				this.InitializeFireflies();
			}
		}

		private void InitializeFireflies() {
			this.FliesInitialized = true;

			for( int i = 0; i < 7; i++ ) {
				this.Flies.Add( new Firefly(
					animation: AnimatedTexture.Create( Main.npcTexture[NPCID.Firefly], 4, this.MyAnimator ),
					screenPosition: new Vector2( Main.rand.Next( 0, Main.screenWidth ), Main.rand.Next( 0, Main.screenHeight ) ),
					velocity: new Vector2( Main.rand.NextFloat() - 0.5f, Main.rand.NextFloat() - 0.5f ),
					acceleration: 0
				) );
			}
		}


		private (int NextFrame, int Duration) MyAnimator( AnimatedTexture anim ) {
			if( anim.CurrentFrame >= ( anim.MaxFrames - 1 ) ) {
				if( Main.rand.NextFloat() >= 0.9f ) {
					return (0, 8);
				} else {
					return (2, 8);
				}
			}
			return (anim.CurrentFrame + 1, 8);
		}


		////////////////

		public override Color GetSceneColor( SceneDrawData drawData ) {
			//float shadeScale = ( this.IsNear ? 192f : 255f ) * brightness;
			float shadeScale = 192f * drawData.Brightness;
			byte shade = (byte)Math.Min( shadeScale, 255 );

			return new Color( shade, shade, shade, 96 );
		}

		public override float GetSceneOpacity( SceneDrawData drawData ) {
			float occludedPercent = drawData.WallPercent + ( drawData.CavePercent - drawData.CaveAndWallPercent );
			float relevantOcclusionPercent = Math.Max( occludedPercent - 0.6f, 0f ) * 2.5f;
			float relevantNonOcclusionPercent = 1f - relevantOcclusionPercent;
			return relevantNonOcclusionPercent * this.GetTownProximity() * drawData.Opacity;
		}


		private float GetTownProximity() {
			float nearest = Main.maxTilesX * 16f;
			float nearestSqr = nearest * nearest;
			NPC nearestNpc = null;

			Main.npc.Any( ( n ) => {
				if( n?.active != true ) {
					return false;
				}

				if( n.boss ) {
					nearestNpc = null;
					return true;
				}
				
				if( n.townNPC ) {
					float distSqr = Vector2.DistanceSquared( Main.LocalPlayer.Center, n.Center );
					if( distSqr < nearestSqr ) {
						nearestNpc = n;
						nearestSqr = distSqr;
					}
				}
				return false;
			} );

			if( nearestNpc == null ) {
				return 0f;
			}

			float dist = (float)Math.Sqrt( nearestSqr );
			float minTownDist = 60f * 16f;
			float maxTownDist = 80f * 16f;
			float diff = maxTownDist - minTownDist;
			float percentAway = MathHelper.Clamp( (dist - minTownDist) / diff, 0f, 1f );
			float percentNear = 1f - percentAway;
			
			return percentNear;
		}


		////////////////

		public override void Update() {
			if( !this.FliesInitialized ) {
				if( Main.npcTexture[NPCID.Firefly] == null ) {
					Main.instance.LoadNPC( NPCID.Firefly );
				}
				if( Main.rand != null ) {
					this.InitializeFireflies();
				}
			}

			if( this.FliesInitialized ) {
				this.UpdateFlyMovement();
			}
		}

		////

		private void UpdateFlyMovement() {
			int count = this.Flies.Count;

			for( int i = 0; i < count; i++ ) {
				this.Flies[i].Update();
			}
		}


		////////////////

		public override void Draw(
					SpriteBatch sb,
					Rectangle rect,
					SceneDrawData drawData,
					float drawDepth ) {
			if( !this.FliesInitialized ) { return; }
			if( Main.dayTime ) { return; }

			var mymod = SurroundingsMod.Instance;

			Color color = this.GetSceneColor( drawData );
			float opacity = this.GetSceneOpacity( drawData );

			if( SurroundingsConfig.Instance.DebugModeSceneInfo ) {
				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer,
					"rect: " + rect +
					", bright: " + drawData.Brightness.ToString("N2") +
					", wall%: " + drawData.WallPercent.ToString("N2") +
					", opacity: " + opacity.ToString("N2") +
					", color: " + color.ToString() +
					", flies: " + string.Join(", ", this.Flies.Select( f=>(int)f.ScreenPosition.X + ":" + (int)f.ScreenPosition.Y) ),
					20
				);
			}

			this.DrawFlies( sb, rect, color, opacity );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}

		////

		private void DrawFlies( SpriteBatch sb, Rectangle rect, Color color, float opacity ) {
			if( opacity == 0f ) {
				return;
			}

			float xScale = ((float)rect.Width / (float)Main.screenWidth) * 2f;
			float yScale = ((float)rect.Height / (float)Main.screenHeight) * 2f;
			var origin = new Vector2( xScale, yScale );
			
			foreach( Firefly fly in this.Flies ) {
				Vector2 pos = fly.ScreenPosition;
				pos.X += (float)rect.X * xScale;
				pos.Y += (float)rect.Y * yScale;

				fly.Draw( sb, pos, color, opacity, origin );
			}
		}
	}
}
