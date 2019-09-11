using System;
using System.Collections.Generic;
using System.Linq;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using HamstarHelpers.Services.AnimatedTexture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;


namespace Surroundings.Scenes {
	public partial class OverworldNightScene : Scene {
		private IList<Firefly> Flies = new List<Firefly>();

		private bool FliesInitialized = false;


		////////////////

		public override int DrawPriority => 2;

		public override Vector2 Scale => new Vector2( 1f, 1f );

		public override float HorizontalTileScrollRate => 1f;

		public override float VerticalTileScrollRate => 0f;

		public override SceneContext Context { get; } = new SceneContext {
			Layer = SceneLayer.Near,
			//IsDay = true,
			VanillaBiome = VanillaBiome.Forest
		};



		////////////////

		public OverworldNightScene() {
			if( Main.rand != null && Main.npcTexture[NPCID.Firefly] != null ) {
				this.InitializeFireflies();
			}
		}

		private void InitializeFireflies() {
			this.FliesInitialized = true;

			Func<AnimatedTexture, (int NextFrame, int Duration)> animator = ( animTex ) => {
				if( animTex.CurrentFrame >= (animTex.MaxFrames - 1) ) {
					if( Main.rand.NextFloat() >= 0.9f ) {
						return (0, 8);
					} else {
						return (2, 8);
					}
				}
				return (animTex.CurrentFrame + 1, 8);
			};

			for( int i = 0; i < 8; i++ ) {
				this.Flies.Add( new Firefly {
					Animation = AnimatedTexture.Create( Main.npcTexture[NPCID.Firefly], 4, animator ),
					ScrPos = new Vector2( Main.rand.Next( 0, Main.screenWidth ), Main.rand.Next( 0, Main.screenHeight ) ),
					Vel = new Vector2( Main.rand.NextFloat() - 0.5f, Main.rand.NextFloat() - 0.5f ),
					Accel = 0
				} );
			}
		}


		////////////////

		public Color GetSceneColor( float brightness ) {
			//float shadeScale = ( this.IsNear ? 192f : 255f ) * brightness;
			float shadeScale = 255f * brightness;
			byte shade = (byte)Math.Min( shadeScale, 255 );

			var color = new Color( shade, shade, shade, 255 );

			return color;
		}


		////////////////

		public override void Update() {
			if( !this.FliesInitialized ) {
				if( Main.npcTexture[NPCID.Firefly] == null ) {
					Main.instance.LoadNPC( NPCID.Firefly );
				}
				if( Main.rand != null ) {
					this.InitializeFireflies();
				}
			}

			if( this.FliesInitialized ) {
				this.AnimateFlies();
			}
		}


		////////////////

		public override void Draw(
				SpriteBatch sb,
				Rectangle rect,
				SceneDrawData drawData,
				float opacity,
				float drawDepth ) {
			if( !this.FliesInitialized ) { return; }
			if( Main.dayTime ) { return; }

			var mymod = SurroundingsMod.Instance;

			float cavePercent = Math.Max( drawData.WallPercent - 0.5f, 0f ) * 2f;
			Color color = this.GetSceneColor(drawData.Brightness) * (1f - cavePercent) * opacity;

			if( mymod.Config.DebugModeInfo ) {
				DebugHelpers.Print( "OverworldNightScene",
					"rect: " + rect +
					", bright: " + drawData.Brightness +
					", cave%: " + cavePercent.ToString("N2") +
					", color: " + color.ToString() +
					", opacity: " + opacity +
					", flies: " + string.Join(", ", this.Flies.Select( f=>(int)f.ScrPos.X + ":" + (int)f.ScrPos.Y) ),
					20
				);
			}

			this.DrawFlies( sb, rect, color, opacity );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}


		private void DrawFlies( SpriteBatch sb, Rectangle rect, Color color, float opacity ) {
			float xScale = rect.Width / Main.screenWidth;
			float yScale = rect.Height / Main.screenHeight;

			foreach( Firefly fly in this.Flies ) {
				Vector2 pos = fly.ScrPos;
				pos.X += (float)rect.X * xScale;
				pos.Y += (float)rect.Y * yScale;

				Color flyColor = fly.Animation.CurrentFrame <= 1 ?
					Color.White * opacity :
					color;

				fly.Animation.Draw( sb, pos, flyColor );
			}
		}
	}
}
