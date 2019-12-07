using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Draw;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;


namespace Surroundings.Scenes.Components.Mists {
	public partial class Mist {
		public Texture2D CloudTex;
		public Vector2 WorldPosition;
		public Vector2 Velocity;
		
		public int AnimationFadeTickDuration;
		public int AnimationPeekTickDuration;

		public int AnimationFadeTicksElapsed = 0;
		public int AnimationPeekTicksElapsed = 0;

		public Vector2 Scale = Vector2.One;


		////////////////

		public bool IsActive { get; private set; } = true;



		////////////////

		public Mist( Vector2 worldCenterPosition,
				Vector2 windVelocity,
				int animationFadeTickDuration,
				int animationPeekTickDuration ) {
			this.CloudTex = Mist.GetRandomCloudTexture();
			this.WorldPosition = worldCenterPosition;
			this.Velocity = windVelocity;
			this.AnimationFadeTickDuration = animationFadeTickDuration;
			this.AnimationPeekTickDuration = animationPeekTickDuration;

			this.WorldPosition.X -= (float)this.CloudTex.Width * this.Scale.X * 0.5f;
			this.WorldPosition.Y -= (float)this.CloudTex.Height * this.Scale.Y * 0.5f;
		}


		////////////////

		public void Update() {
			if( !this.IsActive ) { return; }

			this.WorldPosition += this.Velocity;

			if( this.AnimationFadeTicksElapsed < this.AnimationFadeTickDuration && this.AnimationPeekTicksElapsed == 0 ) {
				this.AnimationFadeTicksElapsed++;
			} else if( this.AnimationPeekTicksElapsed < this.AnimationPeekTickDuration ) {
				this.AnimationPeekTicksElapsed++;
			} else if( this.AnimationFadeTicksElapsed > 1 ) {
				this.AnimationFadeTicksElapsed--;
			} else {
				this.AnimationFadeTicksElapsed = 0;
				this.IsActive = false;
			}
		}


		////////////////

		public void Draw( SpriteBatch sb, Color color ) {
			if( !this.IsActive ) { return; }
			if( this.CloudTex == null ) { return; }

			Vector2 scrPos = this.WorldPosition - Main.screenPosition;

			float fadePercent = (float)this.AnimationFadeTicksElapsed / (float)this.AnimationFadeTickDuration;
			float peekPercent = (float)this.AnimationPeekTicksElapsed / (float)this.AnimationPeekTickDuration;
			peekPercent = Math.Abs( 0.5f - peekPercent );
			peekPercent = 0.5f - peekPercent;
			peekPercent = 2f * peekPercent;
			float dim = (fadePercent + fadePercent + peekPercent) / 3f;

			color *= dim;

			sb.Draw( this.CloudTex, scrPos, null, color, 0f, Vector2.Zero, this.Scale, SpriteEffects.None, 0f );

			if( SurroundingsConfig.Instance.DebugModeMistInfo ) {
				int wid = (int)((float)this.CloudTex.Width * this.Scale.X);
				int hei = (int)((float)this.CloudTex.Height * this.Scale.Y);
				var scrRect = new Rectangle( (int)scrPos.X, (int)scrPos.Y, wid, hei );

				DrawHelpers.DrawBorderedRect( sb, null, (Color.White * 0.25f), scrRect, 2 );
			}
		}
	}
}
