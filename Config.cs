using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace Surroundings {
	public class SurroundingsConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ClientSide;



		////////////////

		[DefaultValue( false )]
		public bool DebugModeInfo = false;

		//[Range(-9999, 9999)]
		//[DefaultValue( -128 )]
		//public int OverworldYOffsetAdd = -128;

		//[DefaultValue( 0.3f )]
		//[Range( -99, 99 )]
		//public float OverworldYOffsetMul = 0.3f;
	}
}
