using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;


namespace Surroundings {
	class SurroundingsOverlay : Overlay {
		private RenderTarget2D SceneRT;
		private RenderTarget2D ScreenRT;



		////////////////

		public SurroundingsOverlay(
					EffectPriority priority = EffectPriority.VeryHigh,
					RenderLayers layer = RenderLayers.Entities )
				: base( priority, layer ) {
		}


		////////////////

		public override void Activate( Vector2 position, params object[] args ) {
			GraphicsDevice device = Main.graphics.GraphicsDevice;

			this.Mode = OverlayMode.FadeIn;
			this.SceneRT = new RenderTarget2D(
				device,
				Main.screenWidth,   //device.PresentationParameters.BackBufferWidth,
				Main.screenHeight,  //device.PresentationParameters.BackBufferHeight,
				false,
				device.PresentationParameters.BackBufferFormat,
				DepthFormat.Depth24
			);
			this.ScreenRT = new RenderTarget2D(
				device,
				Main.screenWidth,   //device.PresentationParameters.BackBufferWidth,
				Main.screenHeight,  //device.PresentationParameters.BackBufferHeight,
				false,
				device.PresentationParameters.BackBufferFormat,
				DepthFormat.Depth24
			);
		}

		public override void Deactivate( params object[] args ) {
			this.Mode = OverlayMode.FadeOut;
			this.SceneRT.Dispose();
		}

		////////////////

		public override bool IsVisible() {
			return true;
		}


		////////////////

		private RenderTarget2D GetSceneRT() {
			if( this.SceneRT.Width != Main.screenWidth || this.SceneRT.Height != Main.screenHeight ) {
				GraphicsDevice device = Main.graphics.GraphicsDevice;
				this.SceneRT.Dispose();

				this.SceneRT = new RenderTarget2D(
					device,
					Main.screenWidth,   //device.PresentationParameters.BackBufferWidth,
					Main.screenHeight,  //device.PresentationParameters.BackBufferHeight,
					false,
					device.PresentationParameters.BackBufferFormat,
					DepthFormat.Depth24
				);
			}

			return this.SceneRT;
		}

		private RenderTarget2D GetScreenRT( RenderTarget2D oldRT ) {
			if( this.ScreenRT.Width != Main.screenWidth || this.ScreenRT.Height != Main.screenHeight ) {
				GraphicsDevice device = Main.graphics.GraphicsDevice;
				this.ScreenRT.Dispose();

				this.ScreenRT = new RenderTarget2D(
					device,
					Main.screenWidth,   //device.PresentationParameters.BackBufferWidth,
					Main.screenHeight,  //device.PresentationParameters.BackBufferHeight,
					false,
					device.PresentationParameters.BackBufferFormat,
					DepthFormat.Depth24
				);
			}

			var rt = this.ScreenRT;
			this.ScreenRT = oldRT;
			return rt;
		}


		////////////////

		public override void Update( GameTime gameTime ) {
		}


		////////////////

		public override void Draw( SpriteBatch sb ) {
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
			var mymod = SurroundingsMod.Instance;
			GraphicsDevice device = Main.graphics.GraphicsDevice;

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

			sb.Begin( SpriteSortMode.Immediate,//Immediate
				BlendState.AlphaBlend,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				null,
				Main.Transform
			);

			sb.Draw( Main.magicPixel, new Rectangle( 0, 0, Main.screenWidth, Main.screenHeight ), Color.Transparent );
			/*sb.Draw( Main.magicPixel,
				new Rectangle( 0, 0, Main.screenWidth, Main.screenHeight ),
				null,
				Color.Transparent,
				0f,
				default(Vector2),
				SpriteEffects.None,
				5f
			);*/
			mymod.Scene.DrawSceneGame( sb );
			mymod.Scene.DrawSceneFar( sb );
			mymod.Scene.DrawSceneNear( sb );
			mymod.Scene.DrawSceneScreen( sb );

			sb.End();

			if( existingRT != null ) {
				Main.screenTarget = this.GetScreenRT( existingRT );
				device.SetRenderTargets( Main.screenTarget );
			} else {
				device.SetRenderTarget( null );
			}

			return existingRT;
		}


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

			sb.Draw( overlay, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White );

			sb.End();
		}
	}
}
