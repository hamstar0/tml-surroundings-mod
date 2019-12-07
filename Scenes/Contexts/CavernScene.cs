using System;
using System.Linq;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts {
	public abstract class CavernScene : Scene {
		public static bool IsPlainCave( SceneContext ctx ) {
			return CavernScene.IsPlainCave( ctx, false );
		}

		public static bool IsPlainCave( SceneContext ctx, bool ignoreConverts ) {
			bool isCave = ctx.AnyOfRegions
				.Any(r => ( r & WorldRegionFlags.Cave) != 0 );
//DebugHelpers.Print("IsPlainCave", ctx.AnyOfBiome != null ? string.Join(",",ctx.AnyOfBiome) : "", 20);

			if( !isCave ) {
				return false;
			}

			foreach( VanillaBiome biome in ctx.AnyOfBiome ) {
				if( !ignoreConverts ) {
					if( ( biome & VanillaBiome.Corruption ) != 0 ) {
						return false;
					}
					if( ( biome & VanillaBiome.Crimson ) != 0 ) {
						return false;
					}
					if( ( biome & VanillaBiome.Hallow ) != 0 ) {
						return false;
					}
				}
				if( ( biome & VanillaBiome.Snow ) != 0 ) {
					return false;
				}
				if( ( biome & VanillaBiome.Desert ) != 0 ) {
					return false;
				}
				if( ( biome & VanillaBiome.Jungle ) != 0 ) {
					return false;
				}
			}

			return true;
		}



		////////////////

		public override int DrawPriority { get; } = 1;

		public override float VerticalTileScrollRate { get; } = 0f;



		////////////////

		public abstract Texture2D GetSceneTexture();

		public override Color GetSceneColor( SceneDrawData drawData ) {
			byte shade = (byte)Math.Min( drawData.Brightness * drawData.Opacity * 255f, 255f );
			byte alpha = (byte)Math.Min( drawData.Opacity * 255f, 255f );
			var color = new Color( shade, shade, shade, alpha );

			return color;
		}

		public abstract float GetSublayerColorFadePercent( float yPercent );

		////////////////

		public float GetSceneVerticalRangePercent( Vector2 origin ) {
			float wldRange = 128f * 16f;
			float withinRange = origin.Y % wldRange;
			float percent = withinRange / wldRange;

			return percent;
		}

		////////////////

		public abstract int GetSceneTextureVerticalOffset( float yPercent, int frameHeight );


		////////////////

		public override void Draw(
					SpriteBatch sb,
					Rectangle frame,
					SceneDrawData drawData,
					float drawDepth ) {
			Texture2D tex = this.GetSceneTexture();

			Color color = this.GetSceneColor( drawData );

			float yPercent1 = this.GetSceneVerticalRangePercent( drawData.Center );
			float yPercent2 = yPercent1 - 0.5f;
			if( yPercent2 < 0 ) {
				yPercent2 = 1 + yPercent2;
			}

			int yOffset1 = this.GetSceneTextureVerticalOffset( yPercent1, frame.Height );
			int yOffset2 = this.GetSceneTextureVerticalOffset( yPercent2, frame.Height );
			int oldY = frame.Y;

			if( SurroundingsConfig.Instance.DebugModeSceneInfo ) {
				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer,
					"brightness: " + drawData.Brightness.ToString( "N2" ) +
					", opacity: " + drawData.Opacity.ToString( "N2" ) +
					", color: " + color.ToString() +
					//", color1: " + (color * this.GetSublayerColorFadePercent(yPercent - 1)).ToString() +
					//", color2: " + (color * this.GetSublayerColorFadePercent(yPercent)).ToString() +
					", yPercent: " + yPercent1.ToString( "N2" ) +
					", yOffset1: " + yOffset1 +
					", yOffset2: " + yOffset2 +
					//", texZoom: " + texZoom.ToString("N2") +
					", frame: " + frame,
					20
				);
				//HUDHelpers.DrawBorderedRect( sb, null, Color.Gray, rect, 2 );
			}

			Color color1 = color * this.GetSublayerColorFadePercent( yPercent1 );
			if( color1.A > 0 ) {
				frame.Y = oldY + yOffset1;
				sb.Draw( tex, frame, null, color1 );
			}

			Color color2 = color * this.GetSublayerColorFadePercent( yPercent2 );
			if( color2.A > 0 ) {
				frame.X = frame.X + ( frame.Width / 2 );
				frame.Y = oldY + yOffset2;
				sb.Draw( tex, frame, null, color2 );
			}
			
			// I want to try to get drawDepth working at some point:
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
