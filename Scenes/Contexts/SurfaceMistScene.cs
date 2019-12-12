using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Surroundings.Scenes.Components.Mists;
using Terraria;


namespace Surroundings.Scenes.Contexts {
	public abstract class SurfaceMistScene : Scene {
		public override int DrawPriority => 1;

		////

		public override Vector2 FrameSize => new Vector2( Main.screenWidth, Main.screenHeight );

		public override float HorizontalTileScrollRate { get; } = 0f;

		public override float VerticalTileScrollRate { get; } = 0f;

		////

		public abstract MistSceneDefinition SceneMists { get; }



		////////////////

		public override float GetSceneOpacity( SceneDrawData drawData ) {
			return drawData.Opacity;
		}


		////////////////

		public override void Update() {
			if( this.RecentDrawnFrameInWorld.Width == 0 || this.RecentDrawnFrameInWorld.Height == 0 ) {
				return;
			}

			Rectangle area = this.RecentDrawnFrameInWorld; //UIHelpers.GetWorldFrameOfScreen();

			MistSceneDefinition.GenerateMists( area, this.SceneMists );
			this.SceneMists.Update();
		}


		////////////////

		public override void Draw(
					SpriteBatch sb,
					Rectangle rect,
					SceneDrawData drawData,
					float drawDepth ) {
			Color color = this.GetSceneColor(drawData) * this.GetSceneOpacity(drawData);

			if( SurroundingsConfig.Instance.DebugModeSceneInfo ) {
				DebugHelpers.Print( this.GetType().Name + "_" + this.Context.Layer,
					"mists: " + this.SceneMists.Mists.Count +
					", rect: " + rect +
					", bright: " + drawData.Brightness.ToString("N2") +
					//", cave%: " + cavePercent.ToString("N2") +
					", opacity: " + this.GetSceneOpacity(drawData).ToString("N2") +
					", base color: " + this.GetSceneColor(drawData).ToString(),
					20
				);
			}

			this.SceneMists.DrawAll( sb, color );
			//sb.Draw( tex, rect, null, color, 0f, default(Vector2), SpriteEffects.None, depth );
		}
	}
}
