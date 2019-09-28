using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using Surroundings.Scenes.Contexts.CavernDirt;
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
		private static object MyLock { get; } = new object();



		////////////////

		private IDictionary<SceneContext, Scene> Definitions = new Dictionary<SceneContext, Scene> {
			// Surface Forest
			{
				new SurfaceForestSceneNear().Context,
				new SurfaceForestSceneNear()
			},
			{
				new SurfaceForestSceneFar().Context,
				new SurfaceForestSceneFar()
			},
			{
				new SurfaceForestNightScene().Context,
				new SurfaceForestNightScene()
			},
			
			// Surface Snow
			{
				new SurfaceSnowSceneNear().Context,
				new SurfaceSnowSceneNear()
			},
			{
				new SurfaceSnowSceneFar().Context,
				new SurfaceSnowSceneFar()
			},
			{
				new SurfaceSnowSceneGame().Context,
				new SurfaceSnowSceneGame()
			},
			
			// Surface Jungle
			{
				new SurfaceJungleSceneNear().Context,
				new SurfaceJungleSceneNear()
			},
			{
				new SurfaceJungleSceneFar().Context,
				new SurfaceJungleSceneFar()
			},
			{
				new SurfaceJungleSceneGame().Context,
				new SurfaceJungleSceneGame()
			},
			
			// Surface Corruption
			{
				new SurfaceCorruptionSceneNear().Context,
				new SurfaceCorruptionSceneNear()
			},
			{
				new SurfaceCorruptionSceneFar().Context,
				new SurfaceCorruptionSceneFar()
			},
			
			// Surface Crimson
			{
				new SurfaceCrimsonSceneNear().Context,
				new SurfaceCrimsonSceneNear()
			},
			{
				new SurfaceCrimsonSceneFar().Context,
				new SurfaceCrimsonSceneFar()
			},
			
			// Solar Rain
			/*{
				new SurfaceRainScene().Context,
				new SurfaceRainScene()
			},*/
			
			// Solar Eclipse
			{
				new EventBloodMoonScene().Context,
				new EventBloodMoonScene()
			},

			{
				new EventSolarEclipseScene().Context,
				new EventSolarEclipseScene()
			},
			
			// Cavern Dirt
			{
				new CavernDirtSceneTopNear().Context,
				new CavernDirtSceneTopNear()
			},
			{
				new CavernDirtSceneTopFar().Context,
				new CavernDirtSceneTopFar()
			},
			{
				new CavernDirtSceneBottomNear().Context,
				new CavernDirtSceneBottomNear()
			},
			{
				new CavernDirtSceneBottomFar().Context,
				new CavernDirtSceneBottomFar()
			},
			
			// Cavern Rock
			{
				new CavernRockSceneTopNear().Context,
				new CavernRockSceneTopNear()
			},
			{
				new CavernRockSceneTopFar().Context,
				new CavernRockSceneTopFar()
			},
			{
				new CavernRockSceneBottomNear().Context,
				new CavernRockSceneBottomNear()
			},
			{
				new CavernRockSceneBottomFar().Context,
				new CavernRockSceneBottomFar()
			},

			// Cavern Snow
			{
				new CavernSnowSceneTopNear().Context,
				new CavernSnowSceneTopNear()
			},
			{
				new CavernSnowSceneTopFar().Context,
				new CavernSnowSceneTopFar()
			},
			{
				new CavernSnowSceneBottomNear().Context,
				new CavernSnowSceneBottomNear()
			},
			{
				new CavernSnowSceneBottomFar().Context,
				new CavernSnowSceneBottomFar()
			},
		};
		
		private IDictionary<Scene, float> SceneFades = new Dictionary<Scene, float>();



		////////////////

		public ScenePicker() {
		}


		////////////////

		public IEnumerable<(Scene Scene, float Opacity)> GetActiveScenes( SceneLayer layer ) {
			return this.GetActiveScenesUnordered( layer )
				.OrderBy( kv => kv.Scene.DrawPriority );
		}

		private IEnumerable<(Scene Scene, float Opacity)> GetActiveScenesUnordered( SceneLayer layer ) {
			lock( ScenePicker.MyLock ) {
				foreach( (Scene scene, float fadeProgress) in this.SceneFades ) {
					if( scene.Context.Layer != layer ) {
						continue;
					}

					if( fadeProgress < 0f ) {
						yield return (scene, 1f + fadeProgress);
					} else {
						yield return (scene, 1f - fadeProgress);
					}
				}
			}
		}


		////////////////

		internal ISet<Scene> GetScenesOfContext( SceneContext ctx, out ISet<Scene> otherScenes ) {
			lock( ScenePicker.MyLock ) {
				var contextScenes = new HashSet<Scene>();
				otherScenes = new HashSet<Scene>();

				int _;
				foreach( (SceneContext checkCtx, Scene scene) in this.Definitions ) {
					if( checkCtx.Check( ctx, false, out _ ) ) {
						contextScenes.Add( scene );
					} else {
						otherScenes.Add( scene );
					}
				}

				return contextScenes;
			}
		}
	}
}
