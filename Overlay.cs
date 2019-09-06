using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;


namespace Surroundings {
	class SurroundingsOverlay : Overlay {
		public RenderTarget2D RT;



		////////////////

		public SurroundingsOverlay(
				EffectPriority priority = EffectPriority.VeryHigh,
				RenderLayers layer = RenderLayers.Entities )
			: base( priority, layer ) {
			GraphicsDevice device = Main.graphics.GraphicsDevice;

			this.RT = new RenderTarget2D(
				device,
				device.PresentationParameters.BackBufferWidth,
				device.PresentationParameters.BackBufferHeight,
				false,
				device.PresentationParameters.BackBufferFormat,
				DepthFormat.Depth24
			);
		}


		////////////////

		public override void Activate( Vector2 position, params object[] args ) {
			this.Mode = OverlayMode.FadeIn;
		}

		public override void Deactivate( params object[] args ) {
			this.Mode = OverlayMode.FadeOut;
		}

		public override bool IsVisible() {
			return true;
		}


		////////////////

		public override void Update( GameTime gameTime ) {
		}


		////////////////

		public override void Draw( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;

			sb.End();

			this.DrawSceneToTarget( sb );

			sb.Begin( SpriteSortMode.Deferred,//Immediate
				BlendState.AlphaBlend,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				mymod.OverlayFX,
				Main.Transform
			);
			////mymod.OverlayFX.CurrentTechnique.Passes[0].Apply();

			sb.Draw( this.RT, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White );

			sb.End();
			sb.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.instance.Rasterizer, null, Main.Transform );
		}


		////////////////

		private void DrawSceneToTarget( SpriteBatch sb ) {
			GraphicsDevice device = Main.graphics.GraphicsDevice;

			device.SetRenderTarget( this.RT );
			device.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

			// Draw the scene
			device.Clear( Color.CornflowerBlue );

			sb.Begin( SpriteSortMode.Deferred,//Immediate
				BlendState.AlphaBlend,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				null,
				Main.Transform
			);

			this.DrawLayerScreen( sb );
			this.DrawLayerNear( sb );
			this.DrawLayerFar( sb );
			this.DrawLayerGame( sb );

			sb.End();

			// Drop the render target
			device.SetRenderTarget( null );
		}


		////////////////

		private void DrawLayerScreen( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;

			mymod.Scene.DrawSceneScreen( sb );

		}

		private void DrawLayerNear( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;

			mymod.Scene.DrawSceneNear( sb );
		}

		private void DrawLayerFar( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;

			mymod.Scene.DrawSceneFar( sb );
		}

		private void DrawLayerGame( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;

			mymod.Scene.DrawSceneGame( sb );
		}
	}
}
