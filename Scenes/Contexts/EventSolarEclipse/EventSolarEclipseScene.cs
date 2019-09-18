using System;
using System.Linq;
using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.EventSolarEclipse {
	public partial class EventSolarEclipseScene : Scene {
		public override SceneContext Context { get; }

		////

		public override int DrawPriority { get; } = 1;

		////

		public override Vector2 FrameSize { get; } = new Vector2( Main.screenWidth, Main.screenHeight );

		public override float HorizontalTileScrollRate { get; } = 0f;

		public override float VerticalTileScrollRate { get; } = 0f;

		////

		public override MistSceneDefinition SceneMists => new MistSceneDefinition(
			mistCount: 24,
			spacingSquared: 4096,
			aboveGroundMinHeight: -( 7 * 16 ),
			aboveGroundMaxHeight: 2 * 16,
			ground: TilePattern.CommonSolid,
			mistScale: new Vector2( 2.5f, 1f ),
			animationFadeTickRate: ( 1f / 60f ),
			animationPeekTickRate: ( 1f / 60f ),
			animationPeekTickRateAddedRandomRange: 1f
		);



		////////////////

		public EventSolarEclipseScene() {
			this.Context = new SceneContext(
				layer: SceneLayer.Game,
				isDay: null,
				vanillaBiome: null,
				currentEvent: null,
				customCondition: () => Main.eclipse
			);
			this.Context.Lock();
		}


		////////////////

		public override Color GetSceneColor( SceneDrawData drawData ) {
			byte shade = (byte)Math.Min( 0.1f * drawData.Brightness * 255f, 255 );

			var color = new Color( shade, shade, shade, 255 );

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
