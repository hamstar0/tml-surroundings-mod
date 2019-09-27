using System;
using HamstarHelpers.Helpers.Debug;
using Terraria;


namespace Surroundings.Scenes.Contexts {
	public abstract class CavernSceneBottom : CavernScene {
		public override int GetSceneTextureVerticalOffset( float yPercent, int texHeight ) {
			int offset = (int)( yPercent * (float)texHeight );
			offset -= 176;
			offset += SurroundingsMod.Instance.DebugOverlayOffset;

			return offset;
		}
	}
}
