using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;


namespace Surroundings {
	public abstract class Scene {
		public abstract int Priority { get; }

		public abstract Vector2 Scale { get; }
		public abstract float HorizontalTileScrollRate { get; }
		public abstract float VerticalTileScrollRate { get; }

		public abstract SceneContext GetContext { get; }



		////////////////

		public abstract void Draw( SpriteBatch sb, Rectangle destination, float depth );
	}
}
