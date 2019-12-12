using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.CavernJungle {
	public abstract class CavernJungleScene : JungleVinesScene {
		public override SceneContext Context { get; }

		public override int DrawPriority { get; } = 1;



		////////////////

		public CavernJungleScene( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,
				currentEvent: null,
				anyOfBiome: new VanillaBiome[] { VanillaBiome.Jungle },
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.Cave },
				customCondition: null
			);
			this.Context.Lock();
		}
	}
}
