using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceSnow {
	public abstract class SurfaceSnowScene : SurfaceTreeScene {
		public override SceneContext Context { get; }



		////////////////

		protected SurfaceSnowScene( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,
				anyOfBiome: new VanillaBiome[] { VanillaBiome.Snow },
				currentEvent: null,
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.Overworld },
				customCondition: null
			);
			this.Context.Lock();
		}


		////////////////

		public override Texture2D GetSceneTexture() {
			//Main.instance.LoadBackground( 37 );

			//return Main.backgroundTexture[37];
			return SurroundingsMod.Instance.GetTexture( "Scenes/Contexts/SurfaceSnow/Trees/SurfaceSnowForest" );
		}

		////////////////

		public override int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			int offset = (int)( yPercent * (float)texHeight * 1.25f );
			offset += 256;
			offset += SurroundingsMod.Instance.DebugOverlayOffset;

			return offset;
		}
	}
}
