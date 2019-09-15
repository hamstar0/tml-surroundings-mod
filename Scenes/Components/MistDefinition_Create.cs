using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;


namespace Surroundings.Scenes.Components {
	public partial class MistDefinition {
		public static void ApplyMists( ref ISet<MistDefinition> mists,
					Rectangle area,
					int mistCount,
					TilePattern ground,
					Vector2 mistScale ) {
			int mistsToAdd = MistDefinition.CountMissingMists( mists, area, mistCount );

			for( int i = 0; i < mistsToAdd; i++ ) {
				area.X -= 128;
				area.Width += 256;

				float animRate = ( Main.rand.NextFloat() * 5f ) + 2f;

				MistDefinition mist = MistDefinition.AttemptCreate( mists,
					area,
					4096f,
					0,
					6 * 16,
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

		public static MistDefinition AttemptCreate( IEnumerable<MistDefinition> existingMists,
				Rectangle worldArea,
				float densitySquared,
				int aboveGroundMinimumHeight,
				int aboveGroundMaximumHeight,
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

			groundPos.Y -= aboveGroundMinimumHeight;
			groundPos.Y -= (int)( Main.rand.NextFloat() * ( aboveGroundMaximumHeight - aboveGroundMinimumHeight ) );

			if( !worldArea.Contains( (int)groundPos.X, (int)groundPos.Y ) ) {
				return null;
			}

			foreach( MistDefinition existingMistDef in existingMists ) {
				// Avoid other mists
				if( Vector2.DistanceSquared( groundPos, existingMistDef.WorldPosition ) < densitySquared ) {
					return null;
				}
			}

			Vector2 drift = MistDefinition.GetWindDrift();

			var mistDef = new MistDefinition( groundPos, drift, animationDurationMultiplier );
			mistDef.WorldPosition = groundPos;

			return mistDef;
		}
	}
}
