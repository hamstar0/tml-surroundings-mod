using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes {
	public class OverworldDayScene : Scene {
		public override Vector2 Scale => new Vector2( 4f, 4f );

		public override bool CanHorizontalScroll => true;

		public override bool CanVerticalScroll => false;

		public override SceneContext GetContext => new SceneContext {
			Layer = SceneLayer.Near,
			//IsDay = true,
			VanillaBiome = VanillaBiome.Forest
		};


		////////////////

		public override void Draw( SpriteBatch sb, Rectangle rect ) {
			Main.instance.LoadBackground( 11 );

			Vector2 brightnessCheckPoint = Main.LocalPlayer.Center;
			int brightnessCheckTileX = (int)(brightnessCheckPoint.X * 0.0625f);
			int brightnessCheckTileY = (int)(brightnessCheckPoint.Y * 0.0625f);
			float brightness = TileWorldHelpers.GaugeBrightnessWithin(
				brightnessCheckTileX - 16,
				brightnessCheckTileY - 12,
				32,
				24
			);

			Texture2D tex = Main.backgroundTexture[11];
			rect.Y += tex.Height / 2;

			sb.Draw( tex, rect, null, Color.White * 0.85f * brightness );
		}
	}
}
