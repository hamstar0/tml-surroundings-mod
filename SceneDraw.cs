using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;


namespace Surroundings {
	public partial class SceneDraw {
		public void Update() {
			var mymod = SurroundingsMod.Instance;
			SceneContext ctx = mymod.ScenePicker.GetCurrentContextSansLayer();

			ctx.Layer = SceneLayer.Screen;
			mymod.ScenePicker.UpdateScenesPerContext( ctx );

			ctx.Layer = SceneLayer.Near;
			mymod.ScenePicker.UpdateScenesPerContext( ctx );

			ctx.Layer = SceneLayer.Far;
			mymod.ScenePicker.UpdateScenesPerContext( ctx );

			ctx.Layer = SceneLayer.Screen;
			mymod.ScenePicker.UpdateScenesPerContext( ctx );
		}


		////////////////

		public void DrawSceneScreen( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;
			IEnumerable<(Scene, float)> scenes = mymod.ScenePicker.GetScenes( SceneLayer.Screen );
			Rectangle rect = this.GetOffsetScreen();

			if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
				return;
			}
			if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
				return;
			}

			foreach( (Scene scene, float opacity) in scenes ) {
				scene.Draw( sb, rect, drawData, opacity, 4f );
			}
		}


		public void DrawSceneNear( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;
			IEnumerable<(Scene, float)> scenes = mymod.ScenePicker.GetScenes( SceneLayer.Near );
			
			foreach( (Scene scene, float opacity) in scenes ) {
				foreach( Rectangle rect in this.GetOffsetsNear( scene ) ) {
					if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
						continue;
					}
					if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
						continue;
					}

					scene.Draw( sb, rect, drawData, opacity, 3f );
				}
			}
		}


		public void DrawSceneFar( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;
			IEnumerable<(Scene, float)> scenes = mymod.ScenePicker.GetScenes( SceneLayer.Far );

			foreach( (Scene scene, float opacity) in scenes ) {
				foreach( Rectangle rect in this.GetOffsetsFar( scene ) ) {
					if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
						continue;
					}
					if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
						continue;
					}

					scene.Draw( sb, rect, drawData, opacity, 2f );
				}
			}
		}


		public void DrawSceneGame( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;
			IEnumerable<(Scene, float)> scenes = mymod.ScenePicker.GetScenes( SceneLayer.Game );

			foreach( (Scene scene, float opacity) in scenes ) {
				foreach( Rectangle rect in this.GetOffsetsGame( scene ) ) {
					if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
						continue;
					}
					if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
						continue;
					}

					scene.Draw( sb, rect, drawData, opacity, 1f );
				}
			}
		}
	}
}
