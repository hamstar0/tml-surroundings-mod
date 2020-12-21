using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace Surroundings.Scenes.Contexts.CavernSnow {
	public class CavernSnowSceneBottomNear : CavernSnowSceneBottom {
		public override Vector2 FrameSize {
			get {
				Texture2D tex = this.GetSceneTexture();
				return new Vector2( (float)tex.Width * 1.5f, (float)tex.Height * 1.5f );
			}
		}

		public override float HorizontalTileScrollRate { get; } = 1.9f;



		////////////////

		public CavernSnowSceneBottomNear() : base( SceneLayer.Near ) {
		}
	}
}
