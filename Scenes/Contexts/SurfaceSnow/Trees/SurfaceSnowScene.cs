using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceSnow {
	public abstract class SurfaceSnowScene : Scene {
		public override SceneContext Context { get; }

		////

		public override int DrawPriority { get; } = 1;

		public override float VerticalTileScrollRate { get; } = 0f;

		////

		public override MistSceneDefinition SceneMists { get; } = null;



		////////////////

		protected SurfaceSnowScene( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,
				vanillaBiome: VanillaBiome.Jungle,
				currentEvent: null,
				regions: WorldRegionFlags.Overworld,
				customCondition: null
			);
			this.Context.Lock();
		}


		////////////////

		public Texture2D GetSceneTexture() {
			Main.instance.LoadBackground( 37 );

			return Main.backgroundTexture[37];
		}

		public override Color GetSceneColor( SceneDrawData drawData ) {
			float cavePercent = Math.Max( drawData.WallPercent - 0.6f, 0f ) * 2.5f;
			byte shade = (byte)Math.Min( 192f * drawData.Brightness, 255 );

			var color = new Color( shade, shade, shade, 255 );

			return color * (1f - cavePercent) * drawData.Opacity;
		}

		////////////////

		public float GetSceneVerticalRangePercent( Vector2 origin ) {
			int plrTileY = (int)( origin.Y / 16 );
			float range = WorldHelpers.SurfaceLayerBottomTileY - WorldHelpers.SurfaceLayerTopTileY;
			float yPercent = (float)( plrTileY - WorldHelpers.SurfaceLayerTopTileY ) / range;
			return 1f - yPercent;
		}

		public int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			int offset = (int)( yPercent * (float)texHeight * 1.25f );
			offset += 320;

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

			float yPercent = this.GetSceneVerticalRangePercent( drawData.Center );
			rect.Y += this.GetSceneTextureVerticalOffset( yPercent, rect.Height );

			if( mymod.Config.DebugModeInfo ) {
				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer,
					"brightness: " + drawData.Brightness.ToString( "N2" ) +
					", wall%: " + drawData.WallPercent.ToString( "N2" ) +
					", opacity: " + drawData.Opacity.ToString( "N2" ) +
					", color: " + color.ToString() +
					", yPercent: " + yPercent.ToString( "N2" ) +
					//", texZoom: " + texZoom.ToString( "N2" ) +
					", rect: "+ rect,
					20
				);
				//HUDHelpers.DrawBorderedRect( sb, null, Color.Gray, rect, 2 );
			}

			sb.Draw( tex, rect, null, color );

			// I want to try to get drawDepth working at some point:
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
