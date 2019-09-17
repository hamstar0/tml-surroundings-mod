using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceJungle {
	public class SurfaceJungleSceneNear : SurfaceJungleScene {
		public override Vector2 SceneScale => new Vector2( 3.5f );

		public override float HorizontalTileScrollRate => 2f;

		public override float VerticalTileScrollRate => 0f;

		////

		public override MistSceneDefinition MistDefinition => null;



		////////////////

		public SurfaceJungleSceneNear() : base( SceneLayer.Near ) {
		}
	}
}
