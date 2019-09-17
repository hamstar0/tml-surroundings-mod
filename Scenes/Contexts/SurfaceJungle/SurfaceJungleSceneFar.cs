using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceJungle {
	public class SurfaceJungleSceneFar : SurfaceJungleScene {
		public override Vector2 SceneScale => new Vector2( 2.5f );

		public override float HorizontalTileScrollRate => 2f;



		////////////////

		public SurfaceJungleSceneFar() : base( SceneLayer.Far ) {
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
