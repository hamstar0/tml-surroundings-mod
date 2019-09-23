using System;
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
				vanillaBiome: VanillaBiome.Cave,
				currentEvent: null,
				regions: WorldRegionFlags.CaveDirt,
				customCondition: null
			);
			this.Context.Lock();
		}


		////////////////

		public override Texture2D GetSceneTexture() {
			return SurroundingsMod.Instance.GetTexture( "Scenes/Contexts/CavernDirt/CavernDirt" );
		}

		////////////////

		public override int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			int offset = (int)( yPercent * (float)texHeight * 1.25f );
			offset += SurroundingsMod.Instance.DebugOverlayOffset;

			return offset;
		}
	}
}
