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
					if( y >= WorldHelpers.DirtLayerTop ) {
						brightness += Lighting.Brightness( x, y );
						wallPercent += 1;
						continue;
					}

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

		public override int Priority => 1;

		public override Vector2 Scale => new Vector2(3.5f, 3.5f);

		public override float HorizontalTileScrollRate => 1f;

		public override float VerticalTileScrollRate => 0f;

		public override SceneContext GetContext => new SceneContext {
			Layer = SceneLayer.Near,
			//IsDay = true,
			VanillaBiome = VanillaBiome.Forest
		};



		////////////////

		public OverworldScene() { }


		////////////////

		public void GetSceneTextures( out Texture2D frontTex, out Texture2D backTex ) {
			int frontTexIdx = 17;
			int backTexIdx = 92;//11;

			Main.instance.LoadBackground( frontTexIdx );
			Main.instance.LoadBackground( backTexIdx );

			frontTex = Main.backgroundTexture[ frontTexIdx ];
			backTex = Main.backgroundTexture[ backTexIdx ];
		}

		public Color GetSceneColor( float brightness, float cavePercent ) {
			//float shadeScale = ( this.IsNear ? 192f : 255f ) * brightness;
			float shadeScale = 192f * brightness;
			byte shade = (byte)Math.Min( shadeScale, 255 );

			var color = new Color( shade, shade, shade, 255 );
			color.A = (byte)(255f * Math.Max( 1f - cavePercent, 0f ));

			return color;
		}

		public float GetSceneVerticalRangePercent( Vector2 origin ) {
			int plrTileY = (int)( origin.Y / 16 );
			float range = WorldHelpers.SurfaceLayerBottom - WorldHelpers.SurfaceLayerTop;
			float yPercent = (float)( plrTileY - WorldHelpers.SurfaceLayerTop ) / range;
			return 1f - yPercent;
		}

		public int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			var mymod = SurroundingsMod.Instance;
			float height = (float)texHeight * this.Scale.Y;

			int offset = (int)( yPercent * (float)height * 0.3f );
			offset += -128;
			return offset;
		}

		////////////////

		public override void Draw( SpriteBatch sb, Rectangle rect, float depth ) {
			var mymod = SurroundingsMod.Instance;
			Vector2 origin = Main.LocalPlayer.Center;
			Texture2D frontTex, backTex;
			this.GetSceneTextures( out frontTex, out backTex );

			float brightness, wallPercent, cavePercent;
			OverworldScene.GetEnvironmentData( origin, out brightness, out wallPercent );
			cavePercent = Math.Max( wallPercent - 0.5f, 0f ) * 2f;

			Color backColor = this.GetSceneColor( brightness, cavePercent );
			Color frontColor = backColor;
			frontColor.B = 0;
			frontColor.G /= 2;

			if( mymod.Config.DebugModeInfo ) {
				DebugHelpers.Print( "OverworldDayScene",
					"brightness: "+brightness
					+", cavePercent: " + cavePercent.ToString("N2")+" ("+(1f-cavePercent).ToString("N2") + ")" +
					", color: "+backColor.ToString(),
					20
				);
			}

			float yPercent = this.GetSceneVerticalRangePercent( origin );

			Rectangle frontRect = rect, backRect = rect;
			backRect.Y += this.GetSceneTextureVerticalOffset( yPercent, frontTex.Height );
			frontRect.Y = backRect.Y + 512;

			sb.Draw( backTex, backRect, null, backColor );
			sb.Draw( frontTex, frontRect, null, frontColor );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
