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
			return this.GetOffsetAtScale( scene.Scale, scene.HorizontalTileScrollRate, scene.VerticalTileScrollRate );
		}


		private IEnumerable<Rectangle> GetOffsetsFar( Scene scene ) {
			return this.GetOffsetAtScale( scene.Scale, scene.HorizontalTileScrollRate, scene.VerticalTileScrollRate );
		}


		private IEnumerable<Rectangle> GetOffsetsGame( Scene scene ) {
			return this.GetOffsetAtScale( scene.Scale, scene.HorizontalTileScrollRate, scene.VerticalTileScrollRate );
		}


		////////////////

		private IEnumerable<Rectangle> GetOffsetAtScale(
				Vector2 scale,
				float horizTileRate,
				float vertTileRate ) {
			Vector2 pos = Main.LocalPlayer.Center;
			int wid = (int)((float)Main.screenWidth * scale.X);
			int hei = (int)((float)Main.screenHeight * scale.Y);
			int x = 0, y = 0;

			if( horizTileRate != 0 ) {
				x = wid - (int)( pos.X % ( (float)wid / horizTileRate ) );
				x = (int)( (float)x * horizTileRate );

				yield return new Rectangle( x - wid, y, wid, hei );
				yield return new Rectangle( (x - wid) - wid, (int)y, wid, hei );
			}
			
			if( vertTileRate != 0 ) {
				y = hei - (int)( pos.Y % ( (float)hei / vertTileRate ) );
				y = (int)( (float)y * vertTileRate );

				yield return new Rectangle( x - wid, y - hei, wid, hei );
				yield return new Rectangle( (x - wid) - wid, y - hei, wid, hei );
			}
		}
	}
}
