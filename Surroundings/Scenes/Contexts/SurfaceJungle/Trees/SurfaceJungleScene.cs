﻿using System;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceJungle {
	public abstract class SurfaceJungleScene : SurfaceTreeScene {
		public override SceneContext Context { get; }



		////////////////

		protected SurfaceJungleScene( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,
				anyOfBiome: new VanillaBiome[] { VanillaBiome.Jungle },
				currentEvent: null,
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.Overworld },
				customCondition: null
			);
			this.Context.Lock();
		}


		////////////////

		public override Texture2D GetSceneTexture() {
			Main.instance.LoadBackground( 61 );

			return Main.backgroundTexture[61];
		}

		////////////////

		public override int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			int offset = (int)( yPercent * (float)texHeight * 1.25f );
			//offset += 320 + SurroundingsMod.Instance.DebugOverlayOffset;
			offset += (Main.screenHeight - 464) + SurroundingsMod.Instance.DebugOverlayOffset;

			return offset;
		}
	}
}
