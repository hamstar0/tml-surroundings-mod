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


		////////////////

		public SurroundingsConfig Config => this.GetConfig<SurroundingsConfig>();
		public SceneDraw SceneDraw { get; } = new SceneDraw();
		public ScenePicker ScenePicker { get; } = new ScenePicker();



		////////////////

		public SurroundingsMod() {
			SurroundingsMod.Instance = this;

			if( !Main.dedServ ) {
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
			}
		}
	}
}