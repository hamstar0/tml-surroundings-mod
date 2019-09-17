﻿using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceJungle {
	public class SurfaceJungleSceneFar : SurfaceJungleScene {
		public override Vector2 FrameSize {
			get {
				Texture2D tex = this.GetSceneTexture();
				return new Vector2( (float)tex.Width, (float)tex.Height );
			}
		}

		public override float HorizontalTileScrollRate => 1f;



		////////////////

		public SurfaceJungleSceneFar() : base( SceneLayer.Far ) {
		}


		////////////////

		public override void Draw(
				SpriteBatch sb,
				Rectangle rect,
				SceneDrawData drawData,
				float drawDepth ) {
			//rect.Y -= 128 + SurroundingsMod.Instance.DebugOverlayOffset;
			base.Draw( sb, rect, drawData, drawDepth );
		}
	}
}
