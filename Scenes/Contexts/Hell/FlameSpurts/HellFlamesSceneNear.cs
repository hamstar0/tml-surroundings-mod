using System;
using HamstarHelpers.Helpers.Debug;
using Terraria;


namespace Surroundings.Scenes.Contexts.Hell {
	public class HellFlamesSceneNear : HellFlamesScene {
		public override float HorizontalTileScrollRate => 2f;



		////////////////

		public HellFlamesSceneNear() : base( SceneLayer.Near ) {
		}
	}
}
