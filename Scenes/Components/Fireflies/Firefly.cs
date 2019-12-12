using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.AnimatedTexture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Components.Fireflies {
	public class Firefly {
		public AnimatedTexture Animation { get; }
		public Vector2 ScreenPosition;
		public Vector2 Velocity;
		public int Acceleration;



		////////////////

		public Firefly( AnimatedTexture animation, Vector2 screenPosition, Vector2 velocity, int acceleration ) {
			this.Animation = animation;
			this.ScreenPosition = screenPosition;
			this.Velocity = velocity;
			this.Acceleration = acceleration;
		}


		////////////////
		
		public bool IsGlowing() {
			return this.Animation.CurrentFrame <= 1;
		}

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


		////////////////

		public void Draw( SpriteBatch sb, Vector2 position, Color color, float opacity, Vector2 origin ) {
			if( opacity == 0f ) { return; }

			Color flyColor = this.IsGlowing() ? Color.Yellow : color;
			flyColor *= opacity;

			if( this.IsGlowing() ) {
				Main.instance.LoadProjectile( 540 );
				Texture2D tex = Main.projectileTexture[ 540 ];

				Color glowColor = Color.Yellow * 0.1f;
				glowColor *= opacity;

				sb.Draw(
					texture: tex,
					position: position,
					sourceRectangle: null,
					color: glowColor,
					rotation: 0f,
					origin: new Vector2( (tex.Width/2) - 6, (tex.Height/2) - 10 ),
					scale: 1f,
					effects: SpriteEffects.None,
					layerDepth: 1f 
				);
			}

//DebugHelpers.Print("fly", "Frame: "+fly.Animation.CurrentFrame+" | "+fly.Animation.CurrentFrameTicksElapsed, 20);
			this.Animation.Draw( sb, position, flyColor, 0f, null, origin );
		}
	}
}
