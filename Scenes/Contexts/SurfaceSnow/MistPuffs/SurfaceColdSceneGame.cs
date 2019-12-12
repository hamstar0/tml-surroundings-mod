using System;
using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceSnow {
	public partial class SurfaceSnowSceneGame : SurfaceMistScene {
		public override SceneContext Context { get; } = new SceneContext(
			layer: SceneLayer.Game,
			isDay: null,
			anyOfBiome: new VanillaBiome[] { VanillaBiome.Snow },
			currentEvent: null,
			anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.Overworld },
			customCondition: null
		);

		////

		public override MistSceneDefinition SceneMists { get; } = new MistSceneDefinition(
			mistCount: 4,
			spacingSquared: (6 * 16) * (6 * 16),
			aboveGroundMinHeight: 0,
			aboveGroundMaxHeight: 1,
			ground: new TilePattern( new TilePatternBuilder {
				HasSolidProperties = true,
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

			return new Color( shade, shade, shade, 224 );
		}
	}
}
