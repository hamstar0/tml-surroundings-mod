﻿using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceForest {
	public class SurfaceForestScene : Scene {
		private bool IsNear;


		////////////////

		public override SceneContext Context { get; }

		////

		public override int DrawPriority => 1;

		////

		public override Vector2 SceneScale => this.IsNear ?
			new Vector2( 3.5f, 3.5f ) :
			new Vector2( 2.5f, 2.5f );

		public override float HorizontalTileScrollRate => this.IsNear ? 2f : 1.5f;

		public override float VerticalTileScrollRate => 0f;

		////

		public override MistSceneDefinition MistDefinition => null;



		////////////////

		public SurfaceForestScene( bool isNear ) {
			this.IsNear = isNear;
			this.Context = new SceneContext {
				Layer = this.IsNear ? SceneLayer.Near : SceneLayer.Far,
				//IsDay = true,
				VanillaBiome = VanillaBiome.Forest
			};
		}


		////////////////

		public Texture2D GetSceneTexture() {
			//int frontTexIdx = 17;
			//int backTexIdx = 92;//11;

			//Main.instance.LoadBackground( frontTexIdx );
			//Main.instance.LoadBackground( backTexIdx );

			//frontTex = Main.backgroundTexture[frontTexIdx];
			return SurroundingsMod.Instance.GetTexture( "Scenes/SurfaceForest" );
			//backTex = Main.backgroundTexture[backTexIdx];
		}

		public override Color GetSceneColor( SceneDrawData drawData ) {
			//float shadeScale = ( this.IsNear ? 192f : 255f ) * brightness;
			float shadeScale = 192f * drawData.Brightness;
			byte shade = (byte)Math.Min( shadeScale, 255 );
			float cavePercent = Math.Max( drawData.WallPercent - 0.6f, 0f ) * 2.5f;

			var color = new Color( shade, shade, shade, 255 );

			return color * ( 1f - cavePercent ) * drawData.Opacity;
		}

		////////////////

		public float GetSceneVerticalRangePercent( Vector2 origin ) {
			int plrTileY = (int)( origin.Y / 16 );
			float range = WorldHelpers.SurfaceLayerBottom - WorldHelpers.SurfaceLayerTop;
			float yPercent = (float)( plrTileY - WorldHelpers.SurfaceLayerTop ) / range;
			return 1f - yPercent;
		}

		public int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			var mymod = SurroundingsMod.Instance;
			float height = (float)texHeight * this.SceneScale.Y;

			int offset = (int)( yPercent * (float)height * 0.3f );
			offset += -128;
			return offset;
		}


		////////////////

		public override void Draw(
				SpriteBatch sb,
				Rectangle rect,
				SceneDrawData drawData,
				float drawDepth ) {
			var mymod = SurroundingsMod.Instance;
			Texture2D tex = this.GetSceneTexture();

			Color color = this.GetSceneColor( drawData );
			//Color frontColor = backColor;
			//frontColor.R = (byte)((float)frontColor.R * 0.75f);
			//frontColor.B = 0;
			//frontColor.G = (byte)((float)(frontColor.G/2) * 0.75f);

			if( mymod.Config.DebugModeInfo ) {
				DebugHelpers.Print( "SurfaceForestScene",
					"brightness: " + drawData.Brightness.ToString("N2") +
					", wall%: " + drawData.WallPercent.ToString("N2") +
					", opacity: " + drawData.Opacity.ToString("N2") +
					", color: " + color.ToString(),
					20
				);
			}

			float yPercent = this.GetSceneVerticalRangePercent( drawData.Center );
			float scale = rect.Width / tex.Width;

			rect.Height = (int)((float)tex.Height * scale);
			rect.Y += this.GetSceneTextureVerticalOffset( yPercent, tex.Height ) + 192;
			rect.Y -= this.IsNear ? 0 : -128;

			sb.Draw( tex, rect, null, color );

			// I want to try to get drawDepth working at some point:
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}