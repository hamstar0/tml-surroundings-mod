using System;
using ModLibsCore.Libraries.Debug;
using Terraria;


namespace Surroundings.Scenes.Contexts {
	public abstract class CavernSceneTop : CavernScene {
		public override int GetSceneTextureVerticalOffset( float yPercent, int frameHeight ) {
			int offset = (int)( yPercent * (float)frameHeight );
			offset -= frameHeight;
			offset -= 176;
			offset += SurroundingsMod.Instance.DebugOverlayOffset;

			return offset;
		}

		public override float GetSublayerColorFadePercent( float yPercent ) {
			float inv = 1f - yPercent;
			return Math.Min( inv * 3f, 1f );
		}
	}
}
