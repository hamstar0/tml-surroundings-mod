using HamstarHelpers.Services.AnimatedTexture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;

namespace Surroundings.Scenes.Components {
	public class MistDefinition {
		public static int CountMissingMists( IList<MistDefinition> mists, Rectangle worldArea, int minimumMists ) {
			int foundMists = 0;

			foreach( MistDefinition mistDef in mists ) {
				int x = (int)mistDef.WorldPosition.X;
				int y = (int)mistDef.WorldPosition.Y;

				if( worldArea.Contains(x, y) ) {
					foundMists++;
				}
			}

			return minimumMists - foundMists;
		}


		public static MistDefinition Create( Rectangle worldArea ) {
			int x = Main.rand.Next( worldArea.X, worldArea.X + worldArea.Width );
			int y = Main.rand.Next( worldArea.Y, worldArea.Y + worldArea.Height );
			var mistDef = new MistDefinition( new Vector2(x, y) );

			mistDef.WorldPosition = new Vector2( x, y );

			return mistDef;
		}



		////////////////

		public AnimatedTexture Animation;
		public Vector2 WorldPosition;
		public Vector2 Velocity;



		////////////////

		public MistDefinition( Vector2 worldPos, Vector2 velocity ) {

		}


		////////////////

		public void Update() {

		}
	}
}
