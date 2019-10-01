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
		private (int WorldLeft, int WorldRight, int WorldTop, int WorldBottom) AnimationsScanRange = (0, 0, 0, 0);


		////////////////

		public override int DrawPriority { get; } = 1;

		public override Vector2 FrameSize => new Vector2( Main.screenWidth, Main.screenHeight );

		public override float VerticalTileScrollRate { get; } = 0f;

		////

		protected IList<Animator> Animators { get; } = new List<Animator>();

		public abstract int NeededAnimationsQuantity { get; }



		////////////////

		public override Color GetSceneColor( SceneDrawData drawData ) {
			return Color.White * drawData.Opacity;
		}


		////////////////


		public abstract Animator CreateAnimation( int worldX, int worldY );


		////////////////

		public sealed override void Update() {
			int nearbyAnims = 0;
			Rectangle mostRecentWldRect = this.MostRecentDrawnFrameInWorld;
			int animRangeLeft = this.AnimationsScanRange.WorldLeft;
			int animRangeRight = this.AnimationsScanRange.WorldRight;
			int animRangeTop = this.AnimationsScanRange.WorldTop;
			int animRangeBot = this.AnimationsScanRange.WorldBottom;

			int rangeWid = animRangeRight - animRangeLeft;
			int rangeHei = mostRecentWldRect.Height;

			var completedAnims = new HashSet<Animator>();

			foreach( Animator anim in this.Animators ) {
				if( anim.HasCompleted ) {
					completedAnims.Add( anim );
					continue;
				}

				if( anim.WorldX >= animRangeLeft && (anim.WorldX + anim.Width) < animRangeRight ) {
					if( anim.WorldY >= animRangeTop && (anim.WorldY + anim.Height) < animRangeBot ) {
						nearbyAnims++;
					}
				}
			}

			for( int i=nearbyAnims; i<this.NeededAnimationsQuantity; i++ ) {
				int randWldX = mostRecentWldRect.X + Main.rand.Next( rangeWid );
				int randWldY = mostRecentWldRect.Y + Main.rand.Next( rangeHei );

				if( completedAnims.Count > 0 ) {
					Animator anim = completedAnims.First();
					anim.Reset( randWldX, randWldY );

					completedAnims.Remove( anim );
				} else {
					Animator anim = this.CreateAnimation( randWldX, randWldY );
					this.Animators.Add( anim );
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

			this.AnimationsScanRange = (
				WorldLeft: wldX - screenFrame.Width,
				WorldRight: wldX + screenFrame.Width + screenFrame.Width,
				WorldTop: wldY,
				WorldBottom: wldY + screenFrame.Height
			);

			Color color = this.GetSceneColor( drawData );

//			if( mymod.Config.DebugModeSceneInfo ) {
				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer,
					"opacity%: " + drawData.Opacity.ToString( "N2" ) +
					", color: " + color.ToString() +
					//", texZoom: " + texZoom.ToString( "N2" ) +
					", screenFrame: " + screenFrame +
					", range: " + this.AnimationsScanRange,
					20
				);
//			}

			this.DrawAnimations( sb, color );

			// I want to try to get drawDepth working at some point:
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}


		////

		protected void DrawAnimations( SpriteBatch sb, Color color ) {
			foreach( Animator anim in this.Animators ) {
				if( !anim.HasCompleted ) {
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
