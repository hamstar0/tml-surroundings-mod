﻿using System;
using ModLibsCore.Libraries.Debug;
using Microsoft.Xna.Framework;
using Terraria;


namespace Surroundings.Scenes.Contexts.CavernJungle {
	public class CavernJungleSceneFar : CavernJungleScene {
		public override Vector2 FrameSize => new Vector2( Main.screenWidth, Main.screenHeight );

		public override float HorizontalTileScrollRate => 1.5f;

		public override float VerticalTileScrollRate => 1.5f;



		////////////////

		public CavernJungleSceneFar() : base( SceneLayer.Near ) {
		}
	}
}
