﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;


namespace Surroundings {
	public abstract class Scene {
		public abstract Vector2 Scale { get; }
		public abstract bool CanHorizontalScroll { get; }
		public abstract bool CanVerticalScroll { get; }

		public abstract SceneContext GetContext { get; }



		////////////////

		public abstract void Draw( SpriteBatch sb, Rectangle destination );
	}
}
