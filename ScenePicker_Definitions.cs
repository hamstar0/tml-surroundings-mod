using HamstarHelpers.Helpers.Debug;
using Surroundings.Scenes.Contexts.CavernDirt;
using Surroundings.Scenes.Contexts.CavernJungle;
using Surroundings.Scenes.Contexts.CavernRock;
using Surroundings.Scenes.Contexts.CavernSnow;
using Surroundings.Scenes.Contexts.EventBloodMoon;
using Surroundings.Scenes.Contexts.EventSolarEclipse;
using Surroundings.Scenes.Contexts.SurfaceCorruption;
using Surroundings.Scenes.Contexts.SurfaceCrimson;
using Surroundings.Scenes.Contexts.SurfaceForest;
using Surroundings.Scenes.Contexts.SurfaceJungle;
using Surroundings.Scenes.Contexts.SurfaceSnow;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;


namespace Surroundings {
	public partial class ScenePicker {
		private void InitializeDefinitions() {
			this.Definitions = new List<Scene> {
				// Surface Forest
				new SurfaceForestSceneNear(),
				new SurfaceForestSceneFar(),
				new SurfaceForestNightScene(),
			
				// Surface Snow
				new SurfaceSnowSceneNear(),
				new SurfaceSnowSceneFar(),
				new SurfaceSnowSceneGame(),
			
				// Surface Jungle
				new SurfaceJungleSceneNear(),
				new SurfaceJungleSceneFar(),
				new SurfaceJungleSceneGame(),
			
				// Surface Corruption
				new SurfaceCorruptionSceneNear(),
				new SurfaceCorruptionSceneFar(),
			
				// Surface Crimson
				new SurfaceCrimsonSceneNear(),
				new SurfaceCrimsonSceneFar(),

				// Solar Rain
				//new SurfaceRainScene(),
			
				// Solar Eclipse
				new EventBloodMoonScene(),
				new EventSolarEclipseScene(),
			
				// Cavern Dirt
				new CavernDirtSceneTopNear(),
				new CavernDirtSceneTopFar(),
				new CavernDirtSceneBottomNear(),
				new CavernDirtSceneBottomFar(),
			
				// Cavern Rock
				new CavernRockSceneTopNear(),
				new CavernRockSceneTopFar(),
				new CavernRockSceneBottomNear(),
				new CavernRockSceneBottomFar(),

				// Cavern Snow
				new CavernSnowSceneTopNear(),
				new CavernSnowSceneTopFar(),
				new CavernSnowSceneBottomNear(),
				new CavernSnowSceneBottomFar(),

				// Cavern Jungle
				new CavernJungleSceneNear(),
				new CavernJungleSceneFar(),
			};

			this.DefinitionContexts = this.Definitions
				.Select( def => def.Context )
				.ToList();
		}
	}
}
