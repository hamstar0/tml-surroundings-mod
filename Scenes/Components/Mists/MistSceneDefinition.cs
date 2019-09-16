using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;


namespace Surroundings.Scenes.Components.Mists {
	public class MistSceneDefinition {
		public int MistCount;
		public float SpacingSquared;
		public int AboveGroundMinHeight;
		public int AboveGroundMaxHeight;
		public TilePattern Ground;
		public Vector2 MistScale;
		public float AnimationDurationMultiplier;
		public float AnimationDurationMultiplierAddedRandomRange;



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
