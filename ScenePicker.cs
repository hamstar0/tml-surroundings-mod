using HamstarHelpers.Helpers.NPCs;
using HamstarHelpers.Helpers.Tiles;
using HamstarHelpers.Helpers.World;
using HamstarHelpers.Helpers.DotNET.Extensions;
using Surroundings.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;


namespace Surroundings {
	public class ScenePicker {
		private IDictionary<SceneContext, Scene> Definitions = new Dictionary<SceneContext, Scene> {
			{
				new OverworldDayScene().GetContext,
				new OverworldDayScene()
			}
		};



		////////////////

		public Scene GetScene( SceneContext ctx ) {
			Scene scene;

			if( Definitions.TryGetValue( ctx, out scene ) ) {
				return scene;
			}

			foreach( (SceneContext checkCtx, Scene otherScene) in this.Definitions ) {
				if( checkCtx.Check( ctx, false ) ) {
					return otherScene;
				}
			}

			return null;
		}


		////////////////

		public SceneContext GetCurrentContext( SceneLayer layer ) {
			int _, __;
			IDictionary<VanillaBiome, float> biomes = TileBiomeHelpers.GetVanillaBiomePercentsOf(
				Main.screenTileCounts, out _, out __ );

			VanillaEventFlag eventFlags = NPCInvasionHelpers.GetCurrentEventTypeSet();
			IEnumerable<VanillaBiome> biome = biomes.Where( kv => kv.Value >= 1f )
				.Select( kv=>kv.Key );

			return new SceneContext {
				IsDay = Main.dayTime,
				VanillaBiome = biome.FirstOrDefault(),
				CurrentEvent = NPCInvasionHelpers.GetCurrentEventTypeSet(),
				Layer = layer
			};
		}
	}
}
