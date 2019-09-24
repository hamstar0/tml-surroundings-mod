using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.NPCs;
using HamstarHelpers.Helpers.Tiles;
using HamstarHelpers.Helpers.World;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Microsoft.Xna.Framework;


namespace Surroundings {
	public partial class ScenePicker {
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
				} else if( biome == VanillaBiome.RockCave ) {
					if( output != VanillaBiome.RockCave ) {
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
				} else if( biome == VanillaBiome.Snow ) {
					if( output != VanillaBiome.Snow ) {
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

		public SceneContext GetCurrentContextSansLayer() {
			int _, __;
			IDictionary<VanillaBiome, float> biomePercents = TileBiomeHelpers.GetVanillaBiomePercentsOf(
				Main.screenTileCounts,
				out _, out __
			);

			Vector2 pos = Main.LocalPlayer.Center;

			if( WorldHelpers.IsDirtLayer( pos ) ) {
				biomePercents[ VanillaBiome.Cave ] = 1f;
			} else if( WorldHelpers.IsRockLayer( pos ) ) {
				biomePercents[ VanillaBiome.RockCave ] = 1f;
			} else if( WorldHelpers.IsSky( pos ) ) {
				biomePercents[ VanillaBiome.Space ] = 1f;
			} else if( WorldHelpers.IsWithinUnderworld( pos ) ) {
				biomePercents[ VanillaBiome.Hell ] = 1f;
			} else if( WorldHelpers.IsBeach( pos ) ) {
				biomePercents[ VanillaBiome.Ocean ] = 1f;
			}

			VanillaEventFlag eventFlags = NPCInvasionHelpers.GetCurrentEventTypeSet();
			IEnumerable<VanillaBiome> biomes = biomePercents
				.Where( kv => kv.Value >= 1f )
				.Select( kv => kv.Key );
			VanillaBiome biome = ScenePicker.PickPriorityBiome( biomes );
			WorldRegionFlags region = WorldHelpers.GetRegion( pos );

			var mymod = SurroundingsMod.Instance;
			if( mymod.Config.DebugModeSceneInfo ) {
				DebugHelpers.Print( "CurrentContext", biome + " and " +
					string.Join( ", ", biomePercents.Where(kv => kv.Value > 0) ) + 
					", Region: "+region,
					20
				);
			}

			var ctx = new SceneContext(
				layer: 0,
				isDay: Main.dayTime,
				vanillaBiome: biome,
				currentEvent: eventFlags,
				anyOfRegions: new WorldRegionFlags[] { region },
				customCondition: null
			);
			return ctx;
		}
	}
}
