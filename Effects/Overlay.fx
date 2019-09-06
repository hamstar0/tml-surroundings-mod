#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif


sampler2D SpriteTextureSampler;


struct PixelShaderInput {
	//float2 screenPos : SV_POSITION;
	float4 texPos : TEXCOORD0;
	float4 color : COLOR;
};

struct PixelShaderOutput {
	float4 color : SV_TARGET0;
};



PixelShaderOutput MainPS( PixelShaderInput coords ) {
	PixelShaderOutput output;
	
	float4 color = tex2D( SpriteTextureSampler, coords.texPos );
	float opacity = abs(0.5 - coords.texPos.x) + abs(0.5 - coords.texPos.y);
	opacity = opacity * opacity;

	output.color = lerp( float4(0,0,0,0), color, opacity );
	
	return output;
}

technique OverlayDraw {
	pass P0 {
		PixelShader = compile ps_2_0 MainPS();
	}
};
