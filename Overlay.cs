using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;


namespace Surroundings {
	partial class SurroundingsOverlay : Overlay {
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
			SurroundingsMod.Instance?.ScenePicker?.Update();
		}
	}
}
