using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.CavernRock {
	public class CavernRockSceneTopFar : CavernRockSceneTop {
		public override Vector2 FrameSize {
			get {
				Texture2D tex = this.GetSceneTexture();
				return new Vector2( (float)tex.Width, (float)tex.Height );
			}
		}

		public override float HorizontalTileScrollRate { get; } = 1.65f;



		////////////////

		public CavernRockSceneTopFar() : base( SceneLayer.Far ) {
		}


		////////////////

		public override int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			int offset = base.GetSceneTextureVerticalOffset( yPercent, texHeight );
			offset += 117;

			return offset;
		}
	}
}
