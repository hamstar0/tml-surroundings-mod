using HamstarHelpers.Helpers.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;


namespace Surroundings {
	public partial class ScenePicker {
		private ISet<Scene> ActiveScenesCache = new HashSet<Scene>();
		private ISet<Scene> OtherScenesCache = new HashSet<Scene>();



		////////////////

		public void Update() {
			if( Main.gameMenu ) { return; }

			this.UpdateLayersScenesAnimations();

			foreach( Scene scene in this.ActiveScenesCache ) {
				scene.Update();
			}
		}

		////

		private void UpdateLayersScenesAnimations() {
			var picker = SurroundingsMod.Instance.ScenePicker;
			SceneContext ctx = picker.GetCurrentContextSansLayer();
			ISet<Scene> otherScenes;

			this.ActiveScenesCache.Clear();
			this.OtherScenesCache.Clear();

			ctx.Layer = SceneLayer.Screen;
			this.ActiveScenesCache.UnionWith( picker.GetScenesOfContext( ctx, out otherScenes ) );
			this.OtherScenesCache.UnionWith( otherScenes );

			ctx.Layer = SceneLayer.Near;
			this.ActiveScenesCache.UnionWith( picker.GetScenesOfContext( ctx, out otherScenes ) );
			this.OtherScenesCache.IntersectWith( otherScenes );

			ctx.Layer = SceneLayer.Far;
			this.ActiveScenesCache.UnionWith( picker.GetScenesOfContext( ctx, out otherScenes ) );
			this.OtherScenesCache.IntersectWith( otherScenes );

			ctx.Layer = SceneLayer.Game;
			this.ActiveScenesCache.UnionWith( picker.GetScenesOfContext( ctx, out otherScenes ) );
			this.OtherScenesCache.IntersectWith( otherScenes );

			picker.UpdateSceneAnimations( this.ActiveScenesCache, this.OtherScenesCache );
		}


		////////////////

		internal void UpdateSceneAnimations( IEnumerable<Scene> activeScenes, IEnumerable<Scene> otherScenes ) {
			foreach( Scene scene in activeScenes ) {
				if( this.UpdateActiveSceneState( scene ) ) {
					this.UpdateSceneFade( scene );
				}
			}
			foreach( Scene scene in otherScenes ) {
				if( this.UpdateInactiveSceneState( scene ) ) {
					this.UpdateSceneFade( scene );
				}
			}
		}


		////

		private bool UpdateActiveSceneState( Scene scene ) {
			bool isScenePresent = this.SceneFades.ContainsKey( scene );

			if( !isScenePresent ) {
				this.SceneFades[scene] = -1f;
				isScenePresent = true;
			} else {
				if( this.SceneFades[scene] > 0 ) {
					this.SceneFades[scene] = -this.SceneFades[scene];
				}
			}

			return isScenePresent;
		}

		private bool UpdateInactiveSceneState( Scene scene ) {
			bool isScenePresent = this.SceneFades.ContainsKey( scene );

			if( isScenePresent ) {
				if( this.SceneFades[scene] == 0f ) {
					this.SceneFades[scene] += 1f / 60f;
				} else if( this.SceneFades[scene] < 0f ) {
					this.SceneFades[scene] = -this.SceneFades[scene];
				}
			}

			return isScenePresent;
		}

		private void UpdateSceneFade( Scene scene ) {
			float oldFade = this.SceneFades[scene];
			if( oldFade == 0f ) {
				return;
			}

			float newFade = oldFade + ( 1f / 60f );

			if( oldFade > 0f ) {
				if( newFade >= 1f ) {
					this.SceneFades.Remove( scene );
				} else {
					this.SceneFades[scene] = newFade;
				}
			} else if( oldFade < 0f ) {
				if( newFade >= 0f ) {
					newFade = 0f;
				}
				this.SceneFades[scene] = newFade;
			}
		}
	}
}
