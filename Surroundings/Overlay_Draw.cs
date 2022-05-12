using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.TModLoader;


namespace Surroundings {
	partial class SurroundingsOverlay : Overlay {
		public override void Draw( SpriteBatch sb ) {
			var config = SurroundingsConfig.Instance;
			if( config?.Get<bool>( nameof(config.EnableOverlays) ) != true ) {
				return;
			}
			if( SurroundingsMod.Instance.HideOverlays ) {
				return;
			}
			if( Lighting.lightMode >= 2 ) {
				return;
			}
			if( !LoadLibraries.IsWorldBeingPlayed() ) {
				return;
			}

			//

			sb.End();

			//

			RenderTarget2D overlayRT = this.RenderMngr.GetSceneRT();

			this.DrawAllScenesToTarget( sb, overlayRT, out RenderTarget2D oldRT );

			//

			Vector2 oldZoom = Main.GameViewMatrix.Zoom;
			Main.GameViewMatrix.Zoom = Vector2.One;
			
			//

			if( oldRT != null ) {
				this.DrawOldScene( sb, oldRT );
			}
			
			this.DrawOverlay( sb, overlayRT );

			//

			Main.GameViewMatrix.Zoom = oldZoom;

			//

			sb.Begin(
				SpriteSortMode.Immediate,
				BlendState.AlphaBlend,
				SamplerState.LinearClamp,
				DepthStencilState.Default,
				RasterizerState.CullNone,
				null,
				Main.Transform
			);
		}


		////////////////

		private void DrawOldScene( SpriteBatch sb, RenderTarget2D oldRT ) {
			sb.Begin(
				SpriteSortMode.Deferred,
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
			sb.Begin(
				SpriteSortMode.Immediate,//Deferred
				BlendState.AlphaBlend,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				SurroundingsMod.Instance.OverlayFX,
				Main.Transform
			);
			/*sb.Begin(
				SpriteSortMode.Immediate,
				BlendState.AlphaBlend,
				SamplerState.LinearClamp,
				DepthStencilState.Default,
				RasterizerState.CullNone,
				SurroundingsMod.Instance.OverlayFX,
				Main.Transform
			);*/

			////mymod.OverlayFX.CurrentTechnique.Passes[0].Apply();

			sb.Draw( overlay, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White );

			sb.End();
		}


		////////////////

		private void DrawClear( SpriteBatch sb ) {
			sb.Begin(
				SpriteSortMode.Immediate,//Deferred
				BlendState.AlphaBlend,//NonPremultiplied,
				Main.DefaultSamplerState,
				DepthStencilState.None,
				Main.instance.Rasterizer,
				null,
				Main.Transform
			);

			/*sb.Draw(
				Main.magicPixel,
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
