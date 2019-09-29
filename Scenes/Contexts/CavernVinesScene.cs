using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts {
	public abstract class CavernVinesScene : Scene {
		public override int DrawPriority { get; } = 1;



		////////////////

		public abstract Texture2D GetSceneTexture();

		public override Color GetSceneColor( SceneDrawData drawData ) {
			byte shade = (byte)Math.Min( 255f * drawData.Brightness, 255 );

			var color = new Color( shade, shade, shade, 255 );

			return color * drawData.Opacity;
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

			if( mymod.Config.DebugModeSceneInfo ) {
				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer,
					"brightness: " + drawData.Brightness.ToString( "N2" ) +
					", opacity%: " + drawData.Opacity.ToString( "N2" ) +
					", color: " + color.ToString() +
					", rect: "+ rect,
					20
				);
			}

			sb.Draw( tex, rect, null, color );

			// I want to try to get drawDepth working at some point:
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
