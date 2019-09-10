using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.DotNET.Extensions;
using System;
using Terraria;


namespace Surroundings {
	public partial class ScenePicker {
		internal void UpdateScenesPerContext( SceneContext ctx ) {
			lock( ScenePicker.MyLock ) {
				foreach( (SceneContext checkCtx, Scene scene) in this.Definitions ) {
					bool isActive = checkCtx.Check( ctx, false );
					this.UpdateSceneState( scene, isActive );
					this.UpdateSceneFade( scene );
				}
			}
		}

		private void UpdateSceneState( Scene scene, bool isActive ) {
			if( isActive ) {
				if( !this.SceneFades.ContainsKey( scene ) ) {
					this.SceneFades[scene] = -1f;
				} else {
					if( this.SceneFades[scene] > 0 ) {
						this.SceneFades[scene] = -this.SceneFades[scene];
					}
				}
			} else {
				if( this.SceneFades.ContainsKey( scene ) ) {
					if( this.SceneFades[scene] == 0f ) {
						this.SceneFades[scene] += 1f / 60f;
					} else if( this.SceneFades[scene] < 0f ) {
						this.SceneFades[scene] = -this.SceneFades[scene];
					}
				}
			}
		}

		private void UpdateSceneFade( Scene scene ) {
			float oldFade = this.SceneFades[scene];
			if( oldFade == 0f ) {
				return;
			}

			float newFade = oldFade + ( 1f / 60f );

			if( oldFade < 1f ) {
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
