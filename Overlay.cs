using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;


namespace Surroundings {
	class SurroundingsOverlay : Overlay {
		private RenderTarget2D RT;
		private Color[] OldRTData = null;



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
			this.RT = new RenderTarget2D(
				device,
				Main.screenWidth,   //device.PresentationParameters.BackBufferWidth,
				Main.screenHeight,  //device.PresentationParameters.BackBufferHeight,
				false,
				device.PresentationParameters.BackBufferFormat,
				DepthFormat.Depth24
			);
			this.OldRTData = new Color[ Main.screenWidth * Main.screenHeight ];
		}

		public override void Deactivate( params object[] args ) {
			this.Mode = OverlayMode.FadeOut;
			this.RT.Dispose();
		}

		////////////////

		public override bool IsVisible() {
			return true;
		}


		////////////////

		private Color[] GetBuffer() {
			if( this.OldRTData == null || this.OldRTData.Length != (Main.screenWidth * Main.screenHeight) ) {
				this.OldRTData = new Color[Main.screenWidth * Main.screenHeight];
			}
			return this.OldRTData;
		}
		

		////////////////

		public override void Update( GameTime gameTime ) {
		}


		////////////////

		public override void Draw( SpriteBatch sb ) {
			sb.End();

			Color[] oldSceneData = this.DrawSceneToTarget( sb );
			if( oldSceneData != null ) {
				this.DrawOldScene( sb, oldSceneData );
			}

			this.DrawOverlay( sb, this.RT );

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

		private Color[] DrawSceneToTarget( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;
			GraphicsDevice device = Main.graphics.GraphicsDevice;

			RenderTargetBinding[] rtBindings = device.GetRenderTargets();
			RenderTarget2D existingRT = rtBindings.Length > 0 ?
				(RenderTarget2D)device.GetRenderTargets()[0].RenderTarget :
				null;

			device.SetRenderTarget( this.RT );
			device.Clear( Color.Transparent );

			Color[] oldData = null;
			if( existingRT != null ) {
				oldData = this.GetBuffer();
				existingRT.GetData( oldData );
			}

			sb.Begin( SpriteSortMode.Deferred,//Immediate
				BlendState.AlphaBlend,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				null,
				Main.Transform
			);

			sb.Draw( Main.magicPixel, new Rectangle(0,0,Main.screenWidth,Main.screenHeight), Color.Transparent );
			mymod.Scene.DrawSceneScreen( sb );
			mymod.Scene.DrawSceneNear( sb );
			mymod.Scene.DrawSceneFar( sb );
			mymod.Scene.DrawSceneGame( sb );

			sb.End();

			device.SetRenderTarget( existingRT );

			return oldData;
		}


		private void DrawOldScene( SpriteBatch sb, Color[] oldSceneData ) {
			sb.Begin( SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				null,
				Main.Transform
			);

			if( oldSceneData != null ) {
				var rect = new Rectangle( 0, 0, Main.screenWidth, Main.screenHeight );
				var oldTex = new Texture2D( Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight );
				oldTex.SetData( oldSceneData );

				sb.Draw( oldTex, rect, Color.White );
			}

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
