using System;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.CavernDirt {
	public abstract class CavernDirtSceneBottom : CavernSceneBottom {
		public override SceneContext Context { get; }

		////////////////

		private Texture2D CachedTex = null;



		////////////////

		public CavernDirtSceneBottom( SceneLayer layer ) {
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
				this.CachedTex = SurroundingsMod.Instance.GetTexture( "Scenes/Contexts/CavernDirt/CavernDirt_Bottom" );
			}
			return this.CachedTex;
		}


		////////////////

		public override int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			int offset = base.GetSceneTextureVerticalOffset( yPercent, texHeight );
			//offset += 128;	!
			offset += (Main.screenHeight - 580);    //640

			return offset;
		}
	}
}
