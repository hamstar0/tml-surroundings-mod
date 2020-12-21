using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace Surroundings.Scenes.Contexts.CavernSnow {
	public abstract class CavernSnowSceneTop : CavernSceneTop {
		public override SceneContext Context { get; }

		////////////////

		private Texture2D CachedTex = null;



		////////////////

		protected CavernSnowSceneTop( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,
				currentEvent: null,
				anyOfBiome: new VanillaBiome[] { VanillaBiome.Snow },
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.Cave },
				customCondition: null
			);
			this.Context.Lock();
		}


		////////////////

		public override Texture2D GetSceneTexture() {
			if( this.CachedTex == null ) {
				this.CachedTex = SurroundingsMod.Instance.GetTexture( "Scenes/Contexts/CavernSnow/CavernSnow_Top" );
			}
			return this.CachedTex;
		}
	}
}
