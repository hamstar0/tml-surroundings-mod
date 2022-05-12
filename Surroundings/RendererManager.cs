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
		private RenderTarget2D SwappableScreenRT;



		////////////////

		public RendererManager() {
			//RenderTarget2D altRT = RendererManager.GetCurrentRT( this.Device );
			RenderTarget2D altRT = new RenderTarget2D(
				graphicsDevice: this.Device,
				width: Main.screenWidth,
				height: Main.screenHeight,
				mipMap: false,
				preferredFormat: this.Device.PresentationParameters.BackBufferFormat,
				preferredDepthFormat: DepthFormat.Depth24
			);

			//

			this.SceneRT = this.GetSceneRT();
			this.SwappableScreenRT = this.GetSwappableScreenRT( altRT );

			//

			this.Device.Clear( Color.Transparent );
		}

		public void Deactivate() {
			this.SceneRT.Dispose();
			this.SwappableScreenRT.Dispose();
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

		public RenderTarget2D GetSwappableScreenRT( RenderTarget2D existingRT ) {
			if( this.SwappableScreenRT != null ) {
				// Refresh if resolution changed
				if( this.SwappableScreenRT.Width != Main.screenWidth || this.SwappableScreenRT.Height != Main.screenHeight ) {
					this.SwappableScreenRT.Dispose();

					this.SwappableScreenRT = null;
				}
			}

			if( this.SwappableScreenRT == null ) {
				this.SwappableScreenRT = new RenderTarget2D(
					graphicsDevice: this.Device,
					width: Main.screenWidth,   //device.PresentationParameters.BackBufferWidth,
					height: Main.screenHeight,  //device.PresentationParameters.BackBufferHeight,
					mipMap: false,
					preferredFormat: this.Device.PresentationParameters.BackBufferFormat,
					preferredDepthFormat: DepthFormat.Depth24
				);
			}

			//

			var newRT = this.SwappableScreenRT;

			this.SwappableScreenRT = existingRT;

			//

			return newRT;
		}
	}
}
