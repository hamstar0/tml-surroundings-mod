using HamstarHelpers.Services.AnimatedTexture;
using Microsoft.Xna.Framework;
using System;
using Terraria;


namespace Surroundings.Scenes.Components.FlameSpurt {
	public class FlameSpurtAnimator : Animator {
		public const string FlameTexturePath = "Scenes/Components/FlameSpurt/FlameSpurt";



		////////////////

		public override AnimatedTexture Animation { get; }

		////

		public override bool HasAbsoluteWidth => false;
		public override bool HasAbsoluteHeight => true;

		public override int WorldX { get; protected set; }
		public override int WorldY {
			get {
				return Main.screenHeight - (int)((float)this.Height * this.Scale.Y);
			}
			protected set { }
		}

		public override Vector2 Scale { get; }



		////////////////

		public FlameSpurtAnimator( int worldX, Vector2 scale ) {
			this.WorldX = worldX;
			this.Scale = scale;
			this.Animation = AnimatedTexture.Create(
				frames: SurroundingsMod.Instance.GetTexture( FlameSpurtAnimator.FlameTexturePath ),
				maxFrames: 12,
				animator: this.MyFrameAnimator
			);
		}


		////////////////

		private (int NextFrame, int TickDelay) MyFrameAnimator( AnimatedTexture anim ) {
			if( !this.IsActive ) {
				return (0, 0);
			}

			int nextFrame = ( anim.CurrentFrame + 1 );

			if( nextFrame >= anim.MaxFrames ) {
				nextFrame = 0;
				this.IsActive = false;
			}

			return (nextFrame, 2);
		}
	}
}
