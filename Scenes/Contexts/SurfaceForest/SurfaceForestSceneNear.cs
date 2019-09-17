using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceForest {
	public class SurfaceForestSceneNear : SurfaceForestScene {
		public override Vector2 FrameSize {
			get {
				Texture2D tex = this.GetSceneTexture();
				return new Vector2( (float)tex.Width * 3.5f, (float)tex.Height * 3.5f );
			}
		}

		public override float HorizontalTileScrollRate => 2f;



		////////////////

		public SurfaceForestSceneNear() : base( SceneLayer.Near ) { }
	}
}
