using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace Surroundings {
	partial class RendererManager {
		public static RenderTarget2D GetCurrentRT( GraphicsDevice device ) {
			RenderTargetBinding[] rts = device.GetRenderTargets();

			RenderTarget2D rt = rts.Length > 0 ?
				(RenderTarget2D)rts[0].RenderTarget :
				null;

			return rt;
		}



		////////////////

		private GraphicsDevice Device => Main.graphics.GraphicsDevice;


		////////////////

		private RenderTarget2D SceneRT;
		private RenderTarget2D ReadiedScreenRT;



		////////////////

		public RendererManager() {
			RenderTarget2D oldRT = RendererManager.GetCurrentRT( this.Device );

			//

			this.SceneRT = this.GetSceneRT();
			this.ReadiedScreenRT = this.GetScreenRT( oldRT );

			//

			this.Device.Clear( Color.Transparent );
		}

		public void Deactivate() {
			this.SceneRT.Dispose();
			this.ReadiedScreenRT.Dispose();
		}


		////////////////

		public RenderTarget2D GetSceneRT() {
			if( this.SceneRT != null ) {
				// Refresh if resolution changed
				if( this.SceneRT.Width != Main.screenWidth || this.SceneRT.Height != Main.screenHeight ) {
					this.SceneRT.Dispose();

					this.SceneRT = null;
				}
			}

			if( this.SceneRT == null ) {
				this.SceneRT = new RenderTarget2D(
					this.Device,
					Main.screenWidth,   //device.PresentationParameters.BackBufferWidth,
					Main.screenHeight,  //device.PresentationParameters.BackBufferHeight,
					false,
					this.Device.PresentationParameters.BackBufferFormat,
					DepthFormat.Depth24
				);
			}

			//

			return this.SceneRT;
		}

		public RenderTarget2D GetScreenRT( RenderTarget2D oldRT ) {
			if( this.ReadiedScreenRT != null ) {
				// Refresh if resolution changed
				if( this.ReadiedScreenRT.Width != Main.screenWidth || this.ReadiedScreenRT.Height != Main.screenHeight ) {
					this.ReadiedScreenRT.Dispose();

					this.ReadiedScreenRT = null;
				}
			}

			if( this.ReadiedScreenRT == null ) {
				this.ReadiedScreenRT = new RenderTarget2D(
					graphicsDevice: this.Device,
					width: Main.screenWidth,   //device.PresentationParameters.BackBufferWidth,
					height: Main.screenHeight,  //device.PresentationParameters.BackBufferHeight,
					mipMap: false,
					preferredFormat: this.Device.PresentationParameters.BackBufferFormat,
					preferredDepthFormat: DepthFormat.Depth24
				);
			}

			//

			var newRT = this.ReadiedScreenRT;

			this.ReadiedScreenRT = oldRT;

			//

			return newRT;
		}
	}
}
