using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;


namespace Surroundings.Scenes.Components {
	public partial class MistDefinition {
		public static MistDefinition Create( Rectangle worldArea, float animationRate ) {
			int x, y;
			Vector2 pos;

			do {
				x = Main.rand.Next( worldArea.X, worldArea.X + worldArea.Width );
				y = Main.rand.Next( worldArea.Y, worldArea.Y + worldArea.Height );
				pos = new Vector2( x, y );

				WorldHelpers.DropToGround( pos, false, TilePattern.CommonSolid, out pos );
				x = (int)pos.X;
				y = (int)pos.Y;
			} while( !worldArea.Contains( x, y ) );

			var mistDef = new MistDefinition( pos, MistDefinition.GetWindDrift(), animationRate );

			mistDef.WorldPosition = new Vector2( x, y );

			return mistDef;
		}



		////////////////

		public Texture2D CloudTex;
		public Vector2 WorldPosition;
		public Vector2 Velocity;

		public float AnimationRate;
		public float AnimationPercent = 0f;



		////////////////

		public MistDefinition( Vector2 worldPos, Vector2 windVelocity, float animationRate=1f ) {
			this.CloudTex = MistDefinition.GetRandomCloudTexture();
			this.WorldPosition = worldPos;
			this.Velocity = windVelocity;
			this.AnimationRate = animationRate;
		}


		////////////////

		public void Update() {
			this.WorldPosition += this.Velocity;
			this.AnimationPercent += (1f / 60f) * this.AnimationRate;
		}


		public void Draw( SpriteBatch sb, Color color ) {
			Vector2 pos = this.WorldPosition - Main.screenPosition;

			float dim = 1f - (Math.Abs( 0.5f - this.AnimationPercent ) * 2f);

			color *= dim;

			sb.Draw( this.CloudTex, pos, color );
		}
	}
}
