using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace Surroundings {
	public partial class SceneDraw {
		public void DrawScenesOfScreenLayer( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;
			IEnumerable<(Scene, float)> scenes = mymod.ScenePicker.GetActiveScenes( SceneLayer.Screen );
			Rectangle screenFrame = this.GetTextureFrameOfScreenLayer();

			foreach( (Scene scene, float opacity) in scenes ) {
				drawData.Opacity = opacity;
				scene.DrawBase( sb, screenFrame, drawData, 4f );
			}
		}


		public void DrawScenesOfNearLayer( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;
			IEnumerable<(Scene, float)> scenes = mymod.ScenePicker.GetActiveScenes( SceneLayer.Near );

			Vector2 wldOrigin = Main.screenPosition;//Main.LocalPlayer.Center;
			wldOrigin.X += Main.screenWidth / 2;
			wldOrigin.Y += Main.screenHeight / 2;
			
			foreach( (Scene scene, float opacity) in scenes ) {
				foreach( Rectangle screenFrame in this.GetTiledTextureFramesOfNearLayer( wldOrigin, scene ) ) {
					drawData.Opacity = opacity;
					scene.DrawBase( sb, screenFrame, drawData, 3f );
				}
			}
		}


		public void DrawScenesOfFarLayer( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;
			Vector2 wldOrigin = Main.screenPosition;//Main.LocalPlayer.Center;
			wldOrigin.X += Main.screenWidth / 2;
			wldOrigin.Y += Main.screenHeight / 2;
			IEnumerable<(Scene, float)> scenes = mymod.ScenePicker.GetActiveScenes( SceneLayer.Far );

			foreach( (Scene scene, float opacity) in scenes ) {
				foreach( Rectangle screenFrame in this.GetTiledTextureFramesOfFarLayer( wldOrigin, scene ) ) {
					drawData.Opacity = opacity;
					scene.DrawBase( sb, screenFrame, drawData, 2f );
				}
			}
		}


		public void DrawScenesOfGameLayer( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;
			Vector2 wldOrigin = Main.screenPosition;//Main.LocalPlayer.Center;
			wldOrigin.X += Main.screenWidth / 2;
			wldOrigin.Y += Main.screenHeight / 2;
			IEnumerable<(Scene, float)> scenes = mymod.ScenePicker.GetActiveScenes( SceneLayer.Game );

			foreach( (Scene scene, float opacity) in scenes ) {
				foreach( Rectangle screenFrame in this.GetTiledTextureFramesOfGameLayer( wldOrigin, scene ) ) {
					/*if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
						continue;
					}
					if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
						continue;
					}*/

					drawData.Opacity = opacity;
					scene.DrawBase( sb, screenFrame, drawData, 1f );
				}
			}
		}
	}
}
