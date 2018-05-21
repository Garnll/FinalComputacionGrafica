Shader "Hidden/ShadowStep" {
	Properties{
		_MainTex("Tex", 2D) = "white" {}
		_LastFrame("LastFrame", 2D) = "white" {}
		_FirstFrame("FirstFrame", 2D) = "white" {}
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

		sampler2D _LastFrame;
		sampler2D _FirstFrame;
		float timer;

		float4 frag(v2f_img i) : COLOR
		{

		float4 main = tex2D(_MainTex, i.uv);
		float4 last = tex2D(_LastFrame, i.uv);

		float result = last + (main - last);

		return last;
		}
		ENDCG
		}
	}
		FallBack off
}
