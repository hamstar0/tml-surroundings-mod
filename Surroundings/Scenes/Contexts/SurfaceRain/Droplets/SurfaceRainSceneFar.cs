using System;
using ModLibsCore.Libraries.Debug;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceRain {
	public class SurfaceRainSceneFar : SurfaceRainScene {
		public override float HorizontalTileScrollRate { get; } = 1.5f;



		////////////////

		protected SurfaceRainSceneFar() : base( false ) {
		}
	}
}
