using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;


namespace Surroundings {
	public partial class SceneDraw {
		public void DrawSceneScreen( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;
			SceneContext ctx = mymod.Logic.GetCurrentContext( SceneLayer.Screen );

			var scene = mymod.Logic.GetScene( ctx );
			if( scene == null ) {
				return;
			}

			Rectangle rect = this.GetOffsetScreen();

			if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
				return;
			}
			if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
				return;
			}

			scene.Draw( sb, rect, 4f );
		}


		public void DrawSceneNear( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;
			SceneContext ctx = mymod.Logic.GetCurrentContext( SceneLayer.Near );

			Scene scene = mymod.Logic.GetScene( ctx );
			if( scene == null ) {
				return;
			}

			foreach( Rectangle rect in this.GetOffsetsNear(scene) ) {
				if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
					continue;
				}
				if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
					continue;
				}

				scene.Draw( sb, rect, 3f );
			}
		}


		public void DrawSceneFar( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;
			SceneContext ctx = mymod.Logic.GetCurrentContext( SceneLayer.Far );

			var scene = mymod.Logic.GetScene( ctx );
			if( scene == null ) {
				return;
			}

			foreach( Rectangle rect in this.GetOffsetsFar(scene) ) {
				if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
					continue;
				}
				if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
					continue;
				}

				scene.Draw( sb, rect, 2f );
			}
		}


		public void DrawSceneGame( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;
			SceneContext ctx = mymod.Logic.GetCurrentContext( SceneLayer.Game );

			var scene = mymod.Logic.GetScene( ctx );
			if( scene == null ) {
				return;
			}

			foreach( Rectangle rect in this.GetOffsetsGame(scene) ) {
				if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
					continue;
				}
				if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
					continue;
				}

				scene.Draw( sb, rect, 1f );
			}
		}
	}
}
