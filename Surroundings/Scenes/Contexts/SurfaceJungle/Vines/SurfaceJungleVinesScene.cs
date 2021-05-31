using System;
using Terraria;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;


namespace Surroundings.Scenes.Contexts.SurfaceJungle {
	public abstract class SurfaceJungleVinesScene : JungleVinesScene {
		public override SceneContext Context { get; }

		public override int DrawPriority { get; } = 1;



		////////////////

		public override float GetSceneOpacity( SceneDrawData drawData ) {
			float occlusion = 1f - Scene.GetSurfaceOpacity( drawData, 0.7f );
			return occlusion * drawData.Opacity;
		}


		////////////////

		public SurfaceJungleVinesScene( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,
				currentEvent: null,
				anyOfBiome: new VanillaBiome[] { VanillaBiome.Jungle },
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.Overworld },
				customCondition: null
			);
			this.Context.Lock();
		}
	}
}
