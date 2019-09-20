using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceRain {
	public abstract class SurfaceRainScene : Scene {
		public override SceneContext Context { get; }

		////

		public override int DrawPriority { get; } = 2;

		////

		public override Vector2 FrameSize { get; } = new Vector2( Main.screenWidth, Main.screenHeight );

		public override float VerticalTileScrollRate { get; } = 0f;

		////

		public override MistSceneDefinition SceneMists { get; } = null;



		////////////////

		protected SurfaceRainScene( bool isNear ) {
			this.Context = new SceneContext(
				layer: isNear ? SceneLayer.Near : SceneLayer.Far,
				isDay: null,
				vanillaBiome: null,
				currentEvent: null,
				regions: WorldRegionFlags.Overworld,
				customCondition: null
			);
			this.Context.Lock();
		}


		////////////////

		public override Color GetSceneColor( SceneDrawData drawData ) {
			byte shade = (byte)Math.Min( drawData.Brightness * 255f * 0.85f, 255 );
			float cavePercent = Math.Max( drawData.WallPercent - 0.5f, 0f ) * 2f;

			var color = new Color( shade, shade, shade, 255 );

			return color * (1f - cavePercent) * drawData.Opacity;
		}


		////////////////

		public override void Update() {
			//if( !Main.raining ) {
			//	object _;
			//	ReflectionHelpers.RunMethod( typeof(Main), null, "StartRain", new object[] { }, out _ );
			//}
		}


		////////////////

		public override void Draw(
				SpriteBatch sb,
				Rectangle rect,
				SceneDrawData drawData,
				float drawDepth ) {
			var mymod = SurroundingsMod.Instance;

			Color color = this.GetSceneColor( drawData );

			if( mymod.Config.DebugModeSceneInfo ) {
				DebugHelpers.Print( this.GetType().Name+"_"+this.Context.Layer,
					"rect: " + rect +
					", max rain: " + Main.maxRain +
					", bright: " + drawData.Brightness.ToString("N2") +
					", wall%: " + drawData.WallPercent.ToString("N2") +
					", opacity: " + drawData.Opacity.ToString("N2") +
					", color: " + color.ToString(),
					20
				);
			}

			this.DrawRain( sb, rect, color );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}

		////

		protected void DrawRain( SpriteBatch sb, Rectangle area, Color color ) {
			var mymod = SurroundingsMod.Instance;

			var rainTypeRects = new Rectangle[6];
			for( int i = 0; i < rainTypeRects.Length; i++ ) {
				rainTypeRects[i] = new Rectangle( i * 4, 0, 2, 40 );
			}

			float scale = ((float)area.Width * this.FrameSize.X) / (float)Main.screenWidth;

			for( int j = 0; j < Main.maxRain; j++ ) {
				if( !Main.rain[j].active ) {
					continue;
				}

				Rain rain = Main.rain[j];

				Vector2 pos = rain.position - Main.screenPosition;
				pos.X += area.X;
				pos.Y += area.Y;

				var dropletSrc = new Rectangle?( rainTypeRects[(int)rain.type] );

				if( mymod.Config.DebugModeSceneInfo ) {
					DebugHelpers.Print( this.GetType().Name+"_"+this.Context.Layer+"_Drop",
						"pos:"+(int)pos.X+","+(int)pos.Y+
						", dropletSrc:"+dropletSrc+
						", color:"+color+
						", scale:"+(scale * rain.scale),
						20 );
				}

				sb.Draw( Main.rainTexture,
					pos,
					dropletSrc,
					color,
					rain.rotation,
					Vector2.Zero,
					scale * rain.scale,
					SpriteEffects.None,
					0f
				);
			}
		}
	}
}
