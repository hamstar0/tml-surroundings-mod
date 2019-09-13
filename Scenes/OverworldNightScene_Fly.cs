using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.AnimatedTexture;
using Microsoft.Xna.Framework;
using Terraria;


namespace Surroundings.Scenes {
	class Firefly {
		public AnimatedTexture Animation;
		public Vector2 ScrPos;
		public Vector2 Vel;
		public int Accel;
	}




	public partial class SurfaceForestNightScene : Scene {
		private void AnimateFlyMovement() {
			int count = this.Flies.Count;

			for( int i=0; i<count; i++ ) {
				var fly = this.Flies[i];

				if( !this.ClampToScreen( fly ) ) {
					this.PropelFlies( fly );
				} else {
					fly.Accel = 0;
				}

				fly.ScrPos += fly.Vel;
			}
		}


		////

		private void PropelFlies( Firefly fly ) {
			fly.Accel--;

			if( fly.Accel > 0 ) {
				if( fly.Vel.LengthSquared() < 16f ) {
					fly.Vel *= 1.01f;
				}
			} else {
				fly.Vel *= 0.99f;

				if( fly.Vel.LengthSquared() < 1f ) {
					fly.Accel = Main.rand.Next( 60 * 2, 60 * 10 );

					fly.Vel.X += ( Main.rand.NextFloat() * 2f ) - 1f;
					fly.Vel.Y += ( Main.rand.NextFloat() * 2f ) - 1f;
				}
			}
		}


		////

		private bool ClampToScreen( Firefly fly ) {
			bool isOoB = false;

			if( fly.ScrPos.X < 0 ) {
				if( fly.Vel.X < 0 ) {
					fly.Vel.X *= 0.98f;
				}
				fly.Vel.X += 0.03f;
				isOoB = true;
			} else if( fly.ScrPos.X >= Main.screenWidth ) {
				if( fly.Vel.X > 0 ) {
					fly.Vel.X *= 0.98f;
				}
				fly.Vel.X -= 0.03f;
				isOoB = true;
			}

			if( fly.ScrPos.Y < 0 ) {
				if( fly.Vel.Y < 0 ) {
					fly.Vel.Y *= 0.98f;
				}
				fly.Vel.Y += 0.03f;
				isOoB = true;
			} else if( fly.ScrPos.Y >= Main.screenHeight ) {
				if( fly.Vel.Y > 0 ) {
					fly.Vel.Y *= 0.98f;
				}
				fly.Vel.Y -= 0.03f;
				isOoB = true;
			}

			return isOoB;
		}
	}
}
