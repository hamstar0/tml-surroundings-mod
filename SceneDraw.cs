using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;


namespace Surroundings {
	public partial class SceneDraw {
		public void DrawScenesOfScreenLayer( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;
			IEnumerable<(Scene, float)> scenes = mymod.ScenePicker.GetActiveScenes( SceneLayer.Screen );
			Rectangle rect = this.GetFrameOfScreenLayer();

			foreach( (Scene scene, float opacity) in scenes ) {
				drawData.Opacity = opacity;
				scene.DrawBase( sb, rect, drawData, 4f );
			}
		}


		public void DrawScenesOfNearLayer( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;
			Vector2 center = Main.LocalPlayer.Center;
			IEnumerable<(Scene, float)> scenes = mymod.ScenePicker.GetActiveScenes( SceneLayer.Near );
			
			foreach( (Scene scene, float opacity) in scenes ) {
				foreach( Rectangle rect in this.GetFramesOfNearLayer( center, scene ) ) {
					drawData.Opacity = opacity;
					scene.DrawBase( sb, rect, drawData, 3f );
				}
			}
		}


		public void DrawScenesOfFarLayer( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;
			Vector2 center = Main.LocalPlayer.Center;
			IEnumerable<(Scene, float)> scenes = mymod.ScenePicker.GetActiveScenes( SceneLayer.Far );

			foreach( (Scene scene, float opacity) in scenes ) {
				foreach( Rectangle rect in this.GetFramesOfFarLayer( center, scene ) ) {
					drawData.Opacity = opacity;
					scene.DrawBase( sb, rect, drawData, 2f );
				}
			}
		}


		public void DrawScenesOfGameLayer( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;
			Vector2 center = Main.LocalPlayer.Center;
			IEnumerable<(Scene, float)> scenes = mymod.ScenePicker.GetActiveScenes( SceneLayer.Game );

			foreach( (Scene scene, float opacity) in scenes ) {
				foreach( Rectangle rect in this.GetFramesOfGameLayer( center, scene ) ) {
					if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
						continue;
					}
					if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
						continue;
					}

					drawData.Opacity = opacity;
					scene.DrawBase( sb, rect, drawData, 1f );
				}
			}
		}
	}
}
