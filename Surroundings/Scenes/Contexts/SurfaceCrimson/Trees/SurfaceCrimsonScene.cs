using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;


namespace Surroundings.Scenes.Contexts.SurfaceCrimson {
	public abstract class SurfaceCrimsonScene : SurfaceTreeScene {
		public override SceneContext Context { get; }



		////////////////

		protected SurfaceCrimsonScene( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,
				anyOfBiome: new VanillaBiome[] { VanillaBiome.Crimson },
				currentEvent: null,
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.Overworld },
				customCondition: null
			);
			this.Context.Lock();
		}


		////////////////

		public override Texture2D GetSceneTexture() {
			Main.instance.LoadBackground( 45 );

			return Main.backgroundTexture[45];
		}

		////////////////

		public override int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			int offset = (int)( yPercent * (float)texHeight * 1.25f );
			//offset += 224 + SurroundingsMod.Instance.DebugOverlayOffset;
			offset += (Main.screenHeight - 568) + SurroundingsMod.Instance.DebugOverlayOffset;  //was 544

			return offset;
		}
	}
}
