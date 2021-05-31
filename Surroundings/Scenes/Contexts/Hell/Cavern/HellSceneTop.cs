using System;
using System.Linq;
using ModLibsCore.Libraries.Debug;
using ModLibsGeneral.Libraries.World;
using ModLibsGeneral.Libraries.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.CavernRock {
	public abstract class HellSceneTop : CavernSceneTop {
		public static Color ApplyBiomeTint( Color color ) {
			var mymod = SurroundingsMod.Instance;
			if( mymod.ScenePicker.CurrentContext == null ) {
				return color;
			}

			foreach( VanillaBiome currBiome in mymod.ScenePicker.CurrentContext.AnyOfBiome ) {
				if( ( currBiome & VanillaBiome.Corruption ) == VanillaBiome.Corruption ) {
					var corrColor = new Color( 152, 120, 152 );
					return Color.Lerp( color, XNAColorLibraries.Mul( color, corrColor ), 0.5f );
				}
				if( ( currBiome & VanillaBiome.Crimson ) == VanillaBiome.Crimson ) {
					var crimColor = new Color( 128, 0, 0 );
					return Color.Lerp( color, XNAColorLibraries.Mul( color, crimColor ), 0.5f );
				}
				if( ( currBiome & VanillaBiome.Hallow ) == VanillaBiome.Corruption ) {
					var hallColor = new Color( 224, 112, 224 );
					return Color.Lerp( color, XNAColorLibraries.Mul( color, hallColor ), 0.5f );
				}
			}

			return color;
		}



		////////////////

		private Texture2D CachedTex = null;

		////////////////

		public override SceneContext Context { get; }



		////////////////

		protected HellSceneTop( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,
				anyOfBiome: null,
				currentEvent: null,
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.Hell },
				customCondition: null
			);
			this.Context.Lock();
		}


		////////////////

		public override Texture2D GetSceneTexture() {
			if( this.CachedTex == null ) {
				this.CachedTex = SurroundingsMod.Instance.GetTexture( "Scenes/Contexts/Hell/Cavern/CavernHell_Top" );
			}
			return this.CachedTex;
		}
	}
}
