using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace Surroundings.Scenes.Contexts.SurfaceJungle {
	public class SurfaceJungleTreeSceneNear : SurfaceJungleScene {
		public override Vector2 FrameSize {
			get {
				Texture2D tex = this.GetSceneTexture();
				return new Vector2( (float)tex.Width * 1.5f, (float)tex.Height * 1.5f );
			}
		}

		public override float HorizontalTileScrollRate { get; } = 2.5f; //1.9f;



		////////////////

		public SurfaceJungleTreeSceneNear() : base( SceneLayer.Near ) {
		}
	}
}
