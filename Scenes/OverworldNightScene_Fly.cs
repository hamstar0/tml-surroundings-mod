using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Terraria;


namespace Surroundings.Scenes {
	public partial class OverworldNightScene : Scene {
		private void AnimateFlies() {
			this.PropelFlies( ref this.Fly1Vel, ref this.Fly1Accel );
			this.PropelFlies( ref this.Fly2Vel, ref this.Fly2Accel );
			this.PropelFlies( ref this.Fly3Vel, ref this.Fly3Accel );
			this.PropelFlies( ref this.Fly4Vel, ref this.Fly4Accel );

			this.Fly1Pos += this.Fly1Vel;
			this.Fly2Pos += this.Fly2Vel;
			this.Fly3Pos += this.Fly3Vel;
			this.Fly4Pos += this.Fly4Vel;
		}


		private void PropelFlies( ref Vector2 velocity, ref int accel ) {
			if( accel > 0 ) {
				velocity *= 1.01f;
			} else {
				velocity *= 0.99f;
			}

			if( velocity.LengthSquared() < 1f && accel-- <= 0 ) {
				accel = Main.rand.Next( 60, 60 * 8 );

				velocity.X += ( Main.rand.NextFloat() * 0.02f ) - 0.01f;
				velocity.Y += ( Main.rand.NextFloat() * 0.02f ) - 0.01f;
			}
		}
	}
}
