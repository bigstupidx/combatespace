Shader "Example/Detail" {
    Properties {
	_Color ("Color", Color) = (1,1,1,1)
      _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert
      struct Input {
          float2 uv_MainTex;
          float2 uv_BumpMap;
          float2 uv_Detail;
      };

	  fixed4 _Color;
      sampler2D _MainTex;

      void surf (Input IN, inout SurfaceOutput o) {
			fixed4 color = _Color; 
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);

			//if(c.a == 1 && ((c.r!=1) || (c.g!=1) || (c.b!=1)))
			//	o.Albedo =  c.rgba;	
			//else if(c.a > 0.0 && ((c.r!=1) || (c.g!=1) || (c.b!=1)))
			//	o.Albedo =  (c.rgba + _Color.rgb)* 0.5;
			//else
			//	o.Albedo = _Color.rgb;

			if (c.a > 0.8)
				o.Albedo = c.rgba;
			else if (c.a > 0.1)
				o.Albedo =  (c.rgba + _Color.rgb)* 0.5;
			else
				o.Albedo = _Color.rgb;
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }