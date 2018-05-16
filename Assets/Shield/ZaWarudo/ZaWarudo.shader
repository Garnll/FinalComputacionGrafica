Shader "Hidden/ZaWarudo" {
	Properties{
		_MainTex("Tex", 2D) = "white" {}
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



		float4 frag(v2f_img i) : COLOR{
		
		//float r = sqrt(pow(i.uv.x, 2) + pow(i.uv.y, 2));
		float a = (atan(0.5 - i.uv.y/0.5 - i.uv.x));

		float w = _MainTex_TexelSize.x;
		float h = _MainTex_TexelSize.y;
		float Ax = 10;
		float Ay = 10;
		
		float centerX = 0.1;
		float centerY = 0.1;



		float radius = sin(_Time.y * 2) * (10 * 0.5)+5;
		float factor = sin(_Time.y) * 0.5;
		float ruido = sin(_Time.y) * 0.5 + (10 - 0.5);

		float dist = radius-0.5 + factor*cos(ruido*a);

		float newX = dist * cos(a);
		float newY = dist * sin(a);
		
		//float newDist = sqrt(pow(newX, 2) + pow (newY, 2));

		float4 result = tex2D(_MainTex, float2(i.uv.x + (dist)*w*Ax - centerX, i.uv.y + (dist)*h*Ay) - centerY);
		return result;
		

		
		}
		ENDCG
		}
	}
		FallBack off
}
