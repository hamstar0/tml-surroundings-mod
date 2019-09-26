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
			bool isCave = ctx.AnyOfRegions?
				.Any( r => ( r & WorldRegionFlags.Cave ) != 0 )
				?? false;

			if( !isCave ) {
				return false;
			}

			return ctx.AnyOfBiome?
				.Any( b => {
					if( ( b & VanillaBiome.Corruption ) != 0 ) {
						return false;
					}
					if( ( b & VanillaBiome.Crimson ) != 0 ) {
						return false;
					}
					if( ( b & VanillaBiome.Hallow ) != 0 ) {
						return false;
					}
					if( ( b & VanillaBiome.Snow ) != 0 ) {
						return false;
					}
					if( ( b & VanillaBiome.Desert ) != 0 ) {
						return false;
					}
					if( ( b & VanillaBiome.Jungle ) != 0 ) {
						return false;
					}
					return true;
				} )
				?? true;
		}



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

		public float GetSublayerColorFadePercent( float yPercent ) {
			float inv = 1f - yPercent;
			return Math.Min( inv * 3f, 1f );
		}

		////////////////

		public float GetSceneVerticalRangePercent( Vector2 origin ) {
			float wldRange = 128f * 16f;
			float withinRange = origin.Y % wldRange;
			float percent = withinRange / wldRange;

			return percent;
		}

		////////////////

		public virtual int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			int offset = (int)( yPercent * (float)texHeight );
			offset -= 176;
			offset += SurroundingsMod.Instance.DebugOverlayOffset;

			return offset;
		}


		////////////////

		public sealed override void Draw(
				SpriteBatch sb,
				Rectangle rect,
				SceneDrawData drawData,
				float drawDepth ) {
			var mymod = SurroundingsMod.Instance;
			Texture2D tex = this.GetSceneTexture();

			Color color = this.GetSceneColor( drawData );

			float yPercent1 = this.GetSceneVerticalRangePercent( drawData.Center );
			float yPercent2 = yPercent1 - 0.5f;
			if( yPercent2 < 0 ) {
				yPercent2 = 1 + yPercent2;
			}

			int yOffset1 = this.GetSceneTextureVerticalOffset( yPercent1, rect.Height );
			int yOffset2 = this.GetSceneTextureVerticalOffset( yPercent2, rect.Height );
			int oldY = rect.Y;

			if( mymod.Config.DebugModeSceneInfo ) {
				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer,
					"brightness: " + drawData.Brightness.ToString("N2") +
					", opacity: " + drawData.Opacity.ToString("N2") +
					", color: " + color.ToString() +
					//", color1: " + (color * this.GetSublayerColorFadePercent(yPercent - 1)).ToString() +
					//", color2: " + (color * this.GetSublayerColorFadePercent(yPercent)).ToString() +
					", yPercent: " + yPercent1.ToString("N2") +
					", yOffset1: " + yOffset1 +
					", yOffset2: " + yOffset2 +
					//", texZoom: " + texZoom.ToString("N2") +
					", rect: "+ rect,
					20
				);
				//HUDHelpers.DrawBorderedRect( sb, null, Color.Gray, rect, 2 );
			}

			Color color1 = color * this.GetSublayerColorFadePercent( yPercent1 );
			if( color1.A > 0 ) {
				rect.Y = (oldY + yOffset1) - rect.Height;
				sb.Draw( tex, rect, null, color1 );
			}

			Color color2 = color * this.GetSublayerColorFadePercent( yPercent2 );
			if( color2.A > 0 ) {
				rect.X = rect.X + (rect.Width / 2);
				rect.Y = (oldY + yOffset2) - tex.Height;
				sb.Draw( tex, rect, null, color2 );
			}

			// I want to try to get drawDepth working at some point:
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
