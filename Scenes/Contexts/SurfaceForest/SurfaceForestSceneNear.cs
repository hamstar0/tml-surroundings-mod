using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceForest {
	public class SurfaceForestSceneNear : SurfaceForestScene {
		public override Vector2 SceneScale => new Vector2( 3.5f, 3.5f );

		public override float HorizontalTileScrollRate => 2f;



		////////////////

		public SurfaceForestSceneNear() : base( SceneLayer.Near ) { }
	}
}
