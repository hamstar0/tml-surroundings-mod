using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.HUD;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;


namespace Surroundings.Scenes.Components.Mists {
	public partial class Mist {
		public Texture2D CloudTex;
		public Vector2 WorldPosition;
		public Vector2 Velocity;
		
		public float AnimationFadeTickRate;
		public float AnimationPeekTickRate;

		public float AnimationPercentTimesThree = 0f;

		public Vector2 Scale = Vector2.One;


		////////////////

		public bool IsActive { get; private set; } = true;



		////////////////

		public Mist( Vector2 worldCenterPosition,
				Vector2 windVelocity,
				float animationFadeTickRate,
				float animationPeekTickRate ) {
			this.CloudTex = Mist.GetRandomCloudTexture();
			this.WorldPosition = worldCenterPosition;
			this.Velocity = windVelocity;
			this.AnimationFadeTickRate = animationFadeTickRate;
			this.AnimationPeekTickRate = animationPeekTickRate;

			this.WorldPosition.X -= (float)this.CloudTex.Width * this.Scale.X;
			this.WorldPosition.Y -= (float)this.CloudTex.Height * this.Scale.Y;
		}


		////////////////

		public void Update() {
			if( !this.IsActive ) { return; }

			this.WorldPosition += this.Velocity;

			if( this.IsActive ) {
				if( this.AnimationPercentTimesThree < 1f || this.AnimationPercentTimesThree > 2f ) {
					this.AnimationPercentTimesThree += this.AnimationFadeTickRate;
				} else {
					this.AnimationPercentTimesThree += this.AnimationPeekTickRate;
				}
			}

			this.IsActive = this.AnimationPercentTimesThree < 3f;
		}


		////////////////

		public void Draw( SpriteBatch sb, Color color ) {
			if( !this.IsActive ) { return; }
			if( this.CloudTex != null ) { return; }

			var mymod = SurroundingsMod.Instance;
			Vector2 pos = this.WorldPosition - Main.screenPosition;

			float animPercent = this.AnimationPercentTimesThree / 3f;
			float invDimHalf = Math.Abs( 0.5f - animPercent );
			float dim = 1f - (invDimHalf * 2f);

			color.R = (byte)((float)color.R * dim);
			color.G = (byte)((float)color.G * dim);
			color.B = (byte)((float)color.B * dim);
			color.A = (byte)((float)color.A * dim);

			sb.Draw( this.CloudTex, pos, null, color, 0f, Vector2.Zero, this.Scale, SpriteEffects.None, 0f );

			if( mymod.Config.DebugModeInfo ) {
				int wid = (int)((float)this.CloudTex.Width * this.Scale.X);
				int hei = (int)((float)this.CloudTex.Height * this.Scale.Y);

				HUDHelpers.DrawBorderedRect(
					sb,
					Color.Transparent,
					Color.White * 0.25f,
					new Rectangle( (int)pos.X, (int)pos.Y, wid, hei ),
					2
				);
			}
		}
	}
}
