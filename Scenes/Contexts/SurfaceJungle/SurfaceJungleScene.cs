using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceJungle {
	public class SurfaceJungleScene : Scene {
		public override SceneContext Context { get; }

		////

		public override int DrawPriority => 1;

		////

		public override Vector2 SceneScale {
			get {
				switch( this.Context.Layer ) {
				case SceneLayer.Near:
					return new Vector2( 3.5f );
				case SceneLayer.Far:
					return new Vector2( 2.5f );
				case SceneLayer.Screen:
				default:
					return new Vector2( 1f );
				}
			}
		}

		public override float HorizontalTileScrollRate {
			get {
				switch( this.Context.Layer ) {
				case SceneLayer.Near:
					return 2f;
				case SceneLayer.Far:
					return 2f;
				case SceneLayer.Screen:
				default:
					return 0f;
				}
			}
		}

		public override float VerticalTileScrollRate => 0f;

		////

		public override MistSceneDefinition MistDefinition => null;



		////////////////

		public SurfaceJungleScene( SceneLayer layer ) {
			this.Context = new SceneContext {
				Layer = layer,
				//IsDay = true,
				VanillaBiome = VanillaBiome.Forest
			};
		}


		////////////////

		public Texture2D GetSceneTexture() {
			Main.instance.LoadBackground( 61 );

			return Main.backgroundTexture[51];
		}

		public override Color GetSceneColor( SceneDrawData drawData ) {
			float cavePercent = Math.Max( drawData.WallPercent - 0.6f, 0f ) * 2.5f;
			byte shade = (byte)Math.Min( 192f * drawData.Brightness, 255 );

			var color = new Color( shade, shade, shade, 255 );

			return color * (1f - cavePercent) * drawData.Opacity;
		}

		////////////////

		public float GetSceneVerticalRangePercent( Vector2 origin ) {
			int plrTileY = (int)( origin.Y / 16 );
			float range = WorldHelpers.SurfaceLayerBottom - WorldHelpers.SurfaceLayerTop;
			float yPercent = (float)( plrTileY - WorldHelpers.SurfaceLayerTop ) / range;
			return 1f - yPercent;
		}

		public int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			var mymod = SurroundingsMod.Instance;
			float height = (float)texHeight * this.SceneScale.Y;

			int offset = (int)( yPercent * (float)height * 0.3f );
			offset += -128;
			return offset;
		}


		////////////////

		public override void Draw(
				SpriteBatch sb,
				Rectangle rect,
				SceneDrawData drawData,
				float drawDepth ) {
			var mymod = SurroundingsMod.Instance;
			Texture2D tex = this.GetSceneTexture();

			Color color = this.GetSceneColor( drawData );

			if( mymod.Config.DebugModeInfo ) {
				DebugHelpers.Print( "SurfaceJungleScene_"+this.Context.Layer,
					"brightness: " + drawData.Brightness.ToString( "N2" ) +
					", wall%: " + drawData.WallPercent.ToString( "N2" ) +
					", opacity: " + drawData.Opacity.ToString( "N2" ) +
					", color: " + color.ToString(),
					20
				);
			}

			float yPercent = this.GetSceneVerticalRangePercent( drawData.Center );
			float scale = rect.Width / tex.Width;

			rect.Height = (int)( (float)tex.Height * scale );
			rect.Y += this.GetSceneTextureVerticalOffset( yPercent, tex.Height ) + 192;
			rect.Y -= this.Context.Layer == SceneLayer.Near ? 0 : -128;

			sb.Draw( tex, rect, null, color );

			// I want to try to get drawDepth working at some point:
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
