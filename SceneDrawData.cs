using Microsoft.Xna.Framework;
using System;
using Terraria;
using HamstarHelpers.Helpers.World;


namespace Surroundings {
	public class SceneDrawData {
		public static SceneDrawData GetEnvironmentData( Vector2 center ) {
			Vector2 brightnessCheckPoint = center;
			int brightnessCheckTileX = (int)( brightnessCheckPoint.X * 0.0625f );
			int brightnessCheckTileY = (int)( brightnessCheckPoint.Y * 0.0625f );
			int minX = brightnessCheckTileX - 16;
			int minY = brightnessCheckTileY - 12;
			int maxX = brightnessCheckTileX + 32;
			int maxY = brightnessCheckTileY + 24;

			float totalBrightness = 0;
			float occluded = 0;
			float cave = 0;
			float wall = 0;

			for( int x = minX; x < maxX; x++ ) {
				for( int y = minY; y < maxY; y++ ) {
					totalBrightness += Lighting.Brightness( x, y );
					occluded += 1;

					if( y >= WorldHelpers.DirtLayerTopTileY ) {
						cave += 1;
					} else {
						Tile tile = Framing.GetTileSafely( x, y );
						wall += tile.wall != 0 ? 1 : 0;
					}
				}
			}

			int total = ( maxX - minX ) * ( maxY - minY );
			float brightness = totalBrightness / total;
			float wallPercent = wall / total;
			float cavePercent = cave / total;
			float occludedPercent = occluded / total;

			return new SceneDrawData( center, brightness, wallPercent, cavePercent, occludedPercent );
		}



		////////////////

		public Vector2 Center { get; }
		public float Brightness { get; }
		public float WallPercent { get; }
		public float CavePercent { get; }
		public float OccludedPercent { get; }
		public float Opacity { get; set; }



		////////////////

		public SceneDrawData( Vector2 center,
				float brightness,
				float wallPercent,
				float cavePercent,
				float occludedPercent ) {
			this.Center = center;
			this.Brightness = brightness;
			this.WallPercent = wallPercent;
			this.CavePercent = cavePercent;
			this.OccludedPercent = occludedPercent;
		}
	}
}
