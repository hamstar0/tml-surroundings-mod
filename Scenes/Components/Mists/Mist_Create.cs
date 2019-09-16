using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;


namespace Surroundings.Scenes.Components.Mists {
	public partial class Mist {
		public static void ApplyMists( ref ISet<Mist> mists,
					Rectangle area,
					int mistCount,
					float spacingSquared,
					int aboveGroundMinHeight,
					int aboveGroundMaxHeight,
					TilePattern ground,
					Vector2 mistScale,
					float animationDurationMultiplier,
					float animationDurationMultiplierAddedRandomRange ) {
			int mistsToAdd = Mist.CountMissingMists( mists, area, mistCount );

			area.X -= 128;
			area.Width += 256;

			for( int i = 0; i < mistsToAdd; i++ ) {
				float animRate = (Main.rand.NextFloat() * animationDurationMultiplierAddedRandomRange ) +
					animationDurationMultiplier;

				Mist mist = Mist.AttemptCreate( mists,
					area,
					spacingSquared,
					aboveGroundMinHeight,
					aboveGroundMaxHeight,//6 * 16,
					ground,
					animRate
				);

				if( mist != null ) {
					mist.Scale = mistScale;
					mists.Add( mist );
				}
			}
		}


		////////////////

		public static Mist AttemptCreate( IEnumerable<Mist> existingMists,
				Rectangle worldArea,
				float spacingSquared,
				int aboveGroundMinHeight,
				int aboveGroundMaxHeight,
				TilePattern ground,
				float animationDurationMultiplier ) {
			int x = Main.rand.Next( worldArea.X, worldArea.X + worldArea.Width );
			int y = Main.rand.Next( worldArea.Y, worldArea.Y + worldArea.Height );
			var worldPos = new Vector2( x, y );

			if( ground.Check( x >> 4, y >> 4 ) ) {
				return null;
			}

			Vector2 groundPos;
			if( !WorldHelpers.DropToGround( worldPos, false, ground, ( y >> 4 ) + 42, out groundPos ) ) {
				return null;
			}

			groundPos.Y -= aboveGroundMinHeight;
			groundPos.Y -= (int)( Main.rand.NextFloat() * ( aboveGroundMaxHeight - aboveGroundMinHeight ) );

			if( !worldArea.Contains( (int)groundPos.X, (int)groundPos.Y ) ) {
				return null;
			}

			foreach( Mist existingMistDef in existingMists ) {
				// Avoid other mists
				if( Vector2.DistanceSquared( groundPos, existingMistDef.WorldPosition ) < spacingSquared ) {
					return null;
				}
			}

			Vector2 drift = Mist.GetWindDrift();

			var mistDef = new Mist( groundPos, drift, animationDurationMultiplier );
			mistDef.WorldPosition = groundPos;

			return mistDef;
		}
	}
}
