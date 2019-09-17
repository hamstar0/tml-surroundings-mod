using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using Surroundings.Scenes.Contexts.EventBloodMoon;
using Surroundings.Scenes.Contexts.EventSolarEclipse;
using Surroundings.Scenes.Contexts.SurfaceForest;
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
			{
				new SurfaceForestSceneNear().Context,
				new SurfaceForestSceneNear()
			},
			{
				new SurfaceForestSceneFar().Context,
				new SurfaceForestSceneFar()
			},
			{
				new SurfaceForestNightScene( true ).Context,
				new SurfaceForestNightScene( true )
			},
			{
				new SurfaceColdScene().Context,
				new SurfaceColdScene()
			},
			/*{
				new SurfaceRainScene().Context,
				new SurfaceRainScene()
			},*/
			{
				new EventBloodMoonScene().Context,
				new EventBloodMoonScene()
			},
			{
				new EventSolarEclipseScene().Context,
				new EventSolarEclipseScene()
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

				foreach( (SceneContext checkCtx, Scene scene) in this.Definitions ) {
					if( checkCtx.Check( ctx, false ) ) {
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
