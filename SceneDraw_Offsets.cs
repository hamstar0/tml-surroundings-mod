using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;


namespace Surroundings {
	public partial class SceneDraw {
		private Rectangle GetOffsetScreen() {
			return new Rectangle( 0, 0, 0, 0 );
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

		private IEnumerable<Rectangle> GetOffsetAtScale(
				float zoom,
				Vector2 scale,
				float horizTileRate,
				float vertTileRate ) {
			Vector2 pos = Main.LocalPlayer.Center;
			int wid = (int)((float)Main.screenWidth * scale.X);
			int hei = (int)((float)Main.screenHeight * scale.Y);
			int x = 0, y = 0;

			if( horizTileRate != 0 ) {
				x = (int)(pos.X % ((float)wid / horizTileRate));
				x *= (int)(horizTileRate);

				yield return new Rectangle( x, y, wid, hei );
				yield return new Rectangle( x - wid, (int)y, wid, hei );
			}

			if( vertTileRate != 0 ) {
				y = (int)(pos.Y % ( hei / zoom ));
				y *= (int)(vertTileRate);

				yield return new Rectangle( x, y - hei, wid, hei );
				yield return new Rectangle( x - wid, y - hei, wid, hei );
			}
		}
	}
}
