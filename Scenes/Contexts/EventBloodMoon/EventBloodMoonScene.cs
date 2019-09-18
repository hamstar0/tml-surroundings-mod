using System;
using System.Linq;
using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.EventBloodMoon {
	public partial class EventBloodMoonScene : Scene {
		public override SceneContext Context { get; }

		////

		public override int DrawPriority { get; } = 1;

		////

		public override Vector2 FrameSize { get; } = new Vector2( Main.screenWidth, Main.screenHeight );

		public override float HorizontalTileScrollRate { get; } = 0f;

		public override float VerticalTileScrollRate { get; } = 0f;

		////

		public override MistSceneDefinition SceneMists { get; } = new MistSceneDefinition(
			mistCount: 10,
			spacingSquared: 4096f,
			aboveGroundMinHeight: 0,
			aboveGroundMaxHeight: 6 * 16,
			ground: TilePattern.CommonSolid,
			mistScale: new Vector2( 2f ),
			animationFadeTickRate: ( 1f / 60f ) * 2,
			animationPeekTickRate: ( 1f / 60f ) * 2,
			animationPeekTickRateAddedRandomRange: 5
		);



		////////////////

		public EventBloodMoonScene() {
			this.Context = new SceneContext(
				layer: SceneLayer.Game,
				isDay: null,
				vanillaBiome: null,
				currentEvent: null,
				customCondition: () => Main.bloodMoon
			);
			this.Context.Lock();
		}


		////////////////

		public override Color GetSceneColor( SceneDrawData drawData ) {
			byte shade = (byte)Math.Min( drawData.Brightness * 255f, 255 );
			byte darkShade = (byte)( (float)shade * 0.1f );

			var color = new Color( shade, darkShade, darkShade, 128 );

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
