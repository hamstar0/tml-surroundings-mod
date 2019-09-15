using System;
using System.Collections.Generic;
using System.Linq;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using HamstarHelpers.Services.AnimatedTexture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components;
using Terraria;
using Terraria.ID;


namespace Surroundings.Scenes {
	public partial class SurfaceForestNightScene : Scene {
		private IList<FireflyDefinition> Flies = new List<FireflyDefinition>();

		private bool FliesInitialized = false;


		////////////////

		public bool IsNear { get; private set; }

		public override int DrawPriority => 2;

		public override Vector2 Scale => new Vector2( 1f, 1f );

		public override float HorizontalTileScrollRate => 1f;

		public override float VerticalTileScrollRate => 0f;

		public override SceneContext Context { get; }



		////////////////

		public SurfaceForestNightScene( bool isNear ) {
			this.IsNear = isNear;
			this.Context = new SceneContext {
				Layer = this.IsNear ? SceneLayer.Near : SceneLayer.Far,
				VanillaBiome = VanillaBiome.Forest
			};

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
				this.Flies.Add( new FireflyDefinition {
					Animation = AnimatedTexture.Create( Main.npcTexture[NPCID.Firefly], 4, animator ),
					ScreenPosition = new Vector2( Main.rand.Next( 0, Main.screenWidth ), Main.rand.Next( 0, Main.screenHeight ) ),
					Velocity = new Vector2( Main.rand.NextFloat() - 0.5f, Main.rand.NextFloat() - 0.5f ),
					Acceleration = 0
				} );
			}
		}


		////////////////

		public Color GetSceneColor( float brightness ) {
			//float shadeScale = ( this.IsNear ? 192f : 255f ) * brightness;
			float shadeScale = 192f * brightness;
			byte shade = (byte)Math.Min( shadeScale, 255 );

			var color = new Color( shade, shade, shade, 96 );

			return color;
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
				float opacity,
				float drawDepth ) {
			if( !this.FliesInitialized ) { return; }
			if( Main.dayTime ) { return; }

			var mymod = SurroundingsMod.Instance;

			float cavePercent = Math.Max( drawData.WallPercent - 0.5f, 0f ) * 2f;
			Color color = this.GetSceneColor(drawData.Brightness) * (1f - cavePercent) * opacity;

			if( mymod.Config.DebugModeInfo ) {
				DebugHelpers.Print( "SurfaceForestNightScene",
					"rect: " + rect +
					", bright: " + drawData.Brightness.ToString("N2") +
					", cave%: " + cavePercent.ToString("N2") +
					", opacity: " + opacity.ToString("N2") +
					", color: " + color.ToString() +
					", flies: " + string.Join(", ", this.Flies.Select( f=>(int)f.ScreenPosition.X + ":" + (int)f.ScreenPosition.Y) ),
					20
				);
			}

			this.DrawFlies( sb, rect, color, opacity );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}


		private void DrawFlies( SpriteBatch sb, Rectangle rect, Color color, float opacity ) {
			float xScale = ((float)rect.Width / (float)Main.screenWidth) * 2f;
			float yScale = ((float)rect.Height / (float)Main.screenHeight) * 2f;

			foreach( FireflyDefinition fly in this.Flies ) {
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
