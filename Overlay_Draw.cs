using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.TModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;


namespace Surroundings {
	partial class SurroundingsOverlay : Overlay {
		public override void Draw( SpriteBatch sb ) {
			if( !LoadHelpers.IsWorldBeingPlayed() ) {
				return;
			}

			sb.End();

			RenderTarget2D oldRT = this.DrawSceneToTarget( sb );

			if( oldRT != null ) {
				this.DrawOldScene( sb, oldRT );
			}

			this.DrawOverlay( sb, this.GetSceneRT() );

			sb.Begin( SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				null,
				Main.Transform
			);
		}


		////////////////

		private RenderTarget2D DrawSceneToTarget( SpriteBatch sb ) {
			GraphicsDevice device = Main.graphics.GraphicsDevice;
			SceneDrawData drawData = SceneDrawData.GetEnvironmentData( Main.LocalPlayer.Center );

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
			this.DrawSceneGame( sb, drawData );
			this.DrawSceneFar( sb, drawData );
			this.DrawSceneNear( sb, drawData );
			this.DrawSceneScreen( sb, drawData );

			if( existingRT != null ) {
				Main.screenTarget = this.GetScreenRT( existingRT );
				device.SetRenderTargets( Main.screenTarget );
			} else {
				device.SetRenderTarget( null );
			}

			return existingRT;
		}

		////

		private void DrawSceneGame( SpriteBatch sb, SceneDrawData drawData ) {
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

			mymod.SceneDraw.DrawSceneGame( sb, drawData );

			sb.End();
		}

		private void DrawSceneFar( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;

			sb.Begin( SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				null,//SurroundingsMod.Instance.BlurFX,
				Main.Transform
			);

			mymod.SceneDraw.DrawSceneFar( sb, drawData );

			sb.End();
		}

		private void DrawSceneNear( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;

			sb.Begin( SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				null,//SurroundingsMod.Instance.BlurFX,
				Main.Transform
			);

			mymod.SceneDraw.DrawSceneNear( sb, drawData );

			sb.End();
		}

		private void DrawSceneScreen( SpriteBatch sb, SceneDrawData drawData ) {
			var mymod = SurroundingsMod.Instance;

			sb.Begin( SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				null,//SurroundingsMod.Instance.BlurFX,
				Main.Transform
			);

			mymod.SceneDraw.DrawSceneScreen( sb, drawData );

			sb.End();
		}


		////////////////

		private void DrawOldScene( SpriteBatch sb, RenderTarget2D oldRT ) {
			sb.Begin( SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				null,
				Main.Transform
			);

			sb.Draw( oldRT, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White );

			sb.End();
		}


		private void DrawOverlay( SpriteBatch sb, Texture2D overlay ) {
			sb.Begin( SpriteSortMode.Immediate,//Deferred
				BlendState.AlphaBlend,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				SurroundingsMod.Instance.OverlayFX,
				Main.Transform
			);
			////mymod.OverlayFX.CurrentTechnique.Passes[0].Apply();

			sb.Draw( overlay, new Rectangle( 0, 0, Main.screenWidth, Main.screenHeight ), Color.White );

			sb.End();
		}


		////////////////

		private void DrawClear( SpriteBatch sb ) {
			sb.Begin( SpriteSortMode.Immediate,//Deferred
				BlendState.AlphaBlend,//NonPremultiplied,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				null,
				Main.Transform
			);

			/*sb.Draw( Main.magicPixel,
				new Rectangle( 0, 0, Main.screenWidth, Main.screenHeight ),
				null,
				Color.Transparent,
				0f,
				default(Vector2),
				SpriteEffects.None,
				5f
			);*/
			sb.Draw(
				Main.magicPixel,
				new Rectangle( 0, 0, Main.screenWidth, Main.screenHeight ),
				Color.Transparent
			);

			sb.End();
		}
	}
}
