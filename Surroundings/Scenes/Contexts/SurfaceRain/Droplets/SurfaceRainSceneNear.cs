﻿using System;
using ModLibsCore.Libraries.Debug;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceRain {
	public class SurfaceRainSceneNear : SurfaceRainScene {
		public override float HorizontalTileScrollRate { get; } = 1.5f;



		////////////////

		protected SurfaceRainSceneNear() : base(true) {
		}
	}
}
