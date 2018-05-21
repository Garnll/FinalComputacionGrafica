Shader "Hidden/ZaWarudo" {
	Properties{
		_MainTex("Tex", 2D) = "white" {}
		_Color("Color", Color) = (0,1,0,1)
	}

		SubShader{
			Pass{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			float4 _MainTex_TexelSize;
			float4 _Color;



		float4 frag(v2f_img i) : COLOR{
		
		
		float4 pant = tex2D(_MainTex, i.uv);

		float _circlePercent = 50;

		float h = _ScreenParams.x / 2;
		float k = _ScreenParams.y / 2;

		float rMax = 500;


		float r = sin(_Time.y * 2) * (rMax * 0.5) + rMax * 0.5;

		float hNormalized = h / _ScreenParams.x;
		float kNormalized = k / _ScreenParams.y;

		float2 center = float2(h,k);
		float dis = distance(i.uv * _ScreenParams, center);


		float width = _MainTex_TexelSize.x;
		float heigth = _MainTex_TexelSize.y;
		float Ax = 1;
		float Ay = 1;

		float a = 0;
		float x = 0;
		float y = 0;
		float4 result = 0;

		if (dis <= r)
		{

			float ruido = sin(_Time.y) + (2);


			x = ruido * sin(r);
			y = ruido * cos(r);
			


			result = tex2D(_MainTex, float2(i.uv.x + (x)*width*Ax, i.uv.y + (y)*heigth*Ay));

			result.rgb = float3(1 - result.r, 1 - result.g, 1 - result.b);
		}
		else
		{
			result = tex2D(_MainTex, float2(i.uv.x + (x)*width*Ax, i.uv.y + (y)*heigth*Ay));
		}

		return result;

		}
		ENDCG
		}
	}
		FallBack off
}
