using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceCorruption {
	public class SurfaceCorruptionSceneFar : SurfaceCorruptionScene {
		public override Vector2 FrameSize {
			get {
				Texture2D tex = this.GetSceneTexture();
				return new Vector2( (float)tex.Width, (float)tex.Height );
			}
		}

		public override float HorizontalTileScrollRate { get; } = 1.65f;



		////////////////

		public SurfaceCorruptionSceneFar() : base( SceneLayer.Far ) {
		}


		////////////////

		public override int GetSceneTextureVerticalOffset( float yRangePercent, int texHeight ) {
			return base.GetSceneTextureVerticalOffset( yRangePercent, texHeight ) + 72;//- 32;
		}
	}
}
