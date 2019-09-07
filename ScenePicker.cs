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
		public static VanillaBiome PickPriorityBiome( IEnumerable<VanillaBiome> biomes ) {
			VanillaBiome output = VanillaBiome.Forest;

			foreach( VanillaBiome biome in biomes ) {
				if( biome == VanillaBiome.Hell ) {
					if( output != VanillaBiome.Hell ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Space ) {
					if( output != VanillaBiome.Space ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Dungeon ) {
					if( output != VanillaBiome.Dungeon ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.SpiderNest ) {
					if( output != VanillaBiome.SpiderNest ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Granite ) {
					if( output != VanillaBiome.Granite ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Marble ) {
					if( output != VanillaBiome.Marble ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Cave ) {
					if( output != VanillaBiome.Cave ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Meteor ) {
					if( output != VanillaBiome.Meteor ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Ocean ) {
					if( output != VanillaBiome.Ocean ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Cold ) {
					if( output != VanillaBiome.Cold ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Mushroom ) {
					if( output != VanillaBiome.Mushroom ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Temple ) {
					if( output != VanillaBiome.Temple ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Jungle ) {
					if( output != VanillaBiome.Jungle ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Corruption ) {
					if( output != VanillaBiome.Corruption ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Crimson ) {
					if( output != VanillaBiome.Crimson ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Hallow ) {
					if( output != VanillaBiome.Hallow ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Desert ) {
					if( output != VanillaBiome.Desert ) {
						output = biome;
					}
				} else {
					output = biome;
				}
			}

			return output;
		}



		////////////////

		private IDictionary<SceneContext, Scene> Definitions = new Dictionary<SceneContext, Scene> {
			{
				new OverworldDayScene( true ).GetContext,
				new OverworldDayScene( true )
			},
			{
				new OverworldDayScene( false ).GetContext,
				new OverworldDayScene( false )
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
				VanillaBiome = ScenePicker.PickPriorityBiome( biome ),
				CurrentEvent = NPCInvasionHelpers.GetCurrentEventTypeSet(),
				Layer = layer
			};
		}
	}
}
