﻿using System;
using ModLibsCore.Libraries.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts {
	public abstract class JungleVinesScene : Scene {
		private Texture2D CachedTex = null;


		////////////////

		public override int DrawPriority { get; } = 1;



		////////////////

		public virtual Texture2D GetSceneTexture() {
			if( this.CachedTex == null ) {
				this.CachedTex = SurroundingsMod.Instance.GetTexture( "Scenes/Contexts/JungleVines" );
			}
			return this.CachedTex;
		}

		public override Color GetSceneColor( SceneDrawData drawData ) {
			byte shade = (byte)Math.Min( 255f * drawData.Brightness, 255 );

			return new Color( shade, shade, shade, 255 );
		}

		public override float GetSceneOpacity( SceneDrawData drawData ) {
			return drawData.Opacity;
		}


		////////////////

		public sealed override void Draw(
					SpriteBatch sb,
					Rectangle rect,
					SceneDrawData drawData,
					float drawDepth ) {
			Texture2D tex = this.GetSceneTexture();

			Color color = this.GetSceneColor(drawData) * this.GetSceneOpacity(drawData);

			if( SurroundingsConfig.Instance.DebugModeSceneInfo ) {
				DebugLibraries.Print( this.GetType().Name + "_" + this.Context.Layer,
					"brightness: " + drawData.Brightness.ToString( "N2" ) +
					", opacity%: " + this.GetSceneOpacity(drawData).ToString( "N2" ) +
					", base color: " + this.GetSceneColor(drawData).ToString() +
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
