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

			IEnumerable<Rectangle> offset = this.GetOffsetScreen();

			scene.Draw( sb, offset.First() );
		}


		public void DrawSceneNear( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;
			SceneContext ctx = mymod.Logic.GetCurrentContext( SceneLayer.Near );

			var scene = mymod.Logic.GetScene( ctx );
			if( scene == null ) {
				return;
			}

			foreach( Rectangle offset in this.GetOffsetNear(scene) ) {
				scene.Draw( sb, offset );
			}
		}


		public void DrawSceneFar( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;
			SceneContext ctx = mymod.Logic.GetCurrentContext( SceneLayer.Far );

			var scene = mymod.Logic.GetScene( ctx );
			if( scene == null ) {
				return;
			}

			foreach( Rectangle offset in this.GetOffsetFar(scene) ) {
				scene.Draw( sb, offset );
			}
		}


		public void DrawSceneGame( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;
			SceneContext ctx = mymod.Logic.GetCurrentContext( SceneLayer.Game );

			var scene = mymod.Logic.GetScene( ctx );
			if( scene == null ) {
				return;
			}

			foreach( Rectangle offset in this.GetOffsetGame(scene) ) {
				scene.Draw( sb, offset );
			}
		}


		////////////////

		private IEnumerable<Rectangle> GetOffsetScreen() {
			yield return new Rectangle( 0, 0, Main.screenWidth, Main.screenHeight );
		}


		private IEnumerable<Rectangle> GetOffsetNear( Scene scene ) {
			Vector2 pos = Main.LocalPlayer.Center;
			int wid = Main.screenWidth;
			int hei = Main.screenHeight;
			int x = 0, y = 0;

			if( scene.CanHorizontalScroll(SceneLayer.Near) ) {
				x = ( (int)pos.X % ( wid / 4 ) ) * 4;

				yield return new Rectangle( x, y, wid, hei );
				yield return new Rectangle( x - wid, y, wid, hei );
			}
			
			if( scene.CanVerticalScroll(SceneLayer.Near) ) {
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
