using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace Surroundings {
	public class SurroundingsConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ClientSide;



		////////////////

		[DefaultValue( false )]
		public bool DebugModeInfo = false;
	}
}
