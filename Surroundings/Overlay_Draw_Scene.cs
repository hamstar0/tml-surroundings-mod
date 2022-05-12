using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using ModLibsCore.Libraries.Debug;


namespace Surroundings {
	partial class SurroundingsOverlay : Overlay {
		private void DrawAllScenesToTarget( SpriteBatch sb, RenderTarget2D targetRT, out RenderTarget2D existingRT ) {
			GraphicsDevice device = Main.graphics.GraphicsDevice;

			existingRT = RendererManager.GetCurrentRT( device );
			if( existingRT == null ) {
				return;
			}

			//

			device.SetRenderTarget( targetRT );

			device.Clear( Color.Transparent );

			//

			this.DrawAllScenes( sb );

			//

			RenderTarget2D swapRT = this.RenderMngr.GetSwappableScreenRT( existingRT );

			device.SetRenderTarget( swapRT );

			//

			Main.screenTarget = swapRT;
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
