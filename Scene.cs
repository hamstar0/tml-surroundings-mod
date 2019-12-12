using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria;


namespace Surroundings {
	public abstract class Scene {
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
