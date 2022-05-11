using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using ModLibsCore.Libraries.Debug;


namespace Surroundings {
	partial class SurroundingsOverlay : Overlay {
		private RendererManager RenderMngr;



		////////////////

		public SurroundingsOverlay(
			EffectPriority priority = EffectPriority.VeryHigh,
			RenderLayers layer = RenderLayers.ForegroundWater	//RenderLayers layer = RenderLayers.Entities )
		) : base( priority, layer ) { }


		////////////////

		public override void Activate( Vector2 position, params object[] args ) {
			this.Mode = OverlayMode.FadeIn;

			this.RenderMngr = new RendererManager();
		}

		public override void Deactivate( params object[] args ) {
			this.Mode = OverlayMode.FadeOut;

			this.RenderMngr.Deactivate();
		}

		////////////////

		public override bool IsVisible() {
			return true;
		}


		////////////////

		public override void Update( GameTime gameTime ) {
			if( Main.gameMenu || Main.netMode == 2 || Main.dedServ ) {
				return;
			}
			if( Lighting.lightMode >= 2 ) {
				return;
			}

			var config = SurroundingsConfig.Instance;
			if( config?.Get<bool>( nameof(config.EnableOverlays) ) != true ) {
				return;
			}
			if( SurroundingsMod.Instance?.HideOverlays != false ) {
				return;
			}

			//

//LogLibraries.LogOnce("OVERLAY.UPDATE");
//DebugLibraries.Print( "OVERLAY.UPDATE", "" );
			SurroundingsMod.Instance.ScenePicker?.Update();
		}
	}
}
