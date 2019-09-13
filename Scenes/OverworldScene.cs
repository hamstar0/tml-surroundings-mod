using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes {
	public class SurfaceForestScene : Scene {
		public override int DrawPriority => 1;

		public override Vector2 Scale => new Vector2( 3.5f, 3.5f );

		public override float HorizontalTileScrollRate => 1f;

		public override float VerticalTileScrollRate => 0f;

		public override SceneContext Context { get; } = new SceneContext {
			Layer = SceneLayer.Near,
			//IsDay = true,
			VanillaBiome = VanillaBiome.Forest
		};



		////////////////

		public SurfaceForestScene() { }


		////////////////

		public void GetSceneTextures( out Texture2D frontTex, out Texture2D backTex ) {
			int frontTexIdx = 17;
			//int backTexIdx = 92;//11;

			Main.instance.LoadBackground( frontTexIdx );
			//Main.instance.LoadBackground( backTexIdx );

			frontTex = Main.backgroundTexture[frontTexIdx];
			backTex = SurroundingsMod.Instance.GetTexture( "Scenes/Overworld" );
			//backTex = Main.backgroundTexture[backTexIdx];
		}

		public Color GetSceneColor( float brightness ) {
			//float shadeScale = ( this.IsNear ? 192f : 255f ) * brightness;
			float shadeScale = 192f * brightness;
			byte shade = (byte)Math.Min( shadeScale, 255 );

			var color = new Color( shade, shade, shade, 255 );

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

		public override void Draw(
				SpriteBatch sb,
				Rectangle rect,
				SceneDrawData drawdata,
				float opacity,
				float drawDepth ) {
			var mymod = SurroundingsMod.Instance;
			Texture2D frontTex;
			Texture2D backTex;
			this.GetSceneTextures( out frontTex, out backTex );

			float cavePercent = Math.Max( drawdata.WallPercent - 0.6f, 0f ) * 2.5f;

			Color backColor = this.GetSceneColor( drawdata.Brightness ) * (1f - cavePercent) * opacity;
			//Color frontColor = backColor;
			//frontColor.R = (byte)((float)frontColor.R * 0.75f);
			//frontColor.B = 0;
			//frontColor.G = (byte)((float)(frontColor.G/2) * 0.75f);

			if( mymod.Config.DebugModeInfo ) {
				DebugHelpers.Print( "OverworldScene",
					"brightness: " + drawdata.Brightness +
					", opacity: "+opacity +
					", cavePercent: " + cavePercent.ToString("N2") +
					", color: " + backColor.ToString(),
					20
				);
			}

			float yPercent = this.GetSceneVerticalRangePercent( drawdata.Center );
			float scale = rect.Width / backTex.Width;

			//Rectangle frontRect = rect;
			Rectangle backRect = rect;
			backRect.Height = (int)((float)backTex.Height * scale);
			backRect.Y += this.GetSceneTextureVerticalOffset( yPercent, frontTex.Height ) + 128;
			//frontRect.Y = backRect.Y + 512 + 128;

			sb.Draw( backTex, backRect, null, backColor );
			//sb.Draw( frontTex, frontRect, null, frontColor );

			// I want to try to get drawDepth working at some point:
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
