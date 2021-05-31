using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;


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

		public override int GetSceneTextureVerticalOffset( float yPercent, int frameHeight ) {
			int offset = (int)( yPercent * (float)frameHeight * 0.8f );
			offset += 96;
			offset += (Main.screenHeight - 512);
			offset += SurroundingsMod.Instance.DebugOverlayOffset;

			return offset;
		}
	}
}
