using System;
using System.Linq;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using HamstarHelpers.Helpers.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.CavernRock {
	public abstract class CavernRockSceneTop : CavernSceneTop {
		public static Color ApplyBiomeTint( Color color ) {
			var mymod = SurroundingsMod.Instance;
			if( mymod.ScenePicker.CurrentContext == null ) {
				return color;
			}

			foreach( VanillaBiome currBiome in mymod.ScenePicker.CurrentContext.AnyOfBiome ) {
				if( ( currBiome & VanillaBiome.Corruption ) == VanillaBiome.Corruption ) {
					var corrColor = new Color( 152, 120, 152 );
					return Color.Lerp( color, XNAColorHelpers.Mul( color, corrColor ), 0.5f );
				}
				if( ( currBiome & VanillaBiome.Crimson ) == VanillaBiome.Crimson ) {
					var crimColor = new Color( 128, 0, 0 );
					return Color.Lerp( color, XNAColorHelpers.Mul( color, crimColor ), 0.5f );
				}
				if( ( currBiome & VanillaBiome.Hallow ) == VanillaBiome.Corruption ) {
					var hallColor = new Color( 224, 112, 224 );
					return Color.Lerp( color, XNAColorHelpers.Mul( color, hallColor ), 0.5f );
				}
			}

			return color;
		}



		////////////////

		public override SceneContext Context { get; }

		////////////////

		private Texture2D CachedTex = null;



		////////////////

		protected CavernRockSceneTop( SceneLayer layer ) {
			this.Context = new SceneContext(
				layer: layer,
				isDay: null,
				anyOfBiome: null,
				currentEvent: null,
				anyOfRegions: new WorldRegionFlags[] { WorldRegionFlags.CaveRock },
				customCondition: (ctx) => {
					bool isPreRock = ctx.AnyOfRegions
						.Any( r => ( r & WorldRegionFlags.CavePreRock ) == WorldRegionFlags.CavePreRock );
					if( isPreRock ) {
						return false;
					}
					return CavernScene.IsPlainCave( ctx, true );
				}
			);
			this.Context.Lock();
		}


		////////////////

		public override Texture2D GetSceneTexture() {
			if( this.CachedTex == null ) {
				this.CachedTex = SurroundingsMod.Instance.GetTexture( "Scenes/Contexts/CavernRock/CavernRock_Top" );
			}
			return this.CachedTex;
		}

		public override Color GetSceneColor( SceneDrawData drawData ) {
			return CavernRockSceneTop.ApplyBiomeTint( base.GetSceneColor(drawData) );
		}
	}
}
