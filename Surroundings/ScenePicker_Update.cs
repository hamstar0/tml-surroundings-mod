using System;
using System.Collections.Generic;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace Surroundings {
	public partial class ScenePicker {
		public void Update() {
			this.UpdateLayersScenesAnimations();
			
			foreach( Scene scene in this.ActiveScenesCache ) {
				scene.Update();
			}
		}

		////

		private void UpdateLayersScenesAnimations() {
			var picker = SurroundingsMod.Instance.ScenePicker;
			this.CurrentContext = picker.GetCurrentContextSansLayer();
			ISet<Scene> otherScenes;

			this.ActiveScenesCache.Clear();
			this.OtherScenesCache.Clear();

			this.CurrentContext.SetLayer( SceneLayer.Screen );
			this.ActiveScenesCache.UnionWith( picker.GetScenesOfContext( this.CurrentContext, out otherScenes ) );
			this.OtherScenesCache.UnionWith( otherScenes );

			this.CurrentContext.SetLayer( SceneLayer.Near );
			this.ActiveScenesCache.UnionWith( picker.GetScenesOfContext( this.CurrentContext, out otherScenes ) );
			this.OtherScenesCache.IntersectWith( otherScenes );

			this.CurrentContext.SetLayer( SceneLayer.Far );
			this.ActiveScenesCache.UnionWith( picker.GetScenesOfContext( this.CurrentContext, out otherScenes ) );
			this.OtherScenesCache.IntersectWith( otherScenes );

			this.CurrentContext.SetLayer( SceneLayer.Game );
			this.ActiveScenesCache.UnionWith( picker.GetScenesOfContext( this.CurrentContext, out otherScenes ) );
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
