using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Terraria;


namespace Surroundings.Scenes.Contexts.Hell {
	public class HellFlamesSceneNear : HellFlamesScene {
		public override float HorizontalTileScrollRate => 2f;

		public override Vector2 Scale { get; } = new Vector2( 3f );

		////

		public override int NeededAnimationsQuantity => 4;
		public override int TickDurationBetweenNewAnimations => 3;



		////////////////

		public HellFlamesSceneNear() : base( SceneLayer.Near ) {
		}
	}
}
