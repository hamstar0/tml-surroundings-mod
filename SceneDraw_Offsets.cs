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
			return this.GetOffsetAtScale( 4, scene.Scale, scene.HorizontalTileScrollRate, scene.VerticalTileScrollRate );
		}


		private IEnumerable<Rectangle> GetOffsetsFar( Scene scene ) {
			return this.GetOffsetAtScale( 2, scene.Scale, scene.HorizontalTileScrollRate, scene.VerticalTileScrollRate );
		}


		private IEnumerable<Rectangle> GetOffsetsGame( Scene scene ) {
			return this.GetOffsetAtScale( 1, scene.Scale, scene.HorizontalTileScrollRate, scene.VerticalTileScrollRate );
		}


		////////////////

		private IEnumerable<Rectangle> GetOffsetAtScale( int zoom, Vector2 scale, float horizTileRate, float vertTileRate ) {
			Vector2 pos = Main.LocalPlayer.Center;
			int wid = (int)( (float)Main.screenWidth * scale.X );
			int hei = (int)( (float)Main.screenHeight * scale.Y );
			float x = 0, y = 0;

			if( horizTileRate != 0 ) {
				x = pos.X % ((float)wid / horizTileRate);
				x *= horizTileRate;

				yield return new Rectangle( (int)x, (int)y, wid, hei );
				yield return new Rectangle( (int)x - wid, (int)y, wid, hei );
			}

			if( vertTileRate != 0 ) {
				y = pos.Y % ( hei / zoom );
				y *= vertTileRate;

				yield return new Rectangle( (int)x, (int)y - hei, wid, hei );
				yield return new Rectangle( (int)x - wid, (int)y - hei, wid, hei );
			}
		}
	}
}
