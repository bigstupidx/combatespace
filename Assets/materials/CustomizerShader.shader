Shader "Example/Detail" {
    Properties {
	_Color ("Color", Color) = (1,1,1,1)
      _MainTex ("Texture", 2D) = "white" {}
     // _BumpMap ("Bumpmap", 2D) = "bump" {}
     // _Detail ("Detail", 2D) = "gray" {}
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
     // sampler2D _BumpMap;
     // sampler2D _Detail;

      void surf (Input IN, inout SurfaceOutput o) {
			fixed4 color = _Color; 
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);

			if(c.a == 1 && ((c.r!=1) || (c.g!=1) || (c.b!=1)))
				o.Albedo =  c.rgba;	
			else if(c.a > 0.0 && ((c.r!=1) || (c.g!=1) || (c.b!=1)))
				o.Albedo =  (c.rgba + _Color.rgb)/2;		
			else
				o.Albedo = _Color.rgb;

         // o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
         // o.Albedo *= tex2D (_Detail, IN.uv_Detail).rgb * 2;
         // o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }