using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.AnimatedTexture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;

namespace Surroundings.Scenes.Components {
	public abstract class Animator {
		public abstract AnimatedTexture Animation { get; }

		public abstract Vector2 Scale { get; }


		////

		public abstract int WorldX { get; }
		public abstract int WorldY { get; }


		////

		public int CenterWorldX => this.WorldX - (this.Width / 2);
		public int CenterWorldY => this.WorldY - (this.Height / 2);

		public int Width => this.Animation.FramesTexture.Width;
		public int Height => this.Animation.FramesTexture.Height / this.Animation.MaxFrames;



		////////////////

		public virtual void Draw( SpriteBatch sb, Color color ) {
			int wldX = this.CenterWorldX - (int)Main.screenPosition.X;
			wldX -= this.Width / 2;
			int wldY = Main.screenHeight - this.Height;

			this.Animation.Draw( sb, new Vector2(wldX, wldY), color, 0f, null, this.Scale );
		}
	}
}
