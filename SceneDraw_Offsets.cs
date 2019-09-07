using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;


namespace Surroundings {
	public partial class SceneDraw {
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
