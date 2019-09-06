using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;


namespace Surroundings {
	class SurroundingsOverlay : Overlay {
		public Vector2 TargetPosition = Vector2.Zero;



		////////////////

		public SurroundingsOverlay(
				EffectPriority priority = EffectPriority.VeryHigh,
				RenderLayers layer = RenderLayers.Entities )
			: base( priority, layer ) { }


		////////////////

		public override void Activate( Vector2 position, params object[] args ) {
			this.TargetPosition = position;
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

			//sb.End();

			this.DrawLayerScreen( sb );
			this.DrawLayerNear( sb );
			this.DrawLayerFar( sb );
			this.DrawLayerGame( sb );

			//sb.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.instance.Rasterizer, null, Main.Transform );
		}


		////////////////

		private void DrawLayerScreen( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;
			Effect fx = mymod.OverlayScreen;

			//sb.Begin( SpriteSortMode.Immediate,
			//	BlendState.AlphaBlend,
			//	Main.DefaultSamplerState,
			//	DepthStencilState.None,
			//	Main.instance.Rasterizer,
			//	fx,
			//	Main.Transform
			//);
			////fx.CurrentTechnique.Passes[0].Apply();

			mymod.Scene.DrawSceneScreen( sb );

			//sb.End();
		}

		private void DrawLayerNear( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;
			Effect fx = mymod.OverlayNear;

			//sb.Begin( SpriteSortMode.Immediate,
			//	BlendState.AlphaBlend,
			//	Main.DefaultSamplerState,
			//	DepthStencilState.None,
			//	Main.instance.Rasterizer,
			//	fx,
			//	Main.Transform
			//);
			////fx.CurrentTechnique.Passes[0].Apply();

			mymod.Scene.DrawSceneNear( sb );

			//sb.End();
		}

		private void DrawLayerFar( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;
			Effect fx = mymod.OverlayFar;

			//sb.Begin( SpriteSortMode.Immediate,
			//	BlendState.AlphaBlend,
			//	Main.DefaultSamplerState,
			//	DepthStencilState.None,
			//	Main.instance.Rasterizer,
			//	fx,
			//	Main.Transform
			//);
			////fx.CurrentTechnique.Passes[0].Apply();

			mymod.Scene.DrawSceneFar( sb );

			//sb.End();
		}

		private void DrawLayerGame( SpriteBatch sb ) {
			var mymod = SurroundingsMod.Instance;
			Effect fx = mymod.OverlayGame;

			//sb.Begin( SpriteSortMode.Immediate,
			//	BlendState.AlphaBlend,
			//	Main.DefaultSamplerState,
			//	DepthStencilState.None,
			//	Main.instance.Rasterizer,
			//	fx,
			//	Main.Transform
			//);
			////fx.CurrentTechnique.Passes[0].Apply();

			mymod.Scene.DrawSceneGame( sb );

			//sb.End();
		}
	}
}
