using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace Surroundings.Scenes.Contexts.CavernSnow {
	public class CavernSnowSceneBottomFar : CavernSnowSceneBottom {
		public override Vector2 FrameSize {
			get {
				Texture2D tex = this.GetSceneTexture();
				return new Vector2( (float)tex.Width, (float)tex.Height );
			}
		}

		public override float HorizontalTileScrollRate { get; } = 1.65f;



		////////////////

		public CavernSnowSceneBottomFar() : base( SceneLayer.Far ) {
		}
	}
}
