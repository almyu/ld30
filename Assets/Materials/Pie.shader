Shader "Custom/Pie" {
	Properties {
		[PerRendererData]
		_MainTex("Base (RGB)", 2D) = "white" {}
		_RedSector("Red Sector", Color) = (1, 0, 0)
		_GreenSector("Green Sector", Color) = (0, 1, 0)
		_BlueSector("Blue Sector", Color) = (0, 0, 1)
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		Lighting Off
		Blend SrcColor One
		
		Pass {
			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			half3 _RedSector, _GreenSector, _BlueSector;

			float4 vert(float4 v : POSITION, float4 vuv : TEXCOORD0, fixed4 vclr : COLOR, out half2 uv, out half4 csmr) : SV_POSITION {
				uv = vuv.xy;
				csmr = half4(vuv.xy * -2.0 + 1.0, vclr.r, 1.0 - vclr.b);
				return mul(UNITY_MATRIX_MVP, v);
			}

			fixed4 frag(half2 uv, half4 csmr) : COLOR {
				const float rpi = 0.31830988618;

				half angle = (atan2(csmr.y, csmr.x) * rpi) * 0.5 + 0.5;

				half3 sector = lerp(_RedSector, lerp(_GreenSector, _BlueSector, step(csmr.w, angle)), step(csmr.z, angle));

				half4 smp = tex2D(_MainTex, uv);
				smp.rgb *= sector * smp.a;

				return smp;
			}
			ENDCG
		}
	} 
}
