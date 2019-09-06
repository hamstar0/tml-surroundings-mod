using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;


namespace Surroundings {
	public abstract class Scene {
		public abstract bool CanHorizontalScroll( SceneLayer layer );
		public abstract bool CanVerticalScroll( SceneLayer layer );

		////

		public abstract SceneContext GetContext();

		public abstract void Draw( SpriteBatch sb, Rectangle destination );
	}
}
