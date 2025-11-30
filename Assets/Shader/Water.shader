Shader "Water" {
	Properties {
		_WaterColor ("Water Color", Vector) = (0,0,0,0)
		_WaterTexture ("Water Texture", 2D) = "white" {}
		_WaterTiling ("WaterTiling", Float) = 1
		_WaterSpeed ("Water Speed", Vector) = (0.02,0.01,0,0)
		_DistortionTexture ("DistortionTexture", 2D) = "white" {}
		_DistortionTiling ("DistortionTiling", Float) = 1
		_DistortionSpeed ("DistortionSpeed", Vector) = (-0.03,-0.01,0,0)
		_DistortionIntensity ("DistortionIntensity", Range(0, 1)) = 0.5
		_FresnelColor ("FresnelColor", Vector) = (1,1,1,0)
		_FresnelIntensity ("FresnelIntensity", Float) = 0.5
		_FresnelPower ("FresnelPower", Float) = 1
		[HideInInspector] _texcoord ("", 2D) = "white" {}
		[HideInInspector] __dirty ("", Float) = 1
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
	//CustomEditor "ASEMaterialInspector"
}