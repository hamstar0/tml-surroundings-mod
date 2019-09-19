using System;
using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceSnow {
	public partial class SurfaceSnowSceneGame : Scene {
		public override SceneContext Context { get; } = new SceneContext(
			layer: SceneLayer.Game,
			vanillaBiome: VanillaBiome.Cold,
			isDay: null,
			currentEvent: null,
			customCondition: null
		);

		////

		public override int DrawPriority => 1;

		////

		public override Vector2 FrameSize => new Vector2( Main.screenWidth, Main.screenHeight );

		public override float HorizontalTileScrollRate { get; } = 0f;

		public override float VerticalTileScrollRate { get; } = 0f;

		////

		public override MistSceneDefinition SceneMists { get; } = new MistSceneDefinition(
			mistCount: 4,
			spacingSquared: (6 * 16) * (6 * 16),
			aboveGroundMinHeight: 0,
			aboveGroundMaxHeight: 1,
			ground: new TilePattern( new TilePatternBuilder {
				IsSolid = true,
				IsPlatform = false,
				IsActuated = false,
				AreaFromCenter = new Rectangle( -1, 0, 2, 1 ),
			} ),
			mistScale: new Vector2( 0.5f, 0.65f ),
			animationFadeTickDuration: 30,
			animationPeekTickDuration: 60,
			animationPeekAddedRandomTickDurationRange: 30
		);



		////////////////

		public SurfaceSnowSceneGame() {
		}


		////////////////

		public override Color GetSceneColor( SceneDrawData drawData ) {
			byte shade = (byte)Math.Min( drawData.Brightness * 255f, 255 );

			var color = new Color( shade, shade, shade, 224 );

			return color * drawData.Opacity;
		}


		////////////////

		public override void Update() {
			if( this.MostRecentDrawWorldRectangle.Width == 0 || this.MostRecentDrawWorldRectangle.Height == 0 ) {
				return;
			}

			Rectangle area = this.MostRecentDrawWorldRectangle; //UIHelpers.GetWorldFrameOfScreen();

			MistSceneDefinition.GenerateMists( area, this.SceneMists );
			this.SceneMists.Update();
		}


		////////////////

		public override void Draw(
				SpriteBatch sb,
				Rectangle rect,
				SceneDrawData drawData,
				float drawDepth ) {
			var mymod = SurroundingsMod.Instance;

			//float cavePercent = Math.Max( drawData.WallPercent - 0.5f, 0f ) * 2f;
			Color color = this.GetSceneColor( drawData );    // * (1f - cavePercent)

			if( mymod.Config.DebugModeInfo ) {
				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer,
					"mists: " + this.SceneMists.Mists.Count +
					", rect: " + rect +
					", bright: " + drawData.Brightness.ToString("N2") +
					//", cave%: " + cavePercent.ToString("N2") +
					", opacity: " + drawData.Opacity.ToString("N2") +
					", color: " + color.ToString(),
					20
				);
			}

			this.SceneMists.DrawAll( sb, color );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
