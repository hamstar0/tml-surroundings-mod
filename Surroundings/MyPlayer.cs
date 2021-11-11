using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Surroundings {
	class SurrondingsPlayer : ModPlayer {
		public override void OnEnterWorld( Player player ) {
			if( player.whoAmI == Main.myPlayer ) {
				if( Main.netMode != NetmodeID.Server ) {
					if( ModLoader.GetMod("Messages") != null ) {
						SurrondingsPlayer.OnEnterWorld_Messages_WeakRef();
					}
				}
			}
		}

		////

		private static void OnEnterWorld_Messages_WeakRef() {
			Messages.MessagesAPI.AddMessage(
				title: "Having framerate issues?",
				description: "Due to the work-in-progress nature of the Surroundings mod, if you are having any framerate "
					+"issues, consider disabling the mod in the Mods menu to see if conditions approve.",
				modOfOrigin: SurroundingsMod.Instance,
				alertPlayer: false,
				isImportant: false,
				parentMessage: Messages.MessagesAPI.ModInfoCategoryMsg,
				id: "SurroundingsPerformance"
			);
		}


		////////////////

		public override void PreUpdate() {
			if( Main.myPlayer == this.player.whoAmI ) {
				Item heldItem = this.player.HeldItem;

				SurroundingsMod.Instance.HideOverlays = heldItem != null && !heldItem.IsAir;
				if( SurroundingsMod.Instance.HideOverlays ) {
					SurroundingsMod.Instance.HideOverlays = 
						(heldItem.type == ItemID.Binoculars && Main.mouseItem?.type != ItemID.Binoculars)
						|| (heldItem.type == ItemID.SniperRifle && Main.mouseRight);
				}
			}
		}
	}
}
