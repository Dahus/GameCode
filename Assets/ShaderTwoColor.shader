Shader "Unlit/ShaderTwoColor"
{
	Properties{
		 _Color1("Color 1", Color) = (1, 0, 0, 1)
		 _Color2("Color 2", Color) = (0, 0, 1, 1)
	}
		SubShader{
			Tags {"Queue" = "Transparent" "RenderType" = "Opaque"}
			LOD 100

			Pass {
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				struct appdata {
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f {
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				float4 _Color1;
				float4 _Color2;

				v2f vert(appdata v) {
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				fixed4 frag(v2f i) : SV_Target {
					if (i.uv.x < 0.5f) {
						return _Color1;
					}
	 else {
	  return _Color2;
  }
}
ENDCG
}
	}
		FallBack "Diffuse"
}
