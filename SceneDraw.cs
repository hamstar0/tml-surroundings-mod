using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;


namespace Surroundings {
	public class SceneDraw {
		public void DrawSceneScreen( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;
			SceneContext ctx = mymod.Logic.GetCurrentContext( SceneLayer.Screen );

			var scene = mymod.Logic.GetScene( ctx );
			if( scene == null ) {
				return;
			}

			IEnumerable<Rectangle> rects = this.GetOffsetScreen();
			Rectangle rect = rects.First();

			if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
				return;
			}
			if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
				return;
			}

			scene.Draw( sb, rect );
		}


		public void DrawSceneNear( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;
			SceneContext ctx = mymod.Logic.GetCurrentContext( SceneLayer.Near );

			var scene = mymod.Logic.GetScene( ctx );
			if( scene == null ) {
				return;
			}

			foreach( Rectangle rect in this.GetOffsetNear(scene) ) {
				if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
					continue;
				}
				if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
					continue;
				}

				scene.Draw( sb, rect );
			}
		}


		public void DrawSceneFar( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;
			SceneContext ctx = mymod.Logic.GetCurrentContext( SceneLayer.Far );

			var scene = mymod.Logic.GetScene( ctx );
			if( scene == null ) {
				return;
			}

			foreach( Rectangle rect in this.GetOffsetFar(scene) ) {
				if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
					continue;
				}
				if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
					continue;
				}

				scene.Draw( sb, rect );
			}
		}


		public void DrawSceneGame( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;
			SceneContext ctx = mymod.Logic.GetCurrentContext( SceneLayer.Game );

			var scene = mymod.Logic.GetScene( ctx );
			if( scene == null ) {
				return;
			}

			foreach( Rectangle rect in this.GetOffsetGame(scene) ) {
				if( rect.X > Main.screenWidth || ( rect.X + rect.Width ) < 0 ) {
					continue;
				}
				if( rect.Y > Main.screenHeight || ( rect.Y + rect.Height ) < 0 ) {
					continue;
				}

				scene.Draw( sb, rect );
			}
		}


		////////////////

		private IEnumerable<Rectangle> GetOffsetScreen() {
			yield return new Rectangle( 0, 0, Main.screenWidth, Main.screenHeight );
		}


		private IEnumerable<Rectangle> GetOffsetNear( Scene scene ) {
			Vector2 pos = Main.LocalPlayer.Center;
			Vector2 scale = scene.Scale;
			int wid = (int)((float)Main.screenWidth * scale.X);
			int hei = (int)((float)Main.screenHeight * scale.Y);
			int x = 0, y = 0;

			if( scene.CanHorizontalScroll ) {
				x = ( (int)pos.X % (wid / 4) ) * 4;

				yield return new Rectangle( x, y, wid, hei );
				yield return new Rectangle( x - wid, y, wid, hei );
			}
			
			if( scene.CanVerticalScroll ) {
				y = ( (int)pos.Y % ( hei / 4 ) ) * 4;

				yield return new Rectangle( x, y - hei, wid, hei );
				yield return new Rectangle( x - wid, y - hei, wid, hei );
			}
		}


		private IEnumerable<Rectangle> GetOffsetFar( Scene scene ) {
			yield return new Rectangle( 0, 0, Main.screenWidth, Main.screenHeight );
		}


		private IEnumerable<Rectangle> GetOffsetGame( Scene scene ) {
			yield return new Rectangle( 0, 0, Main.screenWidth, Main.screenHeight );
		}
	}
}
