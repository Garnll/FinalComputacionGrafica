Shader "Hidden/ShadowStep" {
	Properties{
		_MainTex("Tex", 2D) = "white" {}
		_LastFrame("LastFrame", 2D) = "white" {}
		_Factor("Factor", Range (0,1)) = 0.5
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
		uniform sampler2D _LastFrame;
		float4 _LastFrame_TexelSize;
		float _Factor;


		//float timer;

		float4 frag(v2f_img i) : COLOR
		{

		float4 c1 = tex2D(_MainTex, i.uv);
		i.uv.y = 1- i.uv.y;
		float c2 = tex2D(_LastFrame, i.uv);

		float4 c3 = lerp(c1, c2, _Factor);

		return c3;
		}
		ENDCG
		}
	}
		FallBack off
}
