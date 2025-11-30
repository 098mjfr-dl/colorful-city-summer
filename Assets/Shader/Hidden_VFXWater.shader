Shader "Hidden/VFXWater" {
	Properties {
		_ParticleTextureA ("ParticleTextureA", 2D) = "white" {}
		_Opacity ("Opacity", Range(0, 1)) = 0.9
		[MaterialToggle] _R ("R", Float) = 0
		[MaterialToggle] _G ("G", Float) = 0
		[MaterialToggle] _B ("B", Float) = 0
		[MaterialToggle] _A ("A", Float) = 0
		[HideInInspector] _Cutoff ("Alpha cutoff", Range(0, 1)) = 0.5
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Diffuse"
	//CustomEditor "ShaderForgeMaterialInspector"
}