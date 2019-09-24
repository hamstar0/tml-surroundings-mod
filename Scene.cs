using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Terraria;


namespace Surroundings {
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
