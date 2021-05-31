using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ModLibsCore.Libraries.Debug;


namespace Surroundings.Scenes.Contexts.SurfaceForest {
	public class SurfaceForestSceneNear : SurfaceForestScene {
		public override Vector2 FrameSize {
			get {
				Texture2D tex = this.GetSceneTexture();
				return new Vector2( (float)tex.Width * 3.5f, (float)tex.Height * 3.5f );
			}
		}

		public override float HorizontalTileScrollRate { get; } = 2.5f; //1.9f;



		////////////////

		public SurfaceForestSceneNear() : base( SceneLayer.Near ) { }
	}
}
