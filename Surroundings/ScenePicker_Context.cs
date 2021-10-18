﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.NPCs;
using ModLibsGeneral.Libraries.Tiles;
using ModLibsGeneral.Libraries.World;


namespace Surroundings {
	public partial class ScenePicker {
		public static VanillaBiome PickPriorityBiome( IEnumerable<VanillaBiome> biomes, WorldRegionFlags region ) {
			VanillaBiome output = (region & WorldRegionFlags.Overworld) == WorldRegionFlags.Overworld ?
				VanillaBiome.Forest :
				(region & WorldRegionFlags.CaveRock) == WorldRegionFlags.CaveRock ?
					VanillaBiome.RockCave :
					VanillaBiome.Cave;

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
				} else if( biome == VanillaBiome.Meteor ) {
					if( output != VanillaBiome.Meteor ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Mushroom ) {
					if( output != VanillaBiome.Mushroom ) {
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
				} else if( biome == VanillaBiome.Snow ) {
					if( output != VanillaBiome.Snow ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Jungle ) {
					if( output != VanillaBiome.Jungle ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Temple ) {
					if( output != VanillaBiome.Temple ) {
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
				} else if( biome == VanillaBiome.Ocean ) {
					if( output != VanillaBiome.Ocean ) {
						output = biome;
					}
				} else if( biome == VanillaBiome.Desert ) {
					if( output != VanillaBiome.Desert ) {
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
				} else {
					output = biome;
				}
			}

			return output;
		}



		////////////////

		public SceneContext GetCurrentContextSansLayer() {
			IDictionary<VanillaBiome, float> biomePercents = TileBiomeLibraries.GetVanillaBiomePercentsOf(
				Main.screenTileCounts,
				out _, out _
			);

			Vector2 pos = Main.LocalPlayer.Center;

			/*if( WorldLibraries.IsDirtLayer( pos ) ) {
				biomePercents[ VanillaBiome.Cave ] = 1f;
			} else if( WorldLibraries.IsRockLayer( pos ) ) {
				biomePercents[ VanillaBiome.RockCave ] = 1f;
			} else if( WorldLibraries.IsSky( pos ) ) {
				biomePercents[ VanillaBiome.Space ] = 1f;
			} else if( WorldLibraries.IsWithinUnderworld( pos ) ) {
				biomePercents[ VanillaBiome.Hell ] = 1f;
			} else if( WorldLibraries.IsBeach( pos ) ) {
				biomePercents[ VanillaBiome.Ocean ] = 1f;
			}*/

			VanillaEventFlag eventFlags = NPCInvasionLibraries.GetCurrentEventTypeSet();
			IEnumerable<VanillaBiome> biomes = biomePercents
				.Where( kv => kv.Value >= 1f )
				.OrderBy( kv => -kv.Value )
				.Select( kv => kv.Key );

			WorldRegionFlags region = WorldLocationLibraries.GetRegion( pos, out _ );
			VanillaBiome biome = ScenePicker.PickPriorityBiome( biomes, region );

			if( SurroundingsConfig.Instance.DebugModeSceneInfo ) {
				DebugLibraries.Print( "CurrentContext", biome + " and " +
					string.Join( ", ", biomePercents.Where(kv => kv.Value > 0) ) +
					", Region: " + region,
					20
				);
			}

			var ctx = new SceneContext(
				layer: 0,
				isDay: Main.dayTime,
				currentEvent: eventFlags,
				anyOfBiome: new VanillaBiome[] { biome },
				anyOfRegions: new WorldRegionFlags[] { region },
				customCondition: null
			);
			return ctx;
		}
	}
}
