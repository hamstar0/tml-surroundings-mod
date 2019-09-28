using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.CavernSnow {
	public abstract class CavernSnowSceneBottom : CavernSceneBottom {
		public override SceneContext Context { get; }

		////////////////

		private Texture2D CachedTex = null;



		////////////////

		public CavernSnowSceneBottom( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,
				anyOfBiome: new VanillaBiome[] { VanillaBiome.Snow },
				currentEvent: null,
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.CaveDirt, WorldRegionFlags.CavePreRock },
				customCondition: null
			);
			this.Context.Lock();
		}


		////////////////

		public override Texture2D GetSceneTexture() {
			if( this.CachedTex == null ) {
				this.CachedTex = SurroundingsMod.Instance.GetTexture( "Scenes/Contexts/CavernSnow/CavernSnow_Bottom" );
			}
			return this.CachedTex;
		}


		////////////////

		public override int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			int offset = base.GetSceneTextureVerticalOffset( yPercent, texHeight );
			offset += 128;

			return offset;
		}
	}
}
