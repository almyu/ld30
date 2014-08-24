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

			struct fragdata {
				half2 uv : TEXCOORD0;
				half4 csmr : TEXCOORD1;
			};

			float4 vert(appdata_full i, out fragdata o) : SV_POSITION {
				o.uv = i.texcoord.xy;
				o.csmr = half4(i.texcoord.xy * -2.0 + 1.0, i.color.r, 1.0 - i.color.b);
				return mul(UNITY_MATRIX_MVP, i.vertex);
			}

			fixed4 frag(fragdata i) : COLOR {
				const float rpi = 0.31830988618;

				half angle = (atan2(i.csmr.y, i.csmr.x) * rpi) * 0.5 + 0.5;

				half3 sector = lerp(_RedSector, lerp(_GreenSector, _BlueSector, step(i.csmr.w, angle)), step(i.csmr.z, angle));

				half4 smp = tex2D(_MainTex, i.uv);
				smp.rgb *= sector * smp.a;

				return smp;
			}
			ENDCG
		}
	} 
}
