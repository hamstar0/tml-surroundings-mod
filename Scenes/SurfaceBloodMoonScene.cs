using System;
using System.Collections.Generic;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.UI;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components;
using Terraria;


namespace Surroundings.Scenes {
	public partial class SurfaceBloodMoonScene : Scene {
		private IList<MistDefinition> Mists = new List<MistDefinition>();


		////////////////

		public override int DrawPriority => 3;

		public override Vector2 Scale => new Vector2( 1f, 1f );

		public override float HorizontalTileScrollRate => 0f;

		public override float VerticalTileScrollRate => 0f;

		public override SceneContext Context { get; }



		////////////////

		public SurfaceBloodMoonScene( VanillaBiome biome ) {
			this.Context = new SceneContext {
				VanillaBiome = biome,
				Layer = SceneLayer.Game
			};
		}


		////////////////

		public Color GetSceneColor( float brightness ) {
			byte shade = (byte)Math.Min( brightness * 255f * 0.85f, 255 );

			Color color = new Color( shade, 0, 0, 255 );

			return color;
		}

		public override void Update() {
			Rectangle area = UIHelpers.GetWorldFrameOfScreen();
			int mistsToAdd = MistDefinition.CountMissingMists( this.Mists, area, 10 );
			
			for( int i=0; i<mistsToAdd; i++ ) {
				this.Mists.Add( MistDefinition.Create(area) );
			}

			foreach( MistDefinition mist in this.Mists ) {
				mist.Update();
			}
		}


		////////////////

		public override void Draw(
				SpriteBatch sb,
				Rectangle rect,
				SceneDrawData drawData,
				float opacity,
				float drawDepth ) {
			var mymod = SurroundingsMod.Instance;

			float cavePercent = Math.Max( drawData.WallPercent - 0.5f, 0f ) * 2f;
			Color color = this.GetSceneColor(drawData.Brightness) * (1f - cavePercent) * opacity;

			if( mymod.Config.DebugModeInfo ) {
				DebugHelpers.Print( "SurfaceBloodMoonScene_"+this.Context.VanillaBiome,
					"rect: " + rect +
					", max rain: " + Main.maxRain +
					", bright: " + drawData.Brightness +
					", cave%: " + cavePercent.ToString("N2") +
					", color: " + color.ToString() +
					", opacity: " + opacity,
					20
				);
			}

			this.DrawMist( sb, rect, color );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}


		////

		protected void DrawMist( SpriteBatch sb, Rectangle area, Color color ) {
			foreach( MistDefinition mist in this.Mists ) {
				Vector2 pos = mist.WorldPosition - Main.screenPosition;

				mist.Animation.Draw( sb, pos, color );
			}
		}
	}
}
