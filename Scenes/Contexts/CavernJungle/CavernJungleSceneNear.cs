using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Terraria;


namespace Surroundings.Scenes.Contexts.CavernJungle {
	public class CavernJungleSceneNear : CavernJungleScene {
		public override Vector2 FrameSize => new Vector2( Main.screenWidth, Main.screenHeight ) * 2f;

		public override float HorizontalTileScrollRate => 2f;

		public override float VerticalTileScrollRate => 2f;



		////////////////

		public CavernJungleSceneNear() : base( SceneLayer.Near ) {
		}
	}
}
