using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using HamstarHelpers.Services.AnimatedTexture;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.Hell {
	public abstract class HellFlamesScene : RegionedAnimationsScene {
		public override int ExpectedAnimations => 16;

		public int AnimationFrames => 12;


		////

		public override int RegionTopTileY => WorldHelpers.UnderworldLayerTopTileY;
		public override int RegionBottomTileY => WorldHelpers.UnderworldLayerBottomTileY;


		////

		public override SceneContext Context { get; }


		////////////////

		private Texture2D CachedTex = null;




		////////////////

		public HellFlamesScene( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,
				currentEvent: null,
				anyOfBiome: null,
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.Hell },
				customCondition: null
			);
			this.Context.Lock();
		}


		////////////////

		public override Texture2D GetSceneTexture() {
			if( this.CachedTex == null ) {
				this.CachedTex = SurroundingsMod.Instance.GetTexture( "Scenes/Contexts/Hell/FlameSpurts/FlameSpurt" );
			}
			return this.CachedTex;
		}


		////////////////

		public override AnimatedTexture CreateAnimation() {
			return new AnimatedTexture( this.GetSceneTexture(), this.AnimationFrames, this.FlameAnimator );
		}


		////////////////

		private (int NextFrame, int TickDuration) FlameAnimator( AnimatedTexture anim ) {
			int nextFrame = (anim.CurrentFrame + 1) % anim.MaxFrames;
			return (nextFrame, 8);
		}
	}
}
