using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts {
	public abstract class CavernScene : Scene {
		public override int DrawPriority { get; } = 1;

		public override float VerticalTileScrollRate { get; } = 0f;



		////////////////

		public abstract Texture2D GetSceneTexture();

		public override Color GetSceneColor( SceneDrawData drawData ) {
			float cavePercent = Math.Max( drawData.WallPercent - 0.6f, 0f ) * 2.5f;
			byte shade = (byte)Math.Min( 192f * drawData.Brightness, 255 );

			var color = new Color( shade, shade, shade, 255 );

			return color * (1f - cavePercent) * drawData.Opacity;
		}

		////////////////

		public float GetSceneVerticalRangePercent( Vector2 origin ) {
			float wldRange = 192f * 16f;
			float withinRange = origin.Y % wldRange;
			float percent = withinRange / wldRange;

			return percent;
		}

		public abstract int GetSceneTextureVerticalOffset( float yPercent, int texHeight );

		public float GetSublayerColorFadePercent( float yPercent ) {
			float threshold = 0.75f;
			float abs = Math.Abs( yPercent );
			if( abs < threshold ) {
				return 1f;
			}

			return (abs - threshold) / (1f - threshold);
		}


		////////////////

		public sealed override void Draw(
				SpriteBatch sb,
				Rectangle rect,
				SceneDrawData drawData,
				float drawDepth ) {
			var mymod = SurroundingsMod.Instance;
			Texture2D tex = this.GetSceneTexture();

			Color color = this.GetSceneColor( drawData );

			float yPercent = this.GetSceneVerticalRangePercent( drawData.Center );
			int yOffset1 = this.GetSceneTextureVerticalOffset( yPercent - 1, rect.Height );
			int yOffset2 = this.GetSceneTextureVerticalOffset( yPercent, rect.Height );
			int oldY = rect.Y;

			if( mymod.Config.DebugModeSceneInfo ) {
				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer,
					"brightness: " + drawData.Brightness.ToString( "N2" ) +
					", wall%: " + drawData.WallPercent.ToString( "N2" ) +
					", opacity: " + drawData.Opacity.ToString( "N2" ) +
					", color: " + color.ToString() +
					", yPercent: " + yPercent.ToString( "N2" ) +
					//", texZoom: " + texZoom.ToString( "N2" ) +
					", rect: "+ rect,
					20
				);
				//HUDHelpers.DrawBorderedRect( sb, null, Color.Gray, rect, 2 );
			}

			rect.Y = oldY + yOffset1;
			sb.Draw( tex, rect, null, color * this.GetSublayerColorFadePercent(yPercent - 1) );

			rect.Y = oldY + yOffset2;
			sb.Draw( tex, rect, null, color * this.GetSublayerColorFadePercent(yPercent) );

			// I want to try to get drawDepth working at some point:
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
