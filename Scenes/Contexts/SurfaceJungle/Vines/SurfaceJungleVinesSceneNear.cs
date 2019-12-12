﻿using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceJungle {
	public class SurfaceJungleVinesSceneNear : SurfaceJungleVinesScene {
		public override Vector2 FrameSize => new Vector2( Main.screenWidth, Main.screenHeight ) * 2.25f;

		public override float HorizontalTileScrollRate => 2.25f;

		public override float VerticalTileScrollRate => 2.25f;



		////////////////

		public SurfaceJungleVinesSceneNear() : base( SceneLayer.Near ) {
		}
	}
}
