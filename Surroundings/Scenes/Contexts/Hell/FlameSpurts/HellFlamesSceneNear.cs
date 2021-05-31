using System;
using ModLibsCore.Libraries.Debug;
using Microsoft.Xna.Framework;
using Terraria;


namespace Surroundings.Scenes.Contexts.Hell {
	public class HellFlamesSceneNear : HellFlamesScene {
		public override float HorizontalTileScrollRate => 2f;

		public override Vector2 Scale { get; } = new Vector2( 3f );

		////

		public override int NeededAnimationsQuantity => 3;
		public override int TickDurationBetweenNewAnimations => 4;



		////////////////

		public HellFlamesSceneNear() : base( SceneLayer.Near ) {
		}
	}
}
