using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceCorruption {
	public class SurfaceCorruptionSceneNear : SurfaceCorruptionScene {
		public override Vector2 FrameSize {
			get {
				Texture2D tex = this.GetSceneTexture();
				return new Vector2( (float)tex.Width * 1.5f, (float)tex.Height * 1.5f );
			}
		}

		public override float HorizontalTileScrollRate { get; } = 1.9f;



		////////////////

		public SurfaceCorruptionSceneNear() : base( SceneLayer.Near ) {
		}


		////////////////

		public override int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			return base.GetSceneTextureVerticalOffset( yPercent, texHeight ) - 64;
		}
	}
}
