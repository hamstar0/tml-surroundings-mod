using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.AnimatedTexture;
using Microsoft.Xna.Framework;
using Terraria;


namespace Surroundings.Scenes.Components.Fireflies {
	public class Firefly {
		public AnimatedTexture Animation;
		public Vector2 ScreenPosition;
		public Vector2 Velocity;
		public int Acceleration;



		////////////////

		public void Update() {
			if( !this.ClampToScreen() ) {
				this.PropelFlies();
			} else {
				this.Acceleration = 0;
			}

			this.ScreenPosition += this.Velocity;
		}


		////////////////

		private void PropelFlies() {
			this.Acceleration--;

			if( this.Acceleration > 0 ) {
				if( this.Velocity.LengthSquared() < 16f ) {
					this.Velocity *= 1.01f;
				}
			} else {
				this.Velocity *= 0.99f;

				if( this.Velocity.LengthSquared() < 1f ) {
					this.Acceleration = Main.rand.Next( 60 * 2, 60 * 10 );

					this.Velocity.X += ( Main.rand.NextFloat() * 2f ) - 1f;
					this.Velocity.Y += ( Main.rand.NextFloat() * 2f ) - 1f;
				}
			}
		}


		////

		private bool ClampToScreen() {
			bool isOoB = false;

			if( this.ScreenPosition.X < 0 ) {
				if( this.Velocity.X < 0 ) {
					this.Velocity.X *= 0.98f;
				}
				this.Velocity.X += 0.03f;
				isOoB = true;
			} else if( this.ScreenPosition.X >= Main.screenWidth ) {
				if( this.Velocity.X > 0 ) {
					this.Velocity.X *= 0.98f;
				}
				this.Velocity.X -= 0.03f;
				isOoB = true;
			}

			if( this.ScreenPosition.Y < 0 ) {
				if( this.Velocity.Y < 0 ) {
					this.Velocity.Y *= 0.98f;
				}
				this.Velocity.Y += 0.03f;
				isOoB = true;
			} else if( this.ScreenPosition.Y >= Main.screenHeight ) {
				if( this.Velocity.Y > 0 ) {
					this.Velocity.Y *= 0.98f;
				}
				this.Velocity.Y -= 0.03f;
				isOoB = true;
			}

			return isOoB;
		}
	}
}
