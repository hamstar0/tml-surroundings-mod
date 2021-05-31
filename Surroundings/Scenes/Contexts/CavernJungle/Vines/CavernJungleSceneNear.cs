using System;
using ModLibsCore.Libraries.Debug;
using Microsoft.Xna.Framework;
using Terraria;


namespace Surroundings.Scenes.Contexts.CavernJungle {
	public class CavernJungleSceneNear : CavernJungleScene {
		public override Vector2 FrameSize => new Vector2( Main.screenWidth, Main.screenHeight ) * 2.25f;

		public override float HorizontalTileScrollRate => 2.25f;

		public override float VerticalTileScrollRate => 2.25f;



		////////////////

		public CavernJungleSceneNear() : base( SceneLayer.Near ) {
		}
	}
}
