using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;


namespace Surroundings {
	public partial class ScenePicker {
		private static object MyLock { get; } = new object();



		////////////////

		private IList<Scene> Definitions;
		private IList<SceneContext> DefinitionContexts;

		private IDictionary<Scene, float> SceneFades = new Dictionary<Scene, float>();

		////

		private ISet<Scene> ActiveScenesCache = new HashSet<Scene>();
		private ISet<Scene> OtherScenesCache = new HashSet<Scene>();

		////

		public SceneContext CurrentContext { get; private set; }



		////////////////

		public ScenePicker() {
			this.InitializeDefinitions();
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

				int _, count = this.Definitions.Count;

				for( int i=0; i<count; i++ ) {
					Scene scene = this.Definitions[i];
					SceneContext checkCtx = this.DefinitionContexts[i];

					if( checkCtx.Check(ctx, false, out _) ) {
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
