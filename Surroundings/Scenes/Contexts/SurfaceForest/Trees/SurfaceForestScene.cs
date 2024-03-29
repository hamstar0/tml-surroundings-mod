﻿using System;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceForest {
	public abstract class SurfaceForestScene : Scene {
		public override SceneContext Context { get; }

		////

		public override int DrawPriority { get; } = 1;

		////

		public override float VerticalTileScrollRate { get; } = 0f;



		////////////////

		protected SurfaceForestScene( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,//true,
				currentEvent: null,
				anyOfBiome: new VanillaBiome[] { VanillaBiome.Forest },
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.Overworld },
				customCondition: null
			);
			this.Context.Lock();
		}


		////////////////

		public override Color GetSceneColor( SceneDrawData drawData ) {
			float shadeScale = 192f * drawData.Brightness;  //( this.IsNear ? 192f : 255f ) * brightness;
			byte shade = (byte)Math.Min( shadeScale, 255 );

			return new Color( shade, shade, shade, 255 );
		}

		public override float GetSceneOpacity( SceneDrawData drawData ) {
			return Scene.GetSurfaceOpacity(drawData) * drawData.Opacity;
		}

		public Texture2D GetSceneTexture() {
			//int frontTexIdx = 17;
			//int backTexIdx = 92;//11;

			//Main.instance.LoadBackground( frontTexIdx );
			//Main.instance.LoadBackground( backTexIdx );

			//frontTex = Main.backgroundTexture[frontTexIdx];
			return SurroundingsMod.Instance.GetTexture( "Scenes/Contexts/SurfaceForest/Trees/SurfaceForest" );
			//backTex = Main.backgroundTexture[backTexIdx];
		}

		////////////////

		public float GetSceneVerticalRangePercent( Vector2 origin ) {
			int plrTileY = (int)( origin.Y / 16 );
			float range = WorldLocationLibraries.SurfaceLayerBottomTileY - WorldLocationLibraries.SurfaceLayerTopTileY;
			float yPercent = (float)( plrTileY - WorldLocationLibraries.SurfaceLayerTopTileY ) / range;
			return 1f - yPercent;
		}

		public int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			int offset = (int)( yPercent * (float)texHeight * 0.3f );
			//offset += 64 + SurroundingsMod.Instance.DebugOverlayOffset;
			offset += (Main.screenHeight - 704) + SurroundingsMod.Instance.DebugOverlayOffset;
			offset += 48;

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

			Color color = this.GetSceneColor(drawData) * this.GetSceneOpacity(drawData);
			//Color frontColor = backColor;
			//frontColor.R = (byte)((float)frontColor.R * 0.75f);
			//frontColor.B = 0;
			//frontColor.G = (byte)((float)(frontColor.G/2) * 0.75f);

			float yPercent = this.GetSceneVerticalRangePercent( drawData.Center );
			rect.Y += this.GetSceneTextureVerticalOffset( yPercent, rect.Height );

			if( SurroundingsConfig.Instance.DebugModeSceneInfo ) {
				DebugLibraries.Print( this.GetType().Name + "_" + this.Context.Layer,
					"brightness: " + drawData.Brightness.ToString("N2") +
					", wall%: " + drawData.WallPercent.ToString("N2") +
					", opacity: " + drawData.Opacity.ToString("N2") +
					", color: " + color.ToString() +
					", rect: " + rect,
					20
				);
			}

			sb.Draw( tex, rect, null, color );

			// I want to try to get drawDepth working at some point:
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
