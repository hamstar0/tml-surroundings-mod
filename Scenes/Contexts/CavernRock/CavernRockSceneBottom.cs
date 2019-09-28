using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.CavernRock {
	public abstract class CavernRockSceneBottom : CavernSceneBottom {
		public override SceneContext Context { get; }

		////////////////

		private Texture2D CachedTex = null;



		////////////////

		public CavernRockSceneBottom( SceneLayer layer ) {
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


		////////////////

		public override Texture2D GetSceneTexture() {
			if( this.CachedTex == null ) {
				this.CachedTex = SurroundingsMod.Instance.GetTexture( "Scenes/Contexts/CavernRock/CavernRock_Bottom" );
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
