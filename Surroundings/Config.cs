﻿using System;
using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;


namespace Surroundings {
	public partial class SurroundingsConfig : ModConfig {
		public static SurroundingsConfig Instance => ModContent.GetInstance<SurroundingsConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ClientSide;



		////////////////

		[DefaultValue( true )]
		public bool EnableOverlays { get; set; } = true;

		////////////////

		[DefaultValue( false )]
		public bool DebugModeSceneInfo { get; set; } = false;
		[DefaultValue( false )]
		public bool DebugModeMistInfo { get; set; } = false;
		[DefaultValue( false )]
		public bool DebugModeLayerEdit { get; set; } = false;

		//[Range(-9999, 9999)]
		//[DefaultValue( -128 )]
		//public int OverworldYOffsetAdd = -128;

		//[DefaultValue( 0.3f )]
		//[Range( -99f, 99f )]
		//public float OverworldYOffsetMul = 0.3f;
	}
}
