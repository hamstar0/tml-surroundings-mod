using System;
using System.Collections.Generic;
using System.Linq;
using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components;
using Terraria;


namespace Surroundings.Scenes {
	public partial class SurfaceBloodMoonScene : Scene {
		private ISet<MistDefinition> Mists = new HashSet<MistDefinition>();

		private Rectangle MostRecentDrawWorldRectangle = new Rectangle();


		////////////////

		public override int DrawPriority => 3;

		public override Vector2 Scale => new Vector2( 1f, 1f );

		public override float HorizontalTileScrollRate => 0f;

		public override float VerticalTileScrollRate => 0f;

		public override SceneContext Context { get; }

		////

		public int MistCount { get; } = 10;



		////////////////

		public SurfaceBloodMoonScene() {
			this.Context = new SceneContext {
				Layer = SceneLayer.Game
			};
		}


		////////////////

		public Color GetSceneColor( float brightness ) {
			byte shade = (byte)Math.Min( brightness * 255f, 255 );

			var color = new Color( shade, (byte)((float)shade * 0.1f), (byte)((float)shade * 0.1f), 160 );

			return color;
		}


		////////////////

		public override void Update() {
			if( this.MostRecentDrawWorldRectangle.Width == 0 || this.MostRecentDrawWorldRectangle.Height == 0 ) {
				return;
			}

			//if( Main.bloodMoon ) {
				Rectangle area = this.MostRecentDrawWorldRectangle;  //UIHelpers.GetWorldFrameOfScreen();
				int mistsToAdd = MistDefinition.CountMissingMists( this.Mists, area, this.MistCount );
				
				for( int i = 0; i < mistsToAdd; i++ ) {
					float animRate = ( Main.rand.NextFloat() * 3f ) + 2f;
					MistDefinition mist = MistDefinition.AttemptCreate( this.Mists,
						area,
						4096f,
						0,
						6 * 16,
						TilePattern.CommonSolid,
						animRate
					);

					if( mist != null ) {
						this.Mists.Add( mist );
					}
				}
			//}

			foreach( MistDefinition mist in this.Mists.ToArray() ) {
				mist.Update();

				if( !mist.IsActive ) {
					this.Mists.Remove( mist );
				}
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

			//float cavePercent = Math.Max( drawData.WallPercent - 0.5f, 0f ) * 2f;
			Color color = this.GetSceneColor(drawData.Brightness) * opacity;    // * (1f - cavePercent)

			if( mymod.Config.DebugModeInfo ) {
				DebugHelpers.Print( "SurfaceBloodMoonScene_"+this.Context.VanillaBiome,
					"mists: " + this.Mists.Count +
					", pos: " + (int)(rect.X + Main.screenPosition.X)+", "+(int)(rect.Y + Main.screenPosition.Y) +
					", bright: " + drawData.Brightness.ToString("N2") +
					//", cave%: " + cavePercent.ToString("N2") +
					", opacity: " + opacity.ToString("N2") +
					", color: " + color.ToString(),
					20
				);
			}

			this.MostRecentDrawWorldRectangle = rect;
			this.MostRecentDrawWorldRectangle.X += (int)Main.screenPosition.X;
			this.MostRecentDrawWorldRectangle.Y += (int)Main.screenPosition.Y;

			this.DrawMist( sb, color );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}


		////

		protected void DrawMist( SpriteBatch sb, Color color ) {
			foreach( MistDefinition mist in this.Mists ) {
				mist.Draw( sb, color );
			}
		}
	}
}
