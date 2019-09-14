using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;


namespace Surroundings.Scenes.Components {
	public partial class MistDefinition {
		public static Vector2 GetWindDrift() {
			return new Vector2( Main.windSpeedSet, 0f );
		}


		public static Texture2D GetRandomCloudTexture() {
			switch( Main.rand.Next(0, 7) ) {
			case 0:
				return Main.cloudTexture[2];
			case 1:
				return Main.cloudTexture[3];
			case 2:
				return Main.cloudTexture[14];
			case 3:
				return Main.cloudTexture[15];
			case 4:
				return Main.cloudTexture[16];
			case 5:
				return Main.cloudTexture[17];
			case 6:
			default:
				return Main.cloudTexture[21];
			}
		}


		////////////////

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
	}
}
