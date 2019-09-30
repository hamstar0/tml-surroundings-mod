using System;
using System.Collections.Generic;
using System.Linq;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.AnimatedTexture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts {
	public abstract class RegionedAnimationsScene : Scene {
		private Point CurrentHorizontalWorldRange = default( Point );


		////////////////

		public override int DrawPriority { get; } = 1;

		public override Vector2 FrameSize => new Vector2( Main.screenWidth, Main.screenHeight );

		public override float VerticalTileScrollRate { get; } = 0f;

		public abstract float AnimationScale { get; }

		////

		public abstract int ExpectedAnimations { get; }

		protected IList<(int WorldHorizontalPos, AnimatedTexture Animation)> Animations { get; }
			= new List<(int, AnimatedTexture)>();

		////

		public abstract int RegionTopTileY { get; } //WorldHelpers.UnderworldLayerTopTileY
		public abstract int RegionBottomTileY { get; }  //WorldHelpers.UnderworldLayerBottomTileY



		////////////////

		public abstract Texture2D GetSceneTexture();

		public override Color GetSceneColor( SceneDrawData drawData ) {
			return Color.White * drawData.Opacity;
		}

		public float GetSceneVerticalRangePercent( Vector2 worldPosition ) {
			int tileY = (int)( worldPosition.Y / 16 );
			float tileRange = this.RegionBottomTileY - this.RegionTopTileY;
			float yPercent = (float)( tileY - this.RegionTopTileY ) / tileRange;
			return 1f - yPercent;
		}

		public abstract int GetSceneAnimationVerticalOffset( float yPercent, int tileRange );


		////////////////


		public abstract AnimatedTexture CreateAnimation();


		////////////////

		public sealed override void Update() {
			int nearbyAnims = 0;
			Rectangle mostRecentWldRect = this.MostRecentDrawWorldRectangle;
			Point currHorizWldRange = this.CurrentHorizontalWorldRange;

			foreach( (int horizCenPos, AnimatedTexture anim) in this.Animations ) {
				if( horizCenPos >= currHorizWldRange.X && horizCenPos < currHorizWldRange.Y ) {
					nearbyAnims++;
				}
			}

			for( int i=nearbyAnims; i<this.ExpectedAnimations; i++ ) {
				int randHorizPos = mostRecentWldRect.X + Main.rand.Next( mostRecentWldRect.Width );
				this.Animations.Add( (WorldHorizontalPos: randHorizPos, Animation: this.CreateAnimation()) );
			}
		}


		////////////////

		public sealed override void Draw(
				SpriteBatch sb,
				Rectangle frame,
				SceneDrawData drawData,
				float drawDepth ) {
			var mymod = SurroundingsMod.Instance;

			int x = (int)Main.screenPosition.X + frame.X;
			this.CurrentHorizontalWorldRange = new Point( x - frame.Width, x + (frame.Width * 2) );

			Color color = this.GetSceneColor( drawData );

//			if( mymod.Config.DebugModeSceneInfo ) {
				int tileRange = this.RegionBottomTileY - this.RegionTopTileY;
				float yPercent = this.GetSceneVerticalRangePercent( drawData.Center );
				int yOffset = this.GetSceneAnimationVerticalOffset( yPercent, tileRange );

				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer,
					"opacity%: " + drawData.Opacity.ToString( "N2" ) +
					", color: " + color.ToString() +
					", yPercent: " + yPercent.ToString( "N2" ) +
					", yOffset: " + yOffset +
					//", texZoom: " + texZoom.ToString( "N2" ) +
					", rect: "+ frame,
					20
				);
//			}

			this.DrawAnimations( sb, color, drawData );

			// I want to try to get drawDepth working at some point:
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}


		////

		protected void DrawAnimations( SpriteBatch sb, Color color, SceneDrawData drawData ) {
			int tileRange = this.RegionBottomTileY - this.RegionTopTileY;

			float yPercent = this.GetSceneVerticalRangePercent( drawData.Center );
			int yOffset = this.GetSceneAnimationVerticalOffset( yPercent, tileRange );

			foreach( (int horizCenPos, AnimatedTexture anim) in this.Animations ) {
				float x = horizCenPos - Main.screenPosition.X;
				x -= anim.FramesTexture.Width * 0.5f;

				var scrPos = new Vector2( x, yOffset );

				anim.Draw( sb, scrPos, color, 0f, null, new Vector2(this.AnimationScale) );
			}

//			if( SurroundingsMod.Instance.Config.DebugModeSceneInfo ) {
				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer + "_ANIMS",
					string.Join(", ", this.Animations
						.Select( a => {
							float x = a.WorldHorizontalPos - Main.screenPosition.X;
							x -= a.Animation.FramesTexture.Width * 0.5f;

							return (int)x + ":" + yOffset;
						} )
					), 20
				);
//			}
		}
	}
}
