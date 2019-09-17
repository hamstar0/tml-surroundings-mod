﻿using System;
using System.Linq;
using HamstarHelpers.Classes.Tiles.TilePattern;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts.EventBloodMoon {
	public partial class SurfaceJungleSceneGame : Scene {
		public override SceneContext Context { get; }

		////

		public override int DrawPriority => 1;

		////

		public override Vector2 FrameSize => new Vector2( Main.screenWidth, Main.screenHeight );

		public override float HorizontalTileScrollRate => 0f;

		public override float VerticalTileScrollRate => 0f;

		////

		public override MistSceneDefinition MistDefinition => new MistSceneDefinition(
			mistCount: 10,
			spacingSquared: 4096f,
			aboveGroundMinHeight: 2 * 16,
			aboveGroundMaxHeight: 3 * 16,
			ground: new TilePattern( new TilePatternBuilder { HasWater = true } ),
			mistScale: new Vector2( 1.75f, 0.75f ),
			animationFadeTickRate: ( 1f / 60f ) * 4f,
			animationPeekTickRate: ( 1f / 60f ) * 4f,
			animationPeekTickRateAddedRandomRange: 4f
		);



		////////////////

		public SurfaceJungleSceneGame() {
			this.Context = new SceneContext(
				layer: SceneLayer.Game,
				isDay: null,
				vanillaBiome: null,
				currentEvent: null,
				customCondition: () => Main.bloodMoon
			);
			this.Context.Lock();
		}


		////////////////

		public override Color GetSceneColor( SceneDrawData drawData ) {
			byte shade = (byte)Math.Min( drawData.Brightness * 255f, 255 );
			byte darkShade = (byte)( (float)shade * 0.75f );

			var color = new Color( darkShade, shade, darkShade, 128 );

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
				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer,
					"mists: " + this.MistDefinition.Mists.Count +
					", pos: " + (int)( rect.X + Main.screenPosition.X ) + ", " + (int)( rect.Y + Main.screenPosition.Y ) +
					", bright: " + drawData.Brightness.ToString( "N2" ) +
					//", cave%: " + cavePercent.ToString("N2") +
					", opacity: " + drawData.Opacity.ToString( "N2" ) +
					", color: " + color.ToString(),
					20
				);
			}

			this.MistDefinition.DrawAll( sb, color );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
