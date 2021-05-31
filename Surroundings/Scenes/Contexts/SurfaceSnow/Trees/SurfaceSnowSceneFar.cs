﻿using System;
using ModLibsCore.Libraries.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceSnow {
	public class SurfaceSnowSceneFar : SurfaceSnowScene {
		public override Vector2 FrameSize {
			get {
				Texture2D tex = this.GetSceneTexture();
				return new Vector2( (float)tex.Width, (float)tex.Height );
			}
		}

		public override float HorizontalTileScrollRate { get; } = 1.65f;



		////////////////

		public SurfaceSnowSceneFar() : base( SceneLayer.Far ) {
		}


		////////////////

		public override int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			return base.GetSceneTextureVerticalOffset(yPercent, texHeight) + 16;
		}
	}
}
