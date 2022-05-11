using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using ModLibsCore.Libraries.Debug;


namespace Surroundings {
	partial class SurroundingsOverlay : Overlay {
		private void DrawSceneToRT( SpriteBatch sb, RenderTarget2D currentRT, out RenderTarget2D oldRT ) {
			GraphicsDevice device = Main.graphics.GraphicsDevice;

			oldRT = RendererManager.GetCurrentRT( device );

			//

			device.SetRenderTarget( currentRT );

			device.Clear( Color.Transparent );

			//

			this.DrawAllScenes( sb );

			//

			if( oldRT != null ) {
				Main.screenTarget = this.RenderMngr.GetScreenRT( oldRT );

				device.SetRenderTargets( Main.screenTarget );
			} else {
				device.SetRenderTarget( null );
			}

			device.Clear( Color.Transparent );
		}

		////

		private void DrawAllScenes( SpriteBatch sb ) {
			Vector2 wldScrMid = Main.screenPosition;    //Main.LocalPlayer.Center
			wldScrMid.X += Main.screenWidth / 2;
			wldScrMid.Y += Main.screenHeight / 2;

			SceneDrawData drawData = SceneDrawData.GetEnvironmentData( wldScrMid );

			//

			this.DrawScenesOfGameLayer( sb, drawData );
			this.DrawScenesOfFarLayer( sb, drawData );
			this.DrawScenesOfNearLayer( sb, drawData );
			this.DrawScenesOfScreenLayer( sb, drawData );
		}

		////

		private void DrawScenesOfGameLayer( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;

			//mymod.BlurFX.Parameters["ScreenWidth"].SetValue( (float)Main.screenWidth );
			//mymod.BlurFX.Parameters["ScreenHeight"].SetValue( (float)Main.screenHeight );

			sb.Begin(
				SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				null,//SurroundingsMod.Instance.BlurFX,
				Main.Transform
			);

			mymod.SceneDraw.DrawScenesOfGameLayer( sb, drawData );

			sb.End();
		}

		private void DrawScenesOfFarLayer( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;

			sb.Begin(
				SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				null,//SurroundingsMod.Instance.BlurFX,
				Main.Transform
			);

			mymod.SceneDraw.DrawScenesOfFarLayer( sb, drawData );

			sb.End();
		}

		private void DrawScenesOfNearLayer( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;

			sb.Begin(
				SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				null,//SurroundingsMod.Instance.BlurFX,
				Main.Transform
			);

			mymod.SceneDraw.DrawScenesOfNearLayer( sb, drawData );

			sb.End();
		}

		private void DrawScenesOfScreenLayer( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;

			sb.Begin( SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				null,//SurroundingsMod.Instance.BlurFX,
				Main.Transform
			);

			mymod.SceneDraw.DrawScenesOfScreenLayer( sb, drawData );

			sb.End();
		}
	}
}
