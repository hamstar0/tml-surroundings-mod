﻿using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.NPCs;
using HamstarHelpers.Helpers.World;
using System;
using Terraria;


namespace Surroundings {
	public enum SceneLayer {
		Screen = 1,
		Near = 2,
		Far = 4,
		Game = 8
	}




	public class SceneContext {
		private bool IsLocked = false;


		////////////////

		public SceneLayer Layer { get; private set; }
		public bool? IsDay { get; }
		public VanillaEventFlag? CurrentEvent { get; }
		public VanillaBiome[] AnyOfBiome { get; }
		//public string CustomBiome { get; } = "";
		public WorldRegionFlags[] AnyOfRegions { get; }

		////

		public Func<SceneContext, bool> CustomConditions { get; }



		////////////////

		public SceneContext( SceneLayer layer,
				bool? isDay,
				VanillaEventFlag? currentEvent,
				VanillaBiome[] anyOfBiome,
				WorldRegionFlags[] anyOfRegions,
				Func<SceneContext, bool> customCondition ) {
			this.Layer = layer;
			this.IsDay = isDay;
			this.CurrentEvent = currentEvent;
			this.AnyOfBiome = anyOfBiome ?? new VanillaBiome[] { };
			this.AnyOfRegions = anyOfRegions ?? new WorldRegionFlags[] { };
			this.CustomConditions = customCondition;
		}

		////

		public void Lock() {
			this.IsLocked = true;
		}

		public void SetLayer( SceneLayer layer ) {
			if( this.IsLocked ) {
				throw new Exception( "Layer info is locked." );
			}
			this.Layer = layer;
		}


		////////////////

		public bool Check( SceneContext ctx, bool skipLayer ) {
			if( !skipLayer && this.Layer != ctx.Layer ) {
				return false;
			}

			if( !this.IsDay.HasValue ) {
				if( ctx.IsDay.HasValue ) {
					if( ctx.IsDay.Value != Main.dayTime ) {
						return false;
					}
				}
			} else {
				if( ctx.IsDay.HasValue ) {
					if( ctx.IsDay.Value != this.IsDay.Value ) {
						return false;
					}
				} else {
					if( this.IsDay.Value != Main.dayTime ) {
						return false;
					}
				}
			}

			VanillaEventFlag eventFlags = NPCInvasionHelpers.GetCurrentEventTypeSet();

			if( !this.CurrentEvent.HasValue ) {
				if( ctx.CurrentEvent.HasValue ) {
					if( (ctx.CurrentEvent.Value & eventFlags) != ctx.CurrentEvent.Value ) {
						return false;
					}
				}
			} else {
				if( ctx.CurrentEvent.HasValue ) {
					if( ctx.CurrentEvent.Value != this.CurrentEvent.Value ) {
						return false;
					}
				} else {
					if( (this.CurrentEvent.Value & eventFlags) != this.CurrentEvent.Value ) {
						return false;
					}
				}
			}

			bool foundBiome = this.AnyOfBiome.Length == 0;

			foreach( VanillaBiome biome in this.AnyOfBiome ) {
				foreach( VanillaBiome ctxBiome in ctx.AnyOfBiome ) {
					if( biome == ctxBiome ) {
						foundBiome = true;
						break;
					}
				}
				if( foundBiome ) { break; }
			}
			if( !foundBiome ) {
				return false;
			}

			bool foundRegion = this.AnyOfRegions.Length == 0;

			foreach( WorldRegionFlags region in this.AnyOfRegions ) {
				foreach( WorldRegionFlags ctxRegion in ctx.AnyOfRegions ) {
					if( (region & ctxRegion) == region ) {
						foundRegion = true;
						break;
					}
				}
				if( foundRegion ) { break; }
			}
			if( !foundRegion ) {
				return false;
			}

			if( this.CustomConditions != null ) {
				if( !this.CustomConditions() ) {
					return false;
				}
			}

			return true;
		}


		////////////////

		public override int GetHashCode() {
			int hash = 0;

			try {
				hash = this.IsDay.HasValue ? 1 : 0;
				hash += hash == 1 ? ( this.IsDay.Value ? 2 : 0 ) : 0;
			} catch { }

			hash += ( (int)this.Layer << 2 ).GetHashCode();

			foreach( VanillaBiome biome in this.AnyOfBiome ) {
				hash ^= ( (int)biome << 4 );
			}
			if( this.CurrentEvent != null ) {
				hash += ( (int)this.CurrentEvent << 9 );
			}
			//hash += ( "cbiome" + this.CustomBiome ).GetHashCode() << 20;

			return hash;
		}

		////////////////

		public override string ToString() {
			return this.GetHashCode()
				+" - Events:"+ this.CurrentEvent
				+", Layer:"+this.Layer
				+", Day:"+this.IsDay
				+", Biome:"+this.AnyOfBiome;
				//+", Vanilla Biome:"+this.VanillaBiome
				//+", Custom Biome:"+this.VanillaBiome
		}
	}
}
