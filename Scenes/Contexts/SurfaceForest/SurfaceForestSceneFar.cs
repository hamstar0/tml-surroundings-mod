using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceForest {
	public class SurfaceForestSceneFar : SurfaceForestScene {
		public override Vector2 FrameSize {
			get {
				Texture2D tex = this.GetSceneTexture();
				return new Vector2( (float)tex.Width * 2.5f, (float)tex.Height * 2.5f );
			}
		}

		public override float HorizontalTileScrollRate => 1.5f;

		public override float VerticalTileScrollRate => 0f;

		////

		public override MistSceneDefinition MistDefinition => null;



		////////////////

		public SurfaceForestSceneFar() : base( SceneLayer.Far ) {
		}


		////////////////

		public override void Draw(
				SpriteBatch sb,
				Rectangle rect,
				SceneDrawData drawData,
				float drawDepth ) {
			rect.Y += 128;
			base.Draw( sb, rect, drawData, drawDepth );
		}
	}
}
