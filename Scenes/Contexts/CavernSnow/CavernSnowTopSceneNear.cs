using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;


namespace Surroundings.Scenes.Contexts.CavernSnow {
	public class CavernSnowTopSceneNear : CavernSnowScene {
		public override Vector2 FrameSize {
			get {
				Texture2D tex = this.GetSceneTexture();
				return new Vector2( (float)tex.Width * 1.5f, (float)tex.Height * 1.5f );
			}
		}

		public override float HorizontalTileScrollRate { get; } = 1.9f;

		////////////////

		private Texture2D CachedTex = null;



		////////////////

		public CavernSnowTopSceneNear() : base( SceneLayer.Near ) {
		}


		////////////////

		public override Texture2D GetSceneTexture() {
			if( this.CachedTex == null ) {
				this.CachedTex = SurroundingsMod.Instance.GetTexture( "Scenes/Contexts/CavernSnow/CavernSnow_Top" );
			}
			return this.CachedTex;
		}
	}
}
