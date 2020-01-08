using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceJungle {
	public class SurfaceJungleVinesSceneFar : SurfaceJungleVinesScene {
		public override Vector2 FrameSize => new Vector2( Main.screenWidth, Main.screenHeight );

		public override float HorizontalTileScrollRate => 1.5f;

		public override float VerticalTileScrollRate => 1.5f;



		////////////////

		public SurfaceJungleVinesSceneFar() : base( SceneLayer.Near ) {
		}
	}
}
