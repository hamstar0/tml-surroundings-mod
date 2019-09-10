using HamstarHelpers.Helpers.Debug;
using System;
using System.Collections.Generic;
using Terraria;


namespace Surroundings {
	public partial class ScenePicker {
		internal void UpdateScenes( IEnumerable<Scene> activeScenes, IEnumerable<Scene> otherScenes ) {
			foreach( Scene scene in activeScenes ) {
				if( this.UpdateSceneState( scene, true ) ) {
					this.UpdateSceneFade( scene );
				}
			}
			foreach( Scene scene in otherScenes ) {
				if( this.UpdateSceneState( scene, false ) ) {
					this.UpdateSceneFade( scene );
				}
			}
		}


		////

		private bool UpdateSceneState( Scene scene, bool isActive ) {
			bool isScenePresent = this.SceneFades.ContainsKey( scene );

			if( isActive ) {
				if( !isScenePresent ) {
					this.SceneFades[scene] = -1f;
					isScenePresent = true;
				} else {
					if( this.SceneFades[scene] > 0 ) {
						this.SceneFades[scene] = -this.SceneFades[scene];
					}
				}
			} else {
				if( isScenePresent ) {
					if( this.SceneFades[scene] == 0f ) {
						this.SceneFades[scene] += 1f / 60f;
					} else if( this.SceneFades[scene] < 0f ) {
						this.SceneFades[scene] = -this.SceneFades[scene];
					}
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
