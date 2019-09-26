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

			Func<AnimatedTexture, (int NextFrame, int Duration)> animator = ( animTex ) => {
				if( animTex.CurrentFrame >= (animTex.MaxFrames - 1) ) {
					if( Main.rand.NextFloat() >= 0.9f ) {
						return (0, 8);
					} else {
						return (2, 8);
					}
				}
				return (animTex.CurrentFrame + 1, 8);
			};

			for( int i = 0; i < 7; i++ ) {
				this.Flies.Add( new Firefly(
					animation: AnimatedTexture.Create( Main.npcTexture[NPCID.Firefly], 4, animator ),
					screenPosition: new Vector2( Main.rand.Next( 0, Main.screenWidth ), Main.rand.Next( 0, Main.screenHeight ) ),
					velocity: new Vector2( Main.rand.NextFloat() - 0.5f, Main.rand.NextFloat() - 0.5f ),
					acceleration: 0
				) );
			}
		}


		////////////////

		public override Color GetSceneColor( SceneDrawData drawData ) {
			float cavePercent = Math.Max( drawData.WallPercent - 0.5f, 0f ) * 2f;
			//float shadeScale = ( this.IsNear ? 192f : 255f ) * brightness;
			float shadeScale = 192f * drawData.Brightness;
			byte shade = (byte)Math.Min( shadeScale, 255 );

			var color = new Color( shade, shade, shade, 96 );

			return color * ( 1f - cavePercent ) * drawData.Opacity;
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

			Color color = this.GetSceneColor(drawData);

			if( mymod.Config.DebugModeSceneInfo ) {
				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer,
					"rect: " + rect +
					", bright: " + drawData.Brightness.ToString("N2") +
					", wall%: " + drawData.WallPercent.ToString("N2") +
					", opacity: " + drawData.Opacity.ToString("N2") +
					", color: " + color.ToString() +
					", flies: " + string.Join(", ", this.Flies.Select( f=>(int)f.ScreenPosition.X + ":" + (int)f.ScreenPosition.Y) ),
					20
				);
			}

			this.DrawFlies( sb, rect, color, drawData.Opacity );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}


		private void DrawFlies( SpriteBatch sb, Rectangle rect, Color color, float opacity ) {
			float xScale = ((float)rect.Width / (float)Main.screenWidth) * 2f;
			float yScale = ((float)rect.Height / (float)Main.screenHeight) * 2f;

			foreach( Firefly fly in this.Flies ) {
				Vector2 pos = fly.ScreenPosition;
				pos.X += (float)rect.X * xScale;
				pos.Y += (float)rect.Y * yScale;

				Color flyColor = fly.Animation.CurrentFrame <= 1 ?
					Color.Yellow * opacity :
					color;

//DebugHelpers.Print("fly", "Frame: "+fly.Animation.CurrentFrame+" | "+fly.Animation.CurrentFrameTicksElapsed, 20);
				fly.Animation.Draw( sb, pos, flyColor, 0f, null, new Vector2(xScale, yScale) );
			}
		}
	}
}
