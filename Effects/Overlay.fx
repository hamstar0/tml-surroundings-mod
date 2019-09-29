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
	float4 texPos : TEXCOORD0;
	float4 color : COLOR;
};

struct PixelShaderOutput {
	float4 color : SV_TARGET0;
};



PixelShaderOutput MainPS( PixelShaderInput coords ) {
	PixelShaderOutput output;
	
	float4 color = tex2D( SpriteTextureSampler, coords.texPos );

	//color.rgb = clamp( 0.5f - (atan(8 * color.rgb - 4) / 2.65f), 0.0f, 1.0f );

	float fromCenterDist = abs(0.5f - coords.texPos.x) + abs(0.5f - coords.texPos.y);
	float fadeCurve = 1.0f + (0.4f * log(fromCenterDist));
	float fade = min( 1.75f * fromCenterDist, fadeCurve );	//1.9f?
	fade = max( fade, 0 );

	output.color = lerp( float4(0.0f,0.0f,0.0f,0.0f), color, fade );
	
	return output;
}



technique OverlayDraw {
	pass P0 {
		PixelShader = compile ps_2_0 MainPS();
	}
};
