using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes {
	public partial class SurfaceRainScene : Scene {
		public bool IsNear { get; private set; }

		public override int DrawPriority => 2;

		public override Vector2 Scale => new Vector2( 1f, 1f );

		public override float HorizontalTileScrollRate => 1f;

		public override float VerticalTileScrollRate => 0f;

		public override SceneContext Context { get; }



		////////////////

		public SurfaceRainScene( bool isNear, VanillaBiome biome ) {
			this.IsNear = isNear;
			this.Context = new SceneContext {
				Layer = this.IsNear ? SceneLayer.Near : SceneLayer.Far,
				VanillaBiome = biome
			};
		}


		////////////////

		public Color GetSceneColor( float brightness ) {
			byte shade = (byte)Math.Min( brightness * 0.85f, 255 );

			Color color = new Color( shade, shade, shade, 255 );

			return color;
		}


		////////////////

		public override void Draw(
				SpriteBatch sb,
				Rectangle rect,
				SceneDrawData drawData,
				float opacity,
				float drawDepth ) {
			var mymod = SurroundingsMod.Instance;

			float cavePercent = Math.Max( drawData.WallPercent - 0.5f, 0f ) * 2f;
			Color color = this.GetSceneColor(drawData.Brightness) * (1f - cavePercent) * opacity;

			if( mymod.Config.DebugModeInfo ) {
				DebugHelpers.Print( "SurfaceRainScene",
					"rect: " + rect +
					", bright: " + drawData.Brightness +
					", cave%: " + cavePercent.ToString("N2") +
					", color: " + color.ToString() +
					", opacity: " + opacity,
					20
				);
			}

			this.DrawRain( sb, rect, color );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}

		////

		protected void DrawRain( SpriteBatch sb, Rectangle rect, Color color ) {
			var rainTypeRects = new Rectangle[6];
			for( int i = 0; i < rainTypeRects.Length; i++ ) {
				rainTypeRects[i] = new Rectangle( i * 4, 0, 2, 40 );
			}

			float scale = (float)rect.Width / (float)Main.screenWidth;

			for( int j = 0; j < Main.maxRain; j++ ) {
				if( !Main.rain[j].active ) {
					continue;
				}

				Rain rain = Main.rain[j];

				Vector2 pos = rain.position - Main.screenPosition;
				pos.X += rect.X;
				pos.Y += rect.Y;

				var rainRect = new Rectangle?( rainTypeRects[(int)rain.type] );

				sb.Draw( Main.rainTexture,
					pos,
					rainRect,
					color,
					rain.rotation,
					Vector2.Zero,
					scale * rain.scale,
					SpriteEffects.None,
					0f
				);
			}
		}
	}
}
