using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts {
	public abstract class SurfaceTreeScene : Scene {
		public override int DrawPriority { get; } = 1;

		public override float VerticalTileScrollRate { get; } = 0f;



		////////////////

		public abstract Texture2D GetSceneTexture();

		public override Color GetSceneColor( SceneDrawData drawData ) {
			float occludedPercent = drawData.WallPercent + (drawData.CavePercent - drawData.CaveAndWallPercent);
			float relevantOcclusionPercent = Math.Max( occludedPercent - 0.6f, 0f ) * 2.5f;
			byte shade = (byte)Math.Min( 192f * drawData.Brightness, 255 );

			var color = new Color( shade, shade, shade, 255 );

			return color * (1f - relevantOcclusionPercent) * drawData.Opacity;
		}

		////////////////

		public float GetSceneVerticalRangePercent( Vector2 origin ) {
			int plrTileY = (int)( origin.Y / 16 );
			float range = WorldHelpers.SurfaceLayerBottomTileY - WorldHelpers.SurfaceLayerTopTileY;
			float yPercent = (float)( plrTileY - WorldHelpers.SurfaceLayerTopTileY ) / range;
			return 1f - yPercent;
		}

		public abstract int GetSceneTextureVerticalOffset( float yPercent, int texHeight );


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
			rect.Y += this.GetSceneTextureVerticalOffset( yPercent, rect.Height );

			if( SurroundingsConfig.Instance.DebugModeSceneInfo ) {
				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer,
					"brightness: " + drawData.Brightness.ToString( "N2" ) +
					", wall%: " + drawData.WallPercent.ToString( "N2" ) +
					", caveAndWall%: " + drawData.CaveAndWallPercent.ToString( "N2" ) +
					", opacity%: " + drawData.Opacity.ToString( "N2" ) +
					", color: " + color.ToString() +
					", yPercent: " + yPercent.ToString( "N2" ) +
					//", texZoom: " + texZoom.ToString( "N2" ) +
					", rect: "+ rect,
					20
				);
				//HUDHelpers.DrawBorderedRect( sb, null, Color.Gray, rect, 2 );
			}

			sb.Draw( tex, rect, null, color );

			// I want to try to get drawDepth working at some point:
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
