using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace Surroundings.Scenes.Contexts.CavernRock {
	public class CavernRockSceneBottomNear : CavernRockSceneBottom {
		public override Vector2 FrameSize {
			get {
				Texture2D tex = this.GetSceneTexture();
				return new Vector2( (float)tex.Width * 1.5f, (float)tex.Height * 1.5f );
			}
		}

		public override float HorizontalTileScrollRate { get; } = 1.9f;



		////////////////

		public CavernRockSceneBottomNear() : base( SceneLayer.Near ) {
		}
	}
}
