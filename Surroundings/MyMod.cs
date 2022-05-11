using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Debug.CustomHotkeys;


namespace Surroundings {
	public class SurroundingsMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-surroundings-mod";


		////////////////

		public static SurroundingsMod Instance => ModContent.GetInstance<SurroundingsMod>();



		////////////////

		public bool HideOverlays { get; set; } = false;

		public SceneDraw SceneDraw { get; private set; }
		public ScenePicker ScenePicker { get; private set; }


		////////////////

		internal Effect OverlayFX = null;
		//internal Effect BlurFX = null;

		internal int DebugOverlayOffset = 0;



		////////////////

		public override void Load() {
			if( Main.netMode != NetmodeID.Server && !Main.dedServ ) {
				this.SceneDraw = new SceneDraw();
				this.ScenePicker = new ScenePicker();

				Overlays.Scene["Surroundings"] = new SurroundingsOverlay();
				Overlays.Scene.Activate( "Surroundings" );
			}
		}

		public override void Unload() {
			if( Main.netMode != NetmodeID.Server && !Main.dedServ ) {
				Overlays.Scene.Deactivate( "Surroundings" );
			}
		}


		////

		public override void PostSetupContent() {
			if( Main.netMode != NetmodeID.Server && !Main.dedServ ) {
				this.OverlayFX = this.GetEffect( "Effects/Overlay" );
				//this.BlurFX = this.GetEffect( "Effects/Blur" );

				CustomHotkeys.BindActionToKey1( "SurroundingsFGRaise", () => {
					if( SurroundingsConfig.Instance.DebugModeLayerEdit ) {
						this.DebugOverlayOffset += 8;
						Main.NewText( "Offset: " + this.DebugOverlayOffset );
					}
				} );
				CustomHotkeys.BindActionToKey2( "SurroundingsFGLower", () => {
					if( SurroundingsConfig.Instance.DebugModeLayerEdit ) {
						this.DebugOverlayOffset -= 8;
						Main.NewText( "Offset: " + this.DebugOverlayOffset );
					}
				} );

				/*LoadHooks.AddSafeWorldLoadEachHook( () => {
					SurroundingsOverlay.RefreshRender = true;
				} );*/
			}
		}
	}
}