using System;
using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.EventBloodMoon {
	public partial class EventBloodMoonScene : SurfaceMistScene {
		public override SceneContext Context { get; }

		////

		public override MistSceneDefinition SceneMists { get; } = new MistSceneDefinition(
			mistCount: 10,
			spacingSquared: 4096f,
			aboveGroundMinHeight: 0,
			aboveGroundMaxHeight: 6 * 16,
			ground: TilePattern.CommonSolid,
			mistScale: new Vector2( 2f ),
			animationFadeTickDuration: 2 * 60,
			animationPeekTickDuration: 5 * 60,
			animationPeekAddedRandomTickDurationRange: 5 * 60
		);



		////////////////

		public EventBloodMoonScene() {
			this.Context = new SceneContext(
				layer: SceneLayer.Game,
				isDay: null,
				anyOfBiome: null,
				currentEvent: null,
				anyOfRegions: null,
				customCondition: ctx => Main.bloodMoon
			);
			this.Context.Lock();
		}


		////////////////

		public override Color GetSceneColor( SceneDrawData drawData ) {
			byte shade = 255;//(byte)Math.Min( drawData.Brightness * 255f, 255 );
			byte darkShade = (byte)( (float)shade * 0.1f );

			return new Color( shade, darkShade, darkShade, 128 );
		}
	}
}
