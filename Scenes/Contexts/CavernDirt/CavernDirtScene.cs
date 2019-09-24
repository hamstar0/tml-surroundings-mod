using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.CavernDirt {
	public abstract class CavernDirtScene : CavernScene {
		public override SceneContext Context { get; }

		////////////////

		private Texture2D CachedTex = null;



		////////////////

		protected CavernDirtScene( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,
				vanillaBiome: VanillaBiome.Cave,
				currentEvent: null,
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.CaveDirt },
				customCondition: null
			);
			this.Context.Lock();
		}


		////////////////

		public override Texture2D GetSceneTexture() {
			if( this.CachedTex == null ) {
				this.CachedTex = SurroundingsMod.Instance.GetTexture( "Scenes/Contexts/CavernDirt/CavernDirt_Top" );
			}
			return this.CachedTex;
		}

		////////////////

		public override int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			int offset = (int)( yPercent * (float)texHeight * 1.25f );
			offset += SurroundingsMod.Instance.DebugOverlayOffset;

			return offset;
		}
	}
}
