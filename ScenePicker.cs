using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using Surroundings.Scenes;
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
				new OverworldScene().Context,
				new OverworldScene()
			}
		};

		private IDictionary<Scene, float> SceneFades = new Dictionary<Scene, float>();



		////////////////

		public IEnumerable<(Scene Scene, float Opacity)> GetScenes( SceneLayer layer ) {
			return this.GetScenesUnordered( layer )
				.OrderBy( kv => kv.Scene.DrawPriority );
		}

		private IEnumerable<(Scene Scene, float Opacity)> GetScenesUnordered( SceneLayer layer ) {
			lock( ScenePicker.MyLock ) {
				foreach( (Scene scene, float fadeProgress) in this.SceneFades ) {
					if( scene.Context.Layer != layer ) {
						continue;
					}

					if( fadeProgress < 0f ) {
						yield return (scene, 1f + fadeProgress);
					} else if( fadeProgress >= 0f ) {
						yield return (scene, 1f - fadeProgress);
					}
				}
			}
		}
	}
}
