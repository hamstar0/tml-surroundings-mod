using System;
using System.Linq;
using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.EventBloodMoon {
	public partial class SurfaceJungleSceneGame : Scene {
		public override SceneContext Context { get; }

		////

		public override int DrawPriority { get; } = 1;

		////

		public override Vector2 FrameSize => new Vector2( Main.screenWidth, Main.screenHeight );

		public override float HorizontalTileScrollRate { get; } = 0f;

		public override float VerticalTileScrollRate { get; } = 0f;

		////

		public override MistSceneDefinition SceneMists { get; } = new MistSceneDefinition(
			mistCount: 14,
			spacingSquared: 4096f,
			aboveGroundMinHeight: -16,
			aboveGroundMaxHeight: 0,
			ground: new TilePattern( new TilePatternBuilder {
				AreaFromCenter = new Rectangle(-1, 0, 2, 1),
				HasWater = true
			} ),
			mistScale: new Vector2( 0.8f, 0.45f ),
			animationFadeTickDuration: 2 * 60,
			animationPeekTickDuration: 4 * 60,
			animationPeekAddedRandomTickDurationRange: 4 * 60
		);



		////////////////

		public SurfaceJungleSceneGame() {
			this.Context = new SceneContext(
				layer: SceneLayer.Game,
				isDay: null,
				vanillaBiome: VanillaBiome.Jungle,
				currentEvent: null,
				regions: WorldRegionFlags.Overworld,
				customCondition: null
			);
			this.Context.Lock();
		}


		////////////////

		public override Color GetSceneColor( SceneDrawData drawData ) {
			byte shade = (byte)Math.Min( drawData.Brightness * 255f, 255 );
			shade = (byte)( (float)shade * 0.75f );
			byte darkShade = (byte)( (float)shade * 0.65f );

			var color = new Color( darkShade, shade, darkShade, 128 );

			return color * drawData.Opacity;
		}


		////////////////

		public override void Update() {
			if( this.MostRecentDrawWorldRectangle.Width == 0 || this.MostRecentDrawWorldRectangle.Height == 0 ) {
				return;
			}

			Rectangle area = this.MostRecentDrawWorldRectangle; //UIHelpers.GetWorldFrameOfScreen();

			MistSceneDefinition.GenerateMists( area, this.SceneMists );

			foreach( Mist mist in this.SceneMists.Mists.ToArray() ) {
				mist.Update();

				if( !mist.IsActive ) {
					this.SceneMists.Mists.Remove( mist );
				}
			}
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

			if( mymod.Config.DebugModeSceneInfo ) {
				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer,
					"mists: " + this.SceneMists.Mists.Count +
					", rect: " + rect +
					", bright: " + drawData.Brightness.ToString( "N2" ) +
					//", cave%: " + cavePercent.ToString("N2") +
					", opacity: " + drawData.Opacity.ToString( "N2" ) +
					", color: " + color.ToString(),
					20
				);
			}

			this.SceneMists.DrawAll( sb, color );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
