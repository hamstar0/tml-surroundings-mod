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



const float SampleOffsets[3] = { 0.0, 1.3846153846, 3.2307692308 };
const float SampleWeights[3] = { 0.2270270270, 0.3162162162, 0.0702702703 };



float ScreenWidth;
float ScreenHeight;



PixelShaderOutput HorizontalBlurPS( PixelShaderInput coords ) {
	PixelShaderOutput output;
    float4 color = tex2D( SpriteTextureSampler, coords.texPos ) * SampleWeights[0];

    for( int i=1; i<3; i++ ) {
		float4 offset = { (SampleOffsets[i] / ScreenWidth), 0.0, 0.0, 1.0 };
		
        color += tex2D( SpriteTextureSampler, (coords.texPos + offset) ) * SampleWeights[i];
        color += tex2D( SpriteTextureSampler, (coords.texPos - offset) ) * SampleWeights[i];
    }

	output.color = color;
	return output;
}


PixelShaderOutput VerticalBlurPS( PixelShaderInput coords ) {
	PixelShaderOutput output;
    float4 color = tex2D( SpriteTextureSampler, coords.texPos ) * SampleWeights[0];

    for( int i=1; i<3; i++ ) {
		float4 offset = { 0.0, (SampleOffsets[i] / ScreenHeight), 0.0, 1.0 };
		
        color += tex2D( SpriteTextureSampler, (coords.texPos + offset) ) * SampleWeights[i];
        color += tex2D( SpriteTextureSampler, (coords.texPos - offset) ) * SampleWeights[i];
    }
	
	output.color = color;
	return output;
}



technique OverlayDraw {
	pass P0 {
		PixelShader = compile ps_2_0 HorizontalBlurPS();
	}
	pass P1 {
		PixelShader = compile ps_2_0 VerticalBlurPS();
	}
	pass P2 {
		PixelShader = compile ps_2_0 HorizontalBlurPS();
	}
	pass P3 {
		PixelShader = compile ps_2_0 VerticalBlurPS();
	}
};
