using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using System;
using Terraria;


namespace Surroundings.Scenes.Components.Mists {
	public class MistSceneDefinition {
		public int MistCount { get; }
		public float SpacingSquared { get; }
		public int AboveGroundMinHeight { get; }
		public int AboveGroundMaxHeight { get; }
		public TilePattern Ground { get; }
		public Vector2 MistScale { get; }
		public float AnimationDurationMultiplier { get; }
		public float AnimationDurationMultiplierAddedRandomRange { get; }



		////////////////

		public MistSceneDefinition( int mistCount,
				float spacingSquared,
				int aboveGroundMinHeight,
				int aboveGroundMaxHeight,
				TilePattern ground,
				Vector2 mistScale,
				float animationDurationMultiplier,
				float animationDurationMultiplierAddedRandomRange ) {
			this.MistCount = mistCount;
			this.SpacingSquared = spacingSquared;
			this.AboveGroundMinHeight = aboveGroundMinHeight;
			this.AboveGroundMaxHeight = aboveGroundMaxHeight;
			this.Ground = ground;
			this.MistScale = mistScale;
			this.AnimationDurationMultiplier = animationDurationMultiplier;
			this.AnimationDurationMultiplierAddedRandomRange = animationDurationMultiplierAddedRandomRange;
		}
	}
}
