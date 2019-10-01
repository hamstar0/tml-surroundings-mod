using System;
using System.Collections.Generic;
using System.Linq;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components;
using Terraria;


namespace Surroundings.Scenes.Contexts {
	public abstract class AnimationsScene : Scene {
		private (int WorldLeft, int WorldRight, int WorldTop, int WorldBottom) RecentAnimationsScanRange = (0, 0, 0, 0);

		private int TicksElapsedSinceLastAnimation = 0;


		////////////////

		public override int DrawPriority { get; } = 1;

		public override Vector2 FrameSize => new Vector2( Main.screenWidth, Main.screenHeight );

		public override float VerticalTileScrollRate { get; } = 0f;

		////

		protected IList<Animator> Animators { get; } = new List<Animator>();

		public abstract int NeededAnimationsQuantity { get; }

		public abstract int TickDurationBetweenNewAnimations { get; }



		////////////////

		public override Color GetSceneColor( SceneDrawData drawData ) {
			return Color.White * drawData.Opacity;
		}


		////////////////


		public abstract Animator CreateAnimation( int worldX, int worldY );


		////////////////

		public sealed override void Update() {
			int nearbyAnims = 0;
			int animRangeLeft = this.RecentAnimationsScanRange.WorldLeft;
			int animRangeRight = this.RecentAnimationsScanRange.WorldRight;
			int animRangeTop = this.RecentAnimationsScanRange.WorldTop;
			int animRangeBot = this.RecentAnimationsScanRange.WorldBottom;

			int rangeWid = animRangeRight - animRangeLeft;
			int rangeHei = animRangeBot - animRangeTop;

			var inactiveAnims = new HashSet<Animator>();

			foreach( Animator anim in this.Animators ) {
				if( !anim.IsActive ) {
					inactiveAnims.Add( anim );
					continue;
				}

				if( !anim.HasAbsoluteWidth ) {
					if( ( anim.WorldX + anim.Width ) < animRangeLeft || anim.WorldX >= animRangeRight ) {
						continue;
					}
				}
				if( !anim.HasAbsoluteHeight ) {
					if( ( anim.WorldY + anim.Height ) < animRangeTop && anim.WorldY >= animRangeBot ) {
						continue;
					}
				}

				nearbyAnims++;
			}

			if( this.TicksElapsedSinceLastAnimation++ >= this.TickDurationBetweenNewAnimations ) {
				this.TicksElapsedSinceLastAnimation = 0;

				for( int i = nearbyAnims; i < this.NeededAnimationsQuantity; i++ ) {
					int randWldX = animRangeLeft + Main.rand.Next( rangeWid );
					int randWldY = animRangeTop + Main.rand.Next( rangeHei );

					if( inactiveAnims.Count > 0 ) {
						Animator anim = inactiveAnims.First();
						anim.Reset( randWldX, randWldY );

						inactiveAnims.Remove( anim );
					} else {
						Animator anim = this.CreateAnimation( randWldX, randWldY );
						this.Animators.Add( anim );
					}
				}
			}
		}


		////////////////

		public sealed override void Draw(
				SpriteBatch sb,
				Rectangle screenFrame,
				SceneDrawData drawData,
				float drawDepth ) {
			int wldX = (int)Main.screenPosition.X + screenFrame.X;
			int wldY = (int)Main.screenPosition.Y + screenFrame.Y;

			this.RecentAnimationsScanRange = (
				WorldLeft: wldX - screenFrame.Width,
				WorldRight: wldX + screenFrame.Width + screenFrame.Width,
				WorldTop: wldY,
				WorldBottom: wldY + screenFrame.Height
			);

			Color color = this.GetSceneColor( drawData );

			if( SurroundingsMod.Instance.Config.DebugModeSceneInfo ) {
				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer,
					//"opacity%: " + drawData.Opacity.ToString( "N2" ) +
					//", color: " + color.ToString() +
					"screenFrame: " + screenFrame +
					", anims#: "+ this.Animators.Where(a=>!a.IsActive).Count()+":"+this.Animators.Count +
					", range: " + this.RecentAnimationsScanRange,
					20
				);
			}

			this.DrawAnimations( sb, color );

			// I want to try to get drawDepth working at some point:
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}


		////

		protected void DrawAnimations( SpriteBatch sb, Color color ) {
			foreach( Animator anim in this.Animators ) {
				if( anim.IsActive ) {
					anim.Draw( sb, color );
				}
			}

			/*if( SurroundingsMod.Instance.Config.DebugModeSceneInfo ) {
				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer + "_ANIMS",
					string.Join(", ", this.Animators
						.Select( a => {
							float x = (float)a.WorldX - Main.screenPosition.X;
							x -= (float)a.Width * 0.5f;

							return (int)x;
						} )
					), 20
				);
			}*/
		}
	}
}
