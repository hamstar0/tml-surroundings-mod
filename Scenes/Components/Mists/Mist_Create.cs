using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using System;
using Terraria;


namespace Surroundings.Scenes.Components.Mists {
	public partial class Mist {
		public static Mist AttemptCreate( Rectangle worldArea, MistSceneDefinition mistDef ) {
			int x = Main.rand.Next( worldArea.X, worldArea.X + worldArea.Width );
			int y = Main.rand.Next( worldArea.Y, worldArea.Y + worldArea.Height );
			var worldPos = new Vector2( x, y );

			if( mistDef.Ground.Check( x >> 4, y >> 4 ) ) {
				return null;
			}

			Vector2 groundPos;
			if( !WorldHelpers.DropToGround( worldPos, false, mistDef.Ground, ( y >> 4 ) + 42, out groundPos ) ) {
				return null;
			}

			groundPos.Y -= mistDef.AboveGroundMinHeight;
			groundPos.Y -= (int)( Main.rand.NextFloat() * (mistDef.AboveGroundMaxHeight - mistDef.AboveGroundMinHeight) );

			if( !worldArea.Contains( (int)groundPos.X, (int)groundPos.Y ) ) {
				return null;
			}

			foreach( Mist existingMistDef in mistDef.Mists ) {
				// Avoid other mists
				if( Vector2.DistanceSquared(groundPos, existingMistDef.WorldPosition) < mistDef.SpacingSquared ) {
					return null;
				}
			}

			int fadeDuration = mistDef.AnimationFadeTickDuration;
			int peekDuration = mistDef.AnimationPeekTickDuration;
			peekDuration += (int)(Main.rand.NextFloat() * (float)mistDef.AnimationPeekAddedRandomTickDurationRange);

			Vector2 drift = Mist.GetWindDrift();

			var mist = new Mist( groundPos, drift, fadeDuration, peekDuration );

			return mist;
		}
	}
}
