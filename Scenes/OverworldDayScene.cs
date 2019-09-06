using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes {
	public class OverworldDayScene : Scene {
		public override bool CanHorizontalScroll( SceneLayer layer ) {
			return layer == SceneLayer.Near;
		}

		public override bool CanVerticalScroll( SceneLayer layer ) {
			return false;
		}


		////////////////

		public override void Draw( SpriteBatch sb, Rectangle destination ) {
			Main.instance.LoadBackground( 11 );

			sb.Draw( Main.backgroundTexture[11], destination, null, Color.White );
		}


		public override SceneContext GetContext() {
			return new SceneContext {
				Layer = SceneLayer.Near,
				//IsDay = true,
				VanillaBiome = VanillaBiome.Forest
			};
		}
	}
}
