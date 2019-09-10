using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.NPCs;
using HamstarHelpers.Helpers.Tiles;
using HamstarHelpers.Helpers.World;
using HamstarHelpers.Helpers.DotNET.Extensions;
using Surroundings.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Microsoft.Xna.Framework;


namespace Surroundings {
	public class ScenePicker {
		private static object MyLock { get; } = new object();



		////////////////

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
				new OverworldScene().Context,
				new OverworldScene()
			}
		};

		private IDictionary<Scene, float> SceneFades = new Dictionary<Scene, float>();



		////////////////

		public IEnumerable<(Scene Scene, float Opacity)> GetScenes( SceneLayer layer ) {
			return this.GetScenesUnordered( layer )
				.OrderBy( kv => kv.Scene.DrawPriority );
		}

		private IEnumerable<(Scene Scene, float Opacity)> GetScenesUnordered( SceneLayer layer ) {
			lock( ScenePicker.MyLock ) {
				foreach( (Scene scene, float fadeProgress) in this.SceneFades ) {
					if( scene.Context.Layer != layer ) {
						continue;
					}

					if( fadeProgress < 0f ) {
						yield return (scene, 1f + fadeProgress);
					} else if( fadeProgress >= 0f ) {
						yield return (scene, 1f - fadeProgress);
					}
				}
			}
		}


		////////////////

		internal void UpdateScenesPerContext( SceneContext ctx ) {
			lock( ScenePicker.MyLock ) {
				foreach( (SceneContext checkCtx, Scene scene) in this.Definitions ) {
					bool isActive = checkCtx.Check( ctx, false );
					this.UpdateSceneState( scene, isActive );
					this.UpdateSceneFade( scene );
				}
			}
		}

		private void UpdateSceneState( Scene scene, bool isActive ) {
			if( isActive ) {
				if( !this.SceneFades.ContainsKey( scene ) ) {
					this.SceneFades[scene] = -1f;
				} else {
					if( this.SceneFades[scene] > 0 ) {
						this.SceneFades[scene] = -this.SceneFades[scene];
					}
				}
			} else {
				if( this.SceneFades.ContainsKey( scene ) ) {
					if( this.SceneFades[scene] == 0f ) {
						this.SceneFades[scene] += 1f / 60f;
					} else if( this.SceneFades[scene] < 0f ) {
						this.SceneFades[scene] = -this.SceneFades[scene];
					}
				}
			}
		}

		private void UpdateSceneFade( Scene scene ) {
			float oldFade = this.SceneFades[scene];
			if( oldFade == 0f ) {
				return;
			}

			float newFade = oldFade + ( 1f / 60f );

			if( oldFade < 1f ) {
				if( newFade >= 1f ) {
					this.SceneFades.Remove( scene );
				} else {
					this.SceneFades[scene] = newFade;
				}
			} else if( oldFade < 0f ) {
				if( newFade >= 0f ) {
					newFade = 0f;
				}
				this.SceneFades[scene] = newFade;
			}
		}


		////////////////

		public SceneContext GetCurrentContextSansLayer() {
			int _, __;
			IDictionary<VanillaBiome, float> biomePerents = TileBiomeHelpers.GetVanillaBiomePercentsOf(
				Main.screenTileCounts,
				out _, out __
			);

			Vector2 pos = Main.LocalPlayer.Center;

			if( WorldHelpers.IsDirtLayer( pos ) ) {
				biomePerents[ VanillaBiome.Cave ] = 1f;
			} else if( WorldHelpers.IsRockLayer( pos ) ) {
				biomePerents[ VanillaBiome.RockCave ] = 1f;
			} else if( WorldHelpers.IsSky( pos ) ) {
				biomePerents[ VanillaBiome.Space ] = 1f;
			} else if( WorldHelpers.IsWithinUnderworld( pos ) ) {
				biomePerents[ VanillaBiome.Hell ] = 1f;
			} else if( WorldHelpers.IsBeach( pos ) ) {
				biomePerents[ VanillaBiome.Ocean ] = 1f;
			}

			VanillaEventFlag eventFlags = NPCInvasionHelpers.GetCurrentEventTypeSet();
			IEnumerable<VanillaBiome> biomes = biomePerents
				.Where( kv => kv.Value >= 1f )
				.Select( kv => kv.Key );
			VanillaBiome biome = ScenePicker.PickPriorityBiome( biomes );

			var mymod = SurroundingsMod.Instance;
			if( mymod.Config.DebugModeInfo ) {
				DebugHelpers.Print( "CurrentContext", biome + " and " + string.Join(", ", biomePerents.Where(kv=>kv.Value>0)), 20 );
			}

			return new SceneContext {
				IsDay = Main.dayTime,
				VanillaBiome = biome,
				CurrentEvent = NPCInvasionHelpers.GetCurrentEventTypeSet(),
			};
		}
	}
}
