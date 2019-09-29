using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.CavernJungle {
	public abstract class CavernJungleScene : CavernVinesScene {
		private Texture2D CachedTex = null;


		////////////////

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

		public override Texture2D GetSceneTexture() {
			if( this.CachedTex == null ) {
				this.CachedTex = SurroundingsMod.Instance.GetTexture( "Scenes/Contexts/CavernJungle/JungleVines" );
			}
			return this.CachedTex;
		}
	}
}
