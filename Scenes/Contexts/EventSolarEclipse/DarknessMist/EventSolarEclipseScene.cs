using System;
using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.EventSolarEclipse {
	public partial class EventSolarEclipseScene : SurfaceMistScene {
		public override SceneContext Context { get; }

		////

		public override MistSceneDefinition SceneMists { get; } = new MistSceneDefinition(
			mistCount: 24,
			spacingSquared: 4096,
			aboveGroundMinHeight: -( 7 * 16 ),
			aboveGroundMaxHeight: 2 * 16,
			ground: TilePattern.CommonSolid,
			mistScale: new Vector2( 2.5f, 1f ),
			animationFadeTickDuration: 1 * 60,
			animationPeekTickDuration: 1 * 60,
			animationPeekAddedRandomTickDurationRange: 1 * 60
		);



		////////////////

		public EventSolarEclipseScene() {
			this.Context = new SceneContext(
				layer: SceneLayer.Game,
				isDay: null,
				anyOfBiome: null,
				currentEvent: null,
				anyOfRegions: null,
				customCondition: ctx => Main.eclipse
			);
			this.Context.Lock();
		}


		////////////////

		public override Color GetSceneColor( SceneDrawData drawData ) {
			byte shade = (byte)Math.Min( 0.1f * drawData.Brightness * 255f, 255 );

			var color = new Color( shade, shade, shade, 255 );

			return color * drawData.Opacity;
		}
	}
}
