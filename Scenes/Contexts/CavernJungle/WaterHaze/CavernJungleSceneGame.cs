using System;
using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.CavernJungle {
	public partial class CavernJungleSceneGame : SurfaceMistScene {
		public override SceneContext Context { get; }

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

		public CavernJungleSceneGame() {
			this.Context = new SceneContext(
				layer: SceneLayer.Game,
				isDay: null,
				anyOfBiome: new VanillaBiome[] { VanillaBiome.Jungle },
				currentEvent: null,
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.Cave },
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
	}
}
