using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.Debug.CustomHotkeys;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;


namespace Surroundings {
	public class SurroundingsMod : Mod {
		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-surroundings-mod";


		////////////////

		public static SurroundingsMod Instance { get; private set; }



		////////////////

		internal Effect OverlayFX = null;
		internal Effect BlurFX = null;

		internal int DebugOverlayOffset = 0;


		////////////////

		public SceneDraw SceneDraw { get; } = new SceneDraw();
		public ScenePicker ScenePicker { get; } = new ScenePicker();



		////////////////

		public SurroundingsMod() {
			SurroundingsMod.Instance = this;
		}

		public override void Load() {
			if( Main.netMode != 2 && !Main.dedServ ) {
				Overlays.Scene["Surroundings"] = new SurroundingsOverlay();
				Overlays.Scene.Activate( "Surroundings" );
			}
		}

		public override void Unload() {
			SurroundingsMod.Instance = null;

			if( Main.netMode != 2 && !Main.dedServ ) {
				Overlays.Scene.Deactivate( "Surroundings" );
			}
		}


		////////////////

		public override void PostSetupContent() {
			if( !Main.dedServ && Main.netMode != 2 ) {
				this.OverlayFX = this.GetEffect( "Effects/Overlay" );
				this.BlurFX = this.GetEffect( "Effects/Blur" );

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
			}
		}
	}
}