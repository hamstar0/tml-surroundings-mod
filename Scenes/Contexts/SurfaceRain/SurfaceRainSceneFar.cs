using System;
using HamstarHelpers.Helpers.Debug;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceRain {
	public class SurfaceRainSceneFar : SurfaceRainScene {
		public override float HorizontalTileScrollRate => 1.5f;



		////////////////

		protected SurfaceRainSceneFar() : base( false ) {
		}
	}
}
