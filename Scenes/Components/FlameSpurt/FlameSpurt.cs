using HamstarHelpers.Services.AnimatedTexture;
using Microsoft.Xna.Framework;
using System;
using Terraria;


namespace Surroundings.Scenes.Components.FlameSpurt {
	public class FlameSpurt : Animator {
		public const string FlameTexturePath = "Scenes/Components/FlameSpurt/FlameSpurt";



		////////////////

		public override AnimatedTexture Animation { get; }

		////

		public override int WorldX { get; protected set; }
		public override int WorldY {
			get {
				return Main.screenHeight - ( this.Animation.FramesTexture.Height / this.Animation.MaxFrames );
			}
			protected set {
			}
		}

		public override Vector2 Scale { get; }



		////////////////

		public FlameSpurt( int worldX, Vector2 scale ) {
			this.WorldX = worldX;
			this.Scale = scale;
			this.Animation = AnimatedTexture.Create(
				SurroundingsMod.Instance.GetTexture( FlameSpurt.FlameTexturePath ),
				12,
				this.MyFrameAnimator
			);
		}


		////////////////

		private (int NextFrame, int TickDelay) MyFrameAnimator( AnimatedTexture anim ) {
			if( this.HasCompleted ) {
				return (0, 0);
			}

			int nextFrame = ( anim.CurrentFrame + 1 );

			if( nextFrame >= anim.MaxFrames ) {
				nextFrame = 0;
				this.HasCompleted = true;
			}

			return (nextFrame, 8);
		}
	}
}
