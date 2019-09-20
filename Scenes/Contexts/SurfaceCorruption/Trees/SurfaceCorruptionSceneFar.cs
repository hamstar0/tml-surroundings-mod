using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceHallow {
	public class SurfaceCorruptionSceneFar : SurfaceCorruptionScene {
		public override Vector2 FrameSize {
			get {
				Texture2D tex = this.GetSceneTexture();
				return new Vector2( (float)tex.Width, (float)tex.Height );
			}
		}

		public override float HorizontalTileScrollRate { get; } = 1.5f;



		////////////////

		public SurfaceCorruptionSceneFar() : base( SceneLayer.Far ) {
		}


		////////////////

		public override void Draw(
				SpriteBatch sb,
				Rectangle rect,
				SceneDrawData drawData,
				float drawDepth ) {
			//rect.Y -= 128 + SurroundingsMod.Instance.DebugOverlayOffset;
			base.Draw( sb, rect, drawData, drawDepth );
		}
	}
}
