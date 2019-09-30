using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Terraria;


namespace Surroundings.Scenes.Contexts.Hell {
	public class HellFlamesSceneNear : HellFlamesScene {
		public override float HorizontalTileScrollRate => 2f;

		public override float AnimationScale => 2f;



		////////////////

		public HellFlamesSceneNear() : base( SceneLayer.Near ) {
		}


		////////////////

		public override int GetSceneAnimationVerticalOffset( float yPercent, int tileRange ) {
			return (int)(yPercent * (float)tileRange * 16f);
		}
	}
}
