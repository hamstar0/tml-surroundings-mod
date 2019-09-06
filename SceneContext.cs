using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.NPCs;
using HamstarHelpers.Helpers.World;
using System;
using Terraria;


namespace Surroundings {
	public enum SceneLayer {
		Screen,
		Near,
		Far,
		Game
	}




	public class SceneContext {
		public SceneLayer Layer = SceneLayer.Screen;
		public bool? IsDay = null;
		public VanillaEventFlag? CurrentEvent = null;
		public VanillaBiome? VanillaBiome = null;
		//public string CustomBiome = "";



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

			if( this.VanillaBiome.HasValue ) {
				if( ctx.VanillaBiome.HasValue ) {
					if( this.VanillaBiome != ctx.VanillaBiome ) {
						return false;
					}
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

			if( this.VanillaBiome != null ) {
				hash += ( (int)this.VanillaBiome << 4 );
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
				+", Biome:"+this.VanillaBiome;
				//+", Vanilla Biome:"+this.VanillaBiome
				//+", Custom Biome:"+this.VanillaBiome
		}
	}
}
