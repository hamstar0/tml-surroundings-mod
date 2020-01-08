using HamstarHelpers.Services.Configs;
using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;


namespace Surroundings {
	public class SurroundingsConfig : StackableModConfig {
		public static SurroundingsConfig Instance => ModConfigStack.GetMergedConfigs<SurroundingsConfig>();



		////////////////

		public override ConfigScope Mode => ConfigScope.ClientSide;



		////////////////

		[DefaultValue( false )]
		public bool DebugModeSceneInfo = false;
		[DefaultValue( false )]
		public bool DebugModeMistInfo = false;
		[DefaultValue( false )]
		public bool DebugModeLayerEdit = false;

		//[Range(-9999, 9999)]
		//[DefaultValue( -128 )]
		//public int OverworldYOffsetAdd = -128;

		//[DefaultValue( 0.3f )]
		//[Range( -99f, 99f )]
		//public float OverworldYOffsetMul = 0.3f;
	}
}
