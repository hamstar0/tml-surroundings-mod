using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;


namespace Surroundings.Scenes.Components.Mists {
	public class MistSceneDefinition {
		public static void GenerateMists( Rectangle area, MistSceneDefinition mistScene ) {
			int mistsToAdd = Mist.CountMissingMists( mistScene.Mists, area, mistScene.MistCount );

			area.X -= 128;
			area.Y -= 64;
			area.Width += 256;
			area.Height += 128;

			for( int i = 0; i < mistsToAdd; i++ ) {
				Mist mist = Mist.AttemptCreate( mistScene.Mists, area, mistScene );
				if( mist == null ) {
					continue;
				}

				mist.Scale = mistScene.MistScale;
				mistScene.Mists.Add( mist );
			}
		}



		////////////////

		public ISet<Mist> Mists { get; } = new HashSet<Mist>();

		////////////////

		public int MistCount { get; }
		public float SpacingSquared { get; }
		public int AboveGroundMinHeight { get; }
		public int AboveGroundMaxHeight { get; }
		public TilePattern Ground { get; }
		public Vector2 MistScale { get; }
		public float AnimationFadeTickRate { get; }
		public float AnimationPeekTickRate { get; }
		public float AnimationPeekTickRateAddedRandomRange { get; }



		////////////////

		public MistSceneDefinition( int mistCount,
				float spacingSquared,
				int aboveGroundMinHeight,
				int aboveGroundMaxHeight,
				TilePattern ground,
				Vector2 mistScale,
				float animationFadeTickRate,
				float animationPeekTickRate,
				float animationPeekTickRateAddedRandomRange ) {
			this.MistCount = mistCount;
			this.SpacingSquared = spacingSquared;
			this.AboveGroundMinHeight = aboveGroundMinHeight;
			this.AboveGroundMaxHeight = aboveGroundMaxHeight;
			this.Ground = ground;
			this.MistScale = mistScale;
			this.AnimationFadeTickRate = animationFadeTickRate;
			this.AnimationPeekTickRate = animationPeekTickRate;
			this.AnimationPeekTickRateAddedRandomRange = animationPeekTickRateAddedRandomRange;
		}


		////////////////

		public void Update() {
			foreach( Mist mist in this.Mists.ToArray() ) {
				mist.Update();

				if( !mist.IsActive ) {
					this.Mists.Remove( mist );
				}
			}
		}


		////////////////

		public void DrawAll( SpriteBatch sb, Color color ) {
			foreach( Mist mist in this.Mists ) {
				mist.Draw( sb, color );
			}
		}
	}
}
