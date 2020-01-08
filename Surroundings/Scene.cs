using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria;


namespace Surroundings {
	public abstract class Scene {
		public static float GetSurfaceOpacity( SceneDrawData drawData, float minimumPercent = 0.6f ) {
			float remainingPercent = 1f - minimumPercent;
			float remainingScale = 1f / remainingPercent;

			float occludedPercent = drawData.WallPercent + (drawData.CavePercent - drawData.CaveAndWallPercent);
			float relevantOcclusionPercent = Math.Max(occludedPercent - minimumPercent, 0f) * remainingScale;
			float relevantNonOcclusionPercent = 1f - relevantOcclusionPercent;
			return relevantNonOcclusionPercent;
		}



		////////////////

		protected Rectangle RecentDrawnFrameInWorld = new Rectangle();


		////////////////
		
		public abstract SceneContext Context { get; }

		////

		public abstract int DrawPriority { get; }

		////

		public abstract Vector2 FrameSize { get; }
		public abstract float HorizontalTileScrollRate { get; }
		public abstract float VerticalTileScrollRate { get; }



		////////////////

		public abstract Color GetSceneColor( SceneDrawData drawData );

		public abstract float GetSceneOpacity( SceneDrawData drawData );


		////////////////

		public virtual void Update() { }


		////////////////

		internal void DrawBase(
				SpriteBatch sb,
				Rectangle screenFrame,
				SceneDrawData drawData,
				float drawDepth ) {
			this.RecentDrawnFrameInWorld = screenFrame;
			this.RecentDrawnFrameInWorld.X += (int)Main.screenPosition.X;
			this.RecentDrawnFrameInWorld.Y += (int)Main.screenPosition.Y;

			this.Draw( sb, screenFrame, drawData, drawDepth );
		}

		public abstract void Draw(
			SpriteBatch sb,
			Rectangle screenFrame,
			SceneDrawData drawData,
			float drawDepth
		);
	}
}
