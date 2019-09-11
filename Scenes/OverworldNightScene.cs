using System;
using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.World;
using HamstarHelpers.Services.AnimatedTexture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;


namespace Surroundings.Scenes {
	public partial class OverworldNightScene : Scene {
		private AnimatedTexture Fly1;
		private AnimatedTexture Fly2;
		private AnimatedTexture Fly3;
		private AnimatedTexture Fly4;

		private Vector2 Fly1Pos;
		private Vector2 Fly2Pos;
		private Vector2 Fly3Pos;
		private Vector2 Fly4Pos; 

		private Vector2 Fly1Vel;
		private Vector2 Fly2Vel;
		private Vector2 Fly3Vel;
		private Vector2 Fly4Vel;

		private int Fly1Accel = 0;
		private int Fly2Accel = 0;
		private int Fly3Accel = 0;
		private int Fly4Accel = 0;


		////////////////

		public override int DrawPriority => 2;

		public override Vector2 Scale => new Vector2( 3.5f, 3.5f );

		public override float HorizontalTileScrollRate => 1f;

		public override float VerticalTileScrollRate => 0f;

		public override SceneContext Context { get; } = new SceneContext {
			Layer = SceneLayer.Near,
			//IsDay = true,
			VanillaBiome = VanillaBiome.Forest
		};



		////////////////

		public OverworldNightScene() {
			Func<AnimatedTexture, (int NextFrame, int Duration)> animator = ( animTex ) => {
				if( animTex.CurrentFrame == 3 ) {
					if( Main.rand.NextFloat() >= 0.95f ) {
						return (0, 8);
					} else {
						return (2, 8);
					}
				}
				return (animTex.CurrentFrame + 1, 8);
			};

			this.Fly1 = AnimatedTexture.Create( Main.npcTexture[NPCID.Firefly], 4, animator );
			this.Fly2 = AnimatedTexture.Create( Main.npcTexture[NPCID.Firefly], 4, animator );
			this.Fly3 = AnimatedTexture.Create( Main.npcTexture[NPCID.Firefly], 4, animator );
			this.Fly4 = AnimatedTexture.Create( Main.npcTexture[NPCID.Firefly], 4, animator );

			this.Fly1Pos = new Vector2( Main.rand.Next(0, Main.screenWidth), Main.rand.Next(0, Main.screenWidth) );
			this.Fly2Pos = new Vector2( Main.rand.Next(0, Main.screenWidth), Main.rand.Next(0, Main.screenWidth) );
			this.Fly3Pos = new Vector2( Main.rand.Next(0, Main.screenWidth), Main.rand.Next(0, Main.screenWidth) );
			this.Fly4Pos = new Vector2( Main.rand.Next(0, Main.screenWidth), Main.rand.Next(0, Main.screenWidth) );
		}


		////////////////

		public Color GetSceneColor( float brightness ) {
			//float shadeScale = ( this.IsNear ? 192f : 255f ) * brightness;
			float shadeScale = 255f * brightness;
			byte shade = (byte)Math.Min( shadeScale, 255 );

			var color = new Color( shade, shade, shade, 255 );

			return color;
		}

		public float GetSceneVerticalRangePercent( Vector2 center ) {
			int tileY = (int)( center.Y / 16 );
			float range = WorldHelpers.SurfaceLayerBottom - WorldHelpers.SurfaceLayerTop;
			float yPercent = (float)( tileY - WorldHelpers.SurfaceLayerTop ) / range;
			return 1f - yPercent;
		}

		public int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			var mymod = SurroundingsMod.Instance;
			float height = (float)texHeight * this.Scale.Y;

			int offset = (int)( yPercent * (float)height * 0.3f );
			offset += -128;
			return offset;
		}


		////////////////

		public override void Update() {
			this.AnimateFlies();
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
			Color color = this.GetSceneColor(drawData.Brightness) * cavePercent * opacity;

			if( mymod.Config.DebugModeInfo ) {
				DebugHelpers.Print( "OverworldScene",
					"brightness: " + drawData.Brightness +
					", cavePercent: " + cavePercent.ToString("N2") +
					", color: " + color.ToString(),
					20
				);
			}

			float yPercent = this.GetSceneVerticalRangePercent( drawData.Center );

			rect.Y += this.GetSceneTextureVerticalOffset( yPercent, Main.screenHeight );

			this.DrawFlies( sb, rect, color );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}


		private void DrawFlies( SpriteBatch sb, Rectangle rect, Color color ) {
			float xScale = rect.Width / Main.screenWidth;
			float yScale = rect.Height / Main.screenHeight;

			Vector2 fly1Pos = this.Fly1Pos;
			fly1Pos.X += (float)rect.X * xScale;
			fly1Pos.Y += (float)rect.Y * yScale;
			Vector2 fly2Pos = this.Fly2Pos;
			fly2Pos.X += (float)rect.X * xScale;
			fly2Pos.Y += (float)rect.Y * yScale;
			Vector2 fly3Pos = this.Fly3Pos;
			fly3Pos.X += (float)rect.X * xScale;
			fly3Pos.Y += (float)rect.Y * yScale;
			Vector2 fly4Pos = this.Fly4Pos;
			fly4Pos.X += (float)rect.X * xScale;
			fly4Pos.Y += (float)rect.Y * yScale;

			this.Fly1.Draw( sb, fly1Pos, this.Fly1.CurrentFrame <= 2 ? Color.White : color );
			this.Fly2.Draw( sb, fly2Pos, this.Fly2.CurrentFrame <= 2 ? Color.White : color );
			this.Fly3.Draw( sb, fly3Pos, this.Fly3.CurrentFrame <= 2 ? Color.White : color );
			this.Fly4.Draw( sb, fly4Pos, this.Fly4.CurrentFrame <= 2 ? Color.White : color );
		}
	}
}
