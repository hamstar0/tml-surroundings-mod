using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Surroundings {
	class SurrondingsPlayer : ModPlayer {
		public override void PreUpdate() {
			if( Main.myPlayer == this.player.whoAmI ) {
				Item heldItem = this.player.HeldItem;

				SurroundingsMod.Instance.HideOverlays = heldItem != null && !heldItem.IsAir;
				if( SurroundingsMod.Instance.HideOverlays ) {
					SurroundingsMod.Instance.HideOverlays = heldItem.type == ItemID.Binoculars
						|| (heldItem.type == ItemID.SniperRifle && Main.mouseRight);
				}
			}
		}
	}
}
