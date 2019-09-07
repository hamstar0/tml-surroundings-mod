using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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

			Rectangle rect = this.GetOffsetScreen();

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

			foreach( Rectangle rect in this.GetOffsetsFar(scene) ) {
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

			foreach( Rectangle rect in this.GetOffsetsGame(scene) ) {
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

		private Rectangle GetOffsetScreen() {
			return new Rectangle( 0, 0, Main.screenWidth, Main.screenHeight );
		}


		private IEnumerable<Rectangle> GetOffsetsNear( Scene scene ) {
			return this.GetOffsetAtScale( 4, scene.Scale, scene.CanHorizontalTile, scene.CanVerticalTile );
		}


		private IEnumerable<Rectangle> GetOffsetsFar( Scene scene ) {
			return this.GetOffsetAtScale( 2, scene.Scale, scene.CanHorizontalTile, scene.CanVerticalTile );
		}


		private IEnumerable<Rectangle> GetOffsetsGame( Scene scene ) {
			return this.GetOffsetAtScale( 1, scene.Scale, scene.CanHorizontalTile, scene.CanVerticalTile );
		}


		////////////////

		private IEnumerable<Rectangle> GetOffsetAtScale( int zoom, Vector2 scale, bool canHorizTile, bool canVertTile ) {
			Vector2 pos = Main.LocalPlayer.Center;
			int wid = (int)( (float)Main.screenWidth * scale.X );
			int hei = (int)( (float)Main.screenHeight * scale.Y );
			int x = 0, y = 0;

			if( canHorizTile ) {
				x = ( (int)pos.X % ( wid / zoom ) ) * zoom;

				yield return new Rectangle( x, y, wid, hei );
				yield return new Rectangle( x - wid, y, wid, hei );
			}

			if( canVertTile ) {
				y = ( (int)pos.Y % ( hei / zoom ) ) * zoom;

				yield return new Rectangle( x, y - hei, wid, hei );
				yield return new Rectangle( x - wid, y - hei, wid, hei );
			}
		}
	}
}
