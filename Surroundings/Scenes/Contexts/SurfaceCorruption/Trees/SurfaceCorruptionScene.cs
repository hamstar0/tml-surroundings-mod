using System;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceCorruption {
	public abstract class SurfaceCorruptionScene : SurfaceTreeScene {
		public override SceneContext Context { get; }



		////////////////

		protected SurfaceCorruptionScene( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,
				anyOfBiome: new VanillaBiome[] { VanillaBiome.Corruption },
				currentEvent: null,
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.Overworld },
				customCondition: null
			);
			this.Context.Lock();
		}


		////////////////

		public override Color GetSceneColor( SceneDrawData drawData ) {
			Color color = base.GetSceneColor( drawData );
			color.R = (byte)((float)color.R * 0.75f);
			color.G = (byte)((float)color.G * 0.75f);
			color.B = (byte)((float)color.B * 0.75f);

			return color;
		}

		public override Texture2D GetSceneTexture() {
			Main.instance.LoadBackground( 44 );

			return Main.backgroundTexture[44];
		}

		////////////////

		public override int GetSceneTextureVerticalOffset( float yRangePercent, int texHeight ) {
			int offset = (int)( yRangePercent * (float)texHeight * 1.25f );
			//offset += 256 + SurroundingsMod.Instance.DebugOverlayOffset;
			offset += (Main.screenHeight - 512) + SurroundingsMod.Instance.DebugOverlayOffset;

			return offset;
		}
	}
}
