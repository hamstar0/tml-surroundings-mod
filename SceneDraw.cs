using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;


namespace Surroundings {
	public partial class SceneDraw {
		public void DrawSceneScreen( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;
			IEnumerable<(Scene, float)> scenes = mymod.ScenePicker.GetActiveScenes( SceneLayer.Screen );
			Rectangle rect = this.GetOffsetScreen();

			if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
				return;
			}
			if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
				return;
			}

			foreach( (Scene scene, float opacity) in scenes ) {
				drawData.Opacity = opacity;
				scene.DrawBase( sb, rect, drawData, 4f );
			}
		}


		public void DrawSceneNear( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;
			IEnumerable<(Scene, float)> scenes = mymod.ScenePicker.GetActiveScenes( SceneLayer.Near );
			
			foreach( (Scene scene, float opacity) in scenes ) {
				foreach( Rectangle rect in this.GetOffsetsNear( scene ) ) {
					if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
						continue;
					}
					if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
						continue;
					}

					drawData.Opacity = opacity;
					scene.DrawBase( sb, rect, drawData, 3f );
				}
			}
		}


		public void DrawSceneFar( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;
			IEnumerable<(Scene, float)> scenes = mymod.ScenePicker.GetActiveScenes( SceneLayer.Far );

			foreach( (Scene scene, float opacity) in scenes ) {
				foreach( Rectangle rect in this.GetOffsetsFar( scene ) ) {
					if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
						continue;
					}
					if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
						continue;
					}

					drawData.Opacity = opacity;
					scene.DrawBase( sb, rect, drawData, 2f );
				}
			}
		}


		public void DrawSceneGame( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;
			IEnumerable<(Scene, float)> scenes = mymod.ScenePicker.GetActiveScenes( SceneLayer.Game );

			foreach( (Scene scene, float opacity) in scenes ) {
				foreach( Rectangle rect in this.GetOffsetsGame( scene ) ) {
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
