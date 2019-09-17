using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;


namespace Surroundings {
	public partial class SceneDraw {
		private Rectangle GetFrameOfScreenLayer() {
			return new Rectangle( 0, 0, Main.screenWidth, Main.screenHeight );
		}


		private IEnumerable<Rectangle> GetFramesOfNearLayer( Vector2 worldCenter, Scene scene ) {
			return this.GetFrames( worldCenter,
				scene.FrameSize,
				scene.HorizontalTileScrollRate,
				scene.VerticalTileScrollRate
			);
		}


		private IEnumerable<Rectangle> GetFramesOfFarLayer( Vector2 worldCenter, Scene scene ) {
			return this.GetFrames( worldCenter,
				scene.FrameSize,
				scene.HorizontalTileScrollRate,
				scene.VerticalTileScrollRate
			);
		}


		private IEnumerable<Rectangle> GetFramesOfGameLayer( Vector2 worldCenter, Scene scene ) {
			return this.GetFrames( worldCenter,
				scene.FrameSize,
				scene.HorizontalTileScrollRate,
				scene.VerticalTileScrollRate
			);
		}


		////////////////

		private IEnumerable<Rectangle> GetFrames(
				Vector2 worldCenter,
				Vector2 frameSize,
				float horizTileRate,
				float vertTileRate ) {
			float maxWid = (float)Main.screenWidth;
			float maxHei = (float)Main.screenHeight;

			if( horizTileRate != 0 && vertTileRate != 0 ) {
				foreach( float x in this.GetFrameOffsets(worldCenter.X, frameSize.X, maxWid, horizTileRate) ) {
					foreach( float y in this.GetFrameOffsets(worldCenter.Y, frameSize.Y, maxHei, vertTileRate) ) {
						yield return new Rectangle( (int)x, (int)y, (int)frameSize.X, (int)frameSize.Y );
					}
				}
			} else if( horizTileRate != 0 && vertTileRate == 0 ) {
				foreach( float x in this.GetFrameOffsets(worldCenter.X, frameSize.X, maxWid, horizTileRate ) ) {
					yield return new Rectangle( (int)x, 0, (int)frameSize.X, (int)frameSize.Y );
				}
			} else if( horizTileRate == 0 && vertTileRate != 0 ) {
				foreach( float y in this.GetFrameOffsets(worldCenter.Y, frameSize.Y, maxHei, vertTileRate) ) {
					yield return new Rectangle( 0, (int)y, (int)frameSize.X, (int)frameSize.Y );
				}
			} else {
				yield return new Rectangle( 0, 0, (int)frameSize.X, (int)frameSize.Y );
			}
		}

		////

		private IEnumerable<float> GetFrameOffsets(
				float frameCenter,
				float frameSize,
				float maxSize,
				float tileRate ) {
			float tileWid = frameSize / tileRate;
			float tileSpan = frameCenter % tileWid;
			float tilePercent = tileSpan / tileWid;

			float frameLeft = frameSize * -0.5f;
			float leftmost = frameLeft + ( tilePercent * frameSize );
			leftmost -= frameSize;

			for( float x = leftmost; x < maxSize; x += frameSize ) {
				yield return x;
			}
		}
	}
}
