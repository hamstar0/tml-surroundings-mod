using System;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;
using Microsoft.Xna.Framework;
using Surroundings.Scenes.Components;
using Surroundings.Scenes.Components.FlameSpurt;
using Terraria;


namespace Surroundings.Scenes.Contexts.Hell {
	public abstract class HellFlamesScene : AnimationsScene {
		public abstract Vector2 Scale { get; }


		////

		public override SceneContext Context { get; }



		////////////////

		public override Color GetSceneColor( SceneDrawData drawData ) {
			return Color.White * drawData.Opacity * 0.5f;	// Is this working?
		}


		////////////////

		public HellFlamesScene( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,
				currentEvent: null,
				anyOfBiome: null,
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.Hell },
				customCondition: null
			);
			this.Context.Lock();
		}


		////////////////

		public override Animator CreateAnimation( int worldX, int worldY ) {
			return new FlameSpurtAnimator( worldX, this.Scale );
		}
	}
}
