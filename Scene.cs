using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using HamstarHelpers.Helpers.World;
using Surroundings.Scenes.Components.Mists;


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

			float brightness = 0;
			float wallPercent = 0;

			for( int x = minX; x < maxX; x++ ) {
				for( int y = minY; y < maxY; y++ ) {
					if( y >= WorldHelpers.DirtLayerTop ) {
						brightness += Lighting.Brightness( x, y );
						wallPercent += 1;
						continue;
					}

					Tile tile = Framing.GetTileSafely( x, y );

					brightness += Lighting.Brightness( x, y );
					wallPercent += tile.wall != 0 ? 1 : 0;
				}
			}

			int total = ( maxX - minX ) * ( maxY - minY );
			brightness /= total;
			wallPercent /= total;

			return new SceneDrawData( center, brightness, wallPercent );
		}



		////////////////

		public Vector2 Center { get; }
		public float Brightness { get; }
		public float WallPercent { get; }
		public float Opacity { get; set; }



		////////////////

		public SceneDrawData( Vector2 center, float brightness, float wallPercent ) {
			this.Center = center;
			this.Brightness = brightness;
			this.WallPercent = wallPercent;
		}
	}




	public abstract class Scene {
		protected Rectangle MostRecentDrawWorldRectangle = new Rectangle();


		////////////////

		public abstract SceneContext Context { get; }

		////

		public abstract int DrawPriority { get; }

		////

		public abstract Vector2 FrameSize { get; }
		public abstract float HorizontalTileScrollRate { get; }
		public abstract float VerticalTileScrollRate { get; }

		////

		public abstract MistSceneDefinition MistDefinition { get; }



		////////////////

		public abstract Color GetSceneColor( SceneDrawData drawData );


		////////////////

		public virtual void Update() { }


		////////////////

		internal void DrawBase(
				SpriteBatch sb,
				Rectangle destination,
				SceneDrawData drawData,
				float drawDepth ) {
			this.MostRecentDrawWorldRectangle = destination;
			this.MostRecentDrawWorldRectangle.X += (int)Main.screenPosition.X;
			this.MostRecentDrawWorldRectangle.Y += (int)Main.screenPosition.Y;

			this.Draw( sb, destination, drawData, drawDepth );
		}

		public abstract void Draw(
			SpriteBatch sb,
			Rectangle destination,
			SceneDrawData drawData,
			float drawDepth
		);
	}
}
