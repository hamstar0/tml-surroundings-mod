using System;
using ModLibsCore.Libraries.Debug;
using Terraria;


namespace Surroundings.Scenes.Contexts {
	public abstract class CavernSceneBottom : CavernScene {
		public override int GetSceneTextureVerticalOffset( float yPercent, int frameHeight ) {
			int baseOffset = (int)( yPercent * (float)frameHeight );
			int offset = (Main.screenHeight - frameHeight) + baseOffset;
			offset += SurroundingsMod.Instance.DebugOverlayOffset;

			return offset;
		}

		public override float GetSublayerColorFadePercent( float yPercent ) {
			return Math.Min( yPercent * 3f, 1f );
		}
	}
}
