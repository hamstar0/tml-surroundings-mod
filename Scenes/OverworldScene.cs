using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes {
	public class OverworldScene : Scene {
		public static void GetEnvironmentData( Vector2 origin, out float brightness, out float wallPercent ) {
			Vector2 brightnessCheckPoint = origin;
			int brightnessCheckTileX = (int)( brightnessCheckPoint.X * 0.0625f );
			int brightnessCheckTileY = (int)( brightnessCheckPoint.Y * 0.0625f );
			int minX = brightnessCheckTileX - 16;
			int minY = brightnessCheckTileY - 12;
			int maxX = brightnessCheckTileX + 32;
			int maxY = brightnessCheckTileY + 24;

			brightness = 0;
			wallPercent = 0;

			for( int x=minX; x<maxX; x++ ) {
				for( int y=minY; y<maxY; y++ ) {
					Tile tile = Framing.GetTileSafely( x, y );

					brightness += Lighting.Brightness( x, y );
					wallPercent += tile.wall != 0 ? 1 : 0;
				}
			}

			int total = ( maxX - minX ) * ( maxY - minY );
			brightness /= total;
			wallPercent /= total;
		}



		////////////////

		private bool IsNear;


		////////////////

		public override int Priority => 1;

		public override Vector2 Scale => this.IsNear ? new Vector2(3.5f, 3.5f) : new Vector2(1f, 1f);

		public override float HorizontalTileScrollRate => this.IsNear ? 1f : 0.25f;

		public override float VerticalTileScrollRate => 0f;

		public override SceneContext GetContext => new SceneContext {
			Layer = this.IsNear ? SceneLayer.Near : SceneLayer.Far,
			//IsDay = true,
			VanillaBiome = VanillaBiome.Forest
		};



		////////////////

		public OverworldScene( bool isNear ) {
			this.IsNear = isNear;
		}


		////////////////

		public Texture2D GetSceneTexture() {
			int texIdx = this.IsNear ? 92 : 17;//11;

			Main.instance.LoadBackground( texIdx );

			return Main.backgroundTexture[texIdx];
		}

		public Color GetSceneColor( float brightness, float cavePercent ) {
			float shadeScale = ( this.IsNear ? 192f : 255f ) * brightness;
			byte shade = (byte)Math.Min( shadeScale, 255 );

			var color = new Color( shade, shade, shade, 255 );
			color = color * Math.Max( 1f - cavePercent, 0f );

			return color;
		}

		public float GetSceneVerticalRangePercent( Vector2 origin ) {
			int plrTileY = (int)( origin.Y / 16 );
			float range = WorldHelpers.SurfaceLayerBottom - WorldHelpers.SurfaceLayerTop;
			float yPercent = (float)( plrTileY - WorldHelpers.SurfaceLayerTop ) / range;
			return 1f - yPercent;
		}

		public int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			float scale = ( this.Scale.Y - 1f ) * 0.5f;
			scale += 1f;

			int offset = (int)( yPercent * (float)texHeight * scale );

			if( this.IsNear ) {
				offset += 320 - (int)( (float)256 * scale );
			} else {
				offset += 580 - (int)( (float)300 * scale );
			}

			return offset;
		}

		////////////////

		public override void Draw( SpriteBatch sb, Rectangle rect, float depth ) {
			var mymod = SurroundingsMod.Instance;
			Vector2 origin = Main.LocalPlayer.Center;
			Texture2D tex = this.GetSceneTexture();

			float brightness, wallPercent, cavePercent;
			OverworldScene.GetEnvironmentData( origin, out brightness, out wallPercent );
			cavePercent = Math.Max( wallPercent - 0.5f, 0f ) * 2f;

			Color color = this.GetSceneColor( brightness, wallPercent );

			if( mymod.Config.DebugModeInfo ) {
				DebugHelpers.Print( "OverworldDayScene_" + (this.IsNear ? "Near" : "Far"),
					"cavePercent: " + cavePercent + ", color: "+color.ToString(),
					20
				);
			}

			float yPercent = this.GetSceneVerticalRangePercent( origin );
			
			rect.Y += this.GetSceneTextureVerticalOffset( yPercent, tex.Height );

			sb.Draw( tex, rect, null, color );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
