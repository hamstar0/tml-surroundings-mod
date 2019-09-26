﻿using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.CavernDirt {
	public abstract class CavernDirtScene : CavernScene {
		public override SceneContext Context { get; }



		////////////////

		protected CavernDirtScene( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,
				anyOfBiome: null,
				currentEvent: null,
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.CaveDirt, WorldRegionFlags.CavePreRock },
				customCondition: CavernScene.IsPlainCave
			);
			this.Context.Lock();
		}
	}
}
