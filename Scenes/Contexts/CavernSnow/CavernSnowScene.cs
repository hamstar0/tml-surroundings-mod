using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.CavernSnow {
	public abstract class CavernSnowScene : CavernScene {
		public override SceneContext Context { get; }



		////////////////

		protected CavernSnowScene( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,
				anyOfBiome: new VanillaBiome[] { VanillaBiome.Snow },
				currentEvent: null,
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.CaveRock },
				customCondition: CavernScene.IsPlainCave
			);
			this.Context.Lock();
		}
	}
}
