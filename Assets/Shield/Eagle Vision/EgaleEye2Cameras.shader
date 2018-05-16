Shader "Hidden/EagleEye2Cameras" {

	Properties{
		_MainTex("Tex", 2D) = "white" {}
	    _Mascara("Mascara", 2D) = "white" {}
		_TerrainColor("RGB", Color) = (0,1,0,1)
	    _ObjectColor("RGB", Color) = (0,1,0,1)
		_BorderColor("RGB", Color) = (1,1,1,1)
	}

		SubShader{
			Pass{
			CGPROGRAM
	#pragma vertex vert_img
	#pragma fragment frag
	#pragma fragmentoption ARB_precision_hint_fastest
	#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
	        sampler2D _Mascara;
			float4 _TerrainColor;
			float4 _ObjectColor;
			float4 _BorderColor;


			float4 frag(v2f_img i) : COLOR{

				float4 c1 = tex2D(_Mascara, i.uv);
				

				float s = 10;
				float wx = 0.0005;
				float wy = 0.0005;

				float4 spaces = float4(0, 0, 0, 0);

				float3x3 m = float3x3(
					-1, -1, -1,
					-1, 8, -1,
					-1, -1, -1
					);

				for (int j = -1; j <= 1; j++) {
					for (int g = -1; g <= 1; g++) {
						spaces += tex2D(_Mascara, float2(i.uv.x + j * wx*s, i.uv.y + g * wy*s))*m[j + 1][g + 1];
					}
				}
				float4 kernel = spaces;
				
				float4 c2 = c1;
				float4 c3 = c1 * spaces;
				if (c2.r > 0 && c2.g > 0 && c2.b > 0)
				{
					c2.rgb = float3(1, 1, 1);
				}
				


				float4 tex = tex2D(_MainTex, i.uv);
				float4 texColor = tex *_TerrainColor;

				float4 hole = texColor * (1 - c2);

				
				float4 result = hole + (c1 *_ObjectColor + c3*_BorderColor)*5;

				return result;
		
	}
		ENDCG
		}
	}
		FallBack off
}

