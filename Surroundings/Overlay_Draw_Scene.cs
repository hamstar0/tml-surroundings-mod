using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;


namespace Surroundings {
	partial class SurroundingsOverlay : Overlay {
		private RenderTarget2D DrawSceneToTarget( SpriteBatch sb ) {
			GraphicsDevice device = Main.graphics.GraphicsDevice;
			Vector2 wldScrMid = Main.screenPosition;
			wldScrMid.X += Main.screenWidth / 2;
			wldScrMid.Y += Main.screenHeight / 2;
			SceneDrawData drawData = SceneDrawData.GetEnvironmentData( wldScrMid );	//Main.LocalPlayer.Center );
			
			RenderTargetBinding[] rtBindings = device.GetRenderTargets();
			RenderTarget2D existingRT = rtBindings.Length > 0 ?
				(RenderTarget2D)device.GetRenderTargets()[0].RenderTarget :
				null;

			device.SetRenderTarget( this.GetSceneRT() );
			device.Clear( Color.Transparent );

			//Color[] oldData = null;
			//if( existingRT != null && existingRT == Main.screenTarget ) {
			//	oldData = this.GetBuffer();
			//	existingRT.GetData( oldData );
			//}

			this.DrawClear( sb );
			this.DrawScenesOfGameLayer( sb, drawData );
			this.DrawScenesOfFarLayer( sb, drawData );
			this.DrawScenesOfNearLayer( sb, drawData );
			this.DrawScenesOfScreenLayer( sb, drawData );

			if( existingRT != null ) {
				Main.screenTarget = this.GetScreenRT( existingRT );
				device.SetRenderTargets( Main.screenTarget );
			} else {
				device.SetRenderTarget( null );
			}

			return existingRT;
		}

		////

		private void DrawScenesOfGameLayer( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;

			//mymod.BlurFX.Parameters["ScreenWidth"].SetValue( (float)Main.screenWidth );
			//mymod.BlurFX.Parameters["ScreenHeight"].SetValue( (float)Main.screenHeight );

			sb.Begin( SpriteSortMode.Deferred,
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

			sb.Begin( SpriteSortMode.Deferred,
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

			sb.Begin( SpriteSortMode.Deferred,
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
