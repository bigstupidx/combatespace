Shader "Custom/CustomizerShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MainTex2 ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MainTex2;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {

			fixed4 color = _Color; 
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 c2 = tex2D (_MainTex2, IN.uv_MainTex);

			if(c.a > 0.0 && (c.r!=1) && (c.g!=1) && (c.b!=1))
				o.Albedo =  c.rgba;
			else if(c2.a > 0.0 && (c2.r!=1) && (c2.g!=1) && (c2.b!=1))
				o.Albedo =  c2.rgba;
			else
				o.Albedo = _Color.rgb;

			// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
		//	o.Smoothness = _Glossiness;
			o.Alpha = 1;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
