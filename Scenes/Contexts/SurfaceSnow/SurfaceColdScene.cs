using System;
using System.Linq;
using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.SurfaceSnow {
	public partial class SurfaceColdScene : Scene {
		public override SceneContext Context => new SceneContext {
			Layer = SceneLayer.Game,
			VanillaBiome = VanillaBiome.Cold
		};

		////

		public override int DrawPriority => 1;

		////

		public override Vector2 SceneScale => new Vector2( 1f );

		public override float HorizontalTileScrollRate => 0f;

		public override float VerticalTileScrollRate => 0f;

		////

		public override MistSceneDefinition MistDefinition => new MistSceneDefinition(
			mistCount: 4,
			spacingSquared: 4096,
			aboveGroundMinHeight: 1 * 16,
			aboveGroundMaxHeight: 4 * 16,
			ground: TilePattern.CommonSolid,
			mistScale: new Vector2( 0.5f, 0.75f ),
			animationDurationMultiplier: 1,
			animationDurationMultiplierAddedRandomRange: 1
		);



		////////////////

		public SurfaceColdScene() {
		}


		////////////////

		public override Color GetSceneColor( SceneDrawData drawData ) {
			byte shade = (byte)Math.Min( drawData.Brightness * 255f, 255 );

			var color = new Color( shade, shade, shade, 128 );

			return color * drawData.Opacity;
		}


		////////////////

		public override void Update() {
			if( this.MostRecentDrawWorldRectangle.Width == 0 || this.MostRecentDrawWorldRectangle.Height == 0 ) {
				return;
			}

			Rectangle area = this.MostRecentDrawWorldRectangle; //UIHelpers.GetWorldFrameOfScreen();

			MistSceneDefinition.GenerateMists( area, this.MistDefinition );

			foreach( Mist mist in this.MistDefinition.Mists.ToArray() ) {
				mist.Update();

				if( !mist.IsActive ) {
					this.MistDefinition.Mists.Remove( mist );
				}
			}
		}


		////////////////

		public override void Draw(
				SpriteBatch sb,
				Rectangle rect,
				SceneDrawData drawData,
				float drawDepth ) {
			var mymod = SurroundingsMod.Instance;

			//float cavePercent = Math.Max( drawData.WallPercent - 0.5f, 0f ) * 2f;
			Color color = this.GetSceneColor( drawData );    // * (1f - cavePercent)

			if( mymod.Config.DebugModeInfo ) {
				DebugHelpers.Print( "SurfaceSnowScene",
					"mists: " + this.MistDefinition.Mists.Count +
					", pos: " + (int)(rect.X + Main.screenPosition.X)+", "+(int)(rect.Y + Main.screenPosition.Y) +
					", bright: " + drawData.Brightness.ToString("N2") +
					//", cave%: " + cavePercent.ToString("N2") +
					", opacity: " + drawData.Opacity.ToString("N2") +
					", color: " + color.ToString(),
					20
				);
			}

			this.MistDefinition.DrawAll( sb, color );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
