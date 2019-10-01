using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Surroundings.Scenes.Components;
using Surroundings.Scenes.Components.FlameSpurt;
using Terraria;


namespace Surroundings.Scenes.Contexts.Hell {
	public abstract class HellFlamesScene : AnimationsScene {
		public override int NeededAnimationsQuantity => 8;


		////

		public override SceneContext Context { get; }



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

		public override Animator CreateAnimation( int worldX, int worldY ) {
			return new FlameSpurt( worldX, new Vector2(2f) );
		}
	}
}
