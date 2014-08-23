Shader "Custom/Pie" {
	Properties {
		[PerRendererData]
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Sectors("Sectors (nRGB)", Color) = (0.3333, 0.3333, 0.3333)
		_RedSector("Red Sector", Color) = (1, 0, 0)
		_GreenSector("Green Sector", Color) = (0, 1, 0)
		_BlueSector("Blue Sector", Color) = (0, 0, 1)
	}
	SubShader {
		Tags { "RenderType"="Transparent" }
		
		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		half3 _Sectors;
		half3 _RedSector, _GreenSector, _BlueSector;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			const float rpi = 0.31830988618;

			half2 cs = IN.uv_MainTex.xy * 2.0 - 1.0;
			half angle = (atan2(cs.y, cs.x) * rpi) * 0.5 + 0.5;

			half3 clr = lerp(_RedSector, lerp(_GreenSector, _BlueSector, step(1.0 - _Sectors.b, angle)), step(_Sectors.r, angle));

			half4 smp = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = smp.rgb * clr;
			o.Alpha = smp.a;
		}
		ENDCG
	} 
}
