﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using HamstarHelpers.Helpers.World;


namespace Surroundings {
	public class SceneDrawData {
		public static SceneDrawData GetEnvironmentData( Vector2 center ) {
			Vector2 brightnessCheckPoint = center;
			int brightnessCheckTileX = (int)( brightnessCheckPoint.X * 0.0625f );
			int brightnessCheckTileY = (int)( brightnessCheckPoint.Y * 0.0625f );
			int minX = brightnessCheckTileX - 32;
			int minY = brightnessCheckTileY - 24;
			int maxX = brightnessCheckTileX + 32;
			int maxY = brightnessCheckTileY + 24;

			float totalBrightness = 0;
			float caveAndWall = 0;
			float cave = 0;
			float wall = 0;

			for( int x = minX; x < maxX; x++ ) {
				for( int y = minY; y < maxY; y++ ) {
					totalBrightness += Lighting.Brightness( x, y );

					if( y >= WorldHelpers.DirtLayerTopTileY ) {
						cave += 1;
					}

					Tile tile = Framing.GetTileSafely( x, y );
					if( tile.wall != 0 ) {
						wall += 1;
					}

					if( y >= WorldHelpers.DirtLayerTopTileY && tile.wall != 0 ) {
						caveAndWall += 1;
					}
				}
			}

			int total = ( maxX - minX ) * ( maxY - minY );

			return new SceneDrawData(
				center: center,
				brightness: totalBrightness / total,
				wallPercent: wall / total,
				cavePercent: cave / total,
				caveAndWallPercent: caveAndWall / total
			);
		}



		////////////////

		public Vector2 Center { get; }
		public float Brightness { get; }
		public float WallPercent { get; }
		public float CavePercent { get; }
		public float CaveAndWallPercent { get; }
		public float Opacity { get; set; }



		////////////////

		public SceneDrawData( Vector2 center,
				float brightness,
				float wallPercent,
				float cavePercent,
				float caveAndWallPercent ) {
			this.Center = center;
			this.Brightness = brightness;
			this.WallPercent = wallPercent;
			this.CavePercent = cavePercent;
			this.CaveAndWallPercent = caveAndWallPercent;
		}
	}
}
