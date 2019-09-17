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

		public float AnimationPercent = 0f;

		public Vector2 Scale = Vector2.One;


		////////////////

		public bool IsActive { get; private set; } = true;



		////////////////

		public Mist( Vector2 worldPos,
				Vector2 windVelocity,
				float animationFadeTickRate,
				float animationPeekTickRate ) {
			this.CloudTex = Mist.GetRandomCloudTexture();
			this.WorldPosition = worldPos;
			this.Velocity = windVelocity;
			this.AnimationFadeTickRate = animationFadeTickRate;
			this.AnimationPeekTickRate = animationPeekTickRate;
		}


		////////////////

		public void Update() {
			if( !this.IsActive ) { return; }

			this.WorldPosition += this.Velocity;

			if( this.IsActive ) {
				if( this.AnimationPercent < (2f/5f) || this.AnimationPercent > (3f/5f) ) {
					this.AnimationPercent += ( 1f / 60f ) / this.AnimationFadeTickRate;
				} else {
					this.AnimationPercent += ( 1f / 60f ) / this.AnimationPeekTickRate;
				}
			}

			this.IsActive = this.AnimationPercent < 1f;
		}


		////////////////

		public void Draw( SpriteBatch sb, Color color ) {
			if( !this.IsActive ) { return; }

			var mymod = SurroundingsMod.Instance;
			Vector2 pos = this.WorldPosition - Main.screenPosition;

			float dim = 1f - (Math.Abs( 0.5f - this.AnimationPercent ) * 2f);

			color *= dim;

			if( this.CloudTex != null ) {
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
}
