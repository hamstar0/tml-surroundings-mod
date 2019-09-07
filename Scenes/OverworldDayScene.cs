using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes {
	public class OverworldDayScene : Scene {
		private bool IsNear;


		////////////////

		public override Vector2 Scale => this.IsNear ? new Vector2(3f,3f) : new Vector2(1f, 1f);

		public override bool CanHorizontalTile => true;

		public override bool CanVerticalTile => false;

		public override SceneContext GetContext => new SceneContext {
			Layer = this.IsNear ? SceneLayer.Near : SceneLayer.Far,
			//IsDay = true,
			VanillaBiome = VanillaBiome.Forest
		};



		////////////////

		public OverworldDayScene( bool isNear ) {
			this.IsNear = isNear;
		}

		////////////////

		public override void Draw( SpriteBatch sb, Rectangle rect, float depth ) {
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

			Color color = Color.White;
			//color.A = 192;
			color.R = (byte)((float)color.R * brightness);
			color.G = (byte)((float)color.G * brightness);
			color.B = (byte)((float)color.B * brightness);

			int plrTileY = (int)(brightnessCheckPoint.Y / 16);
			float range = WorldHelpers.SurfaceLayerBottom - WorldHelpers.SurfaceLayerTop;
			float yPercent = (float)(plrTileY - WorldHelpers.SurfaceLayerTop) / range;
			yPercent = 1f - yPercent;

			Texture2D tex = Main.backgroundTexture[11];

			float scale = (this.Scale.Y - 1f) * 0.33f;
			scale += 1f;

			rect.Y += (int)(yPercent * tex.Height * scale);

			sb.Draw( tex, rect, null, color );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
