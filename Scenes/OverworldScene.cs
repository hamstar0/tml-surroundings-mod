﻿using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Tiles;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes {
	public class OverworldScene : Scene {
		private bool IsNear;


		////////////////

		public override Vector2 Scale => this.IsNear ? new Vector2(3.5f, 3.5f) : new Vector2(1f, 1f);

		public override float HorizontalTileScrollRate => this.IsNear ? 1f : 0.25f;

		public override float VerticalTileScrollRate => 0f;

		public override SceneContext GetContext => new SceneContext {
			Layer = this.IsNear ? SceneLayer.Near : SceneLayer.Far,
			//IsDay = true,
			VanillaBiome = VanillaBiome.Forest
		};



		////////////////

		public OverworldScene( bool isNear ) {
			this.IsNear = isNear;
		}

		////////////////

		public override void Draw( SpriteBatch sb, Rectangle rect, float depth ) {
			var mymod = SurroundingsMod.Instance;

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

			byte shade = (byte)Math.Min( (this.IsNear ? 192f : 255f) * brightness, 255 );
			var color = new Color( shade, shade, shade, 255 );

			if( mymod.Config.DebugModeInfo ) {
				DebugHelpers.Print( "OverworldDayScene_" + (this.IsNear ? "Near" : "Far"),
					"Brightness: " + brightness + ", color: "+color.ToString(),
					20
				);
			}

			int plrTileY = (int)(brightnessCheckPoint.Y / 16);
			float range = WorldHelpers.SurfaceLayerBottom - WorldHelpers.SurfaceLayerTop;
			float yPercent = (float)(plrTileY - WorldHelpers.SurfaceLayerTop) / range;
			yPercent = 1f - yPercent;

			Texture2D tex = Main.backgroundTexture[11];

			float scale = (this.Scale.Y - 1f) * 0.5f;
			scale += 1f;

			rect.Y += (int)(yPercent * (float)tex.Height * scale);
			rect.Y += 580 - (int)((float)300 * scale);

			sb.Draw( tex, rect, null, color );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
