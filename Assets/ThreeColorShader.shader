Shader "Custom/ThreeColorShader" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
		_Color1("Color 1", Color) = (1,1,1,1)
		_Color2("Color 2", Color) = (1,1,1,1)
		_Color3("Color 3", Color) = (1,1,1,1)
	}

		SubShader{
			Tags {"Queue" = "Transparent" "RenderType" = "Opaque"}
			Pass {
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				struct appdata {
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f {
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				float4 _MainTex_ST;
				float4 _Color1;
				float4 _Color2;
				float4 _Color3;

				v2f vert(appdata v) {
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					return o;
				}

				fixed4 frag(v2f i) : SV_Target {
					// «адаем границы дл€ каждой части
					float lowerBound1 = 0.0;
					float upperBound1 = 0.33;
					float lowerBound2 = 0.33;
					float upperBound2 = 0.67;
					float lowerBound3 = 0.67;
					float upperBound3 = 1.0;

					// ¬ычисл€ем, в какой части находитс€ текущий пиксель
					float pixelPos = i.uv.x;
					if (pixelPos < upperBound1) {
						return _Color1 * tex2D(_MainTex, i.uv);
					}
	 else if (pixelPos >= lowerBound2 && pixelPos < upperBound2) {
	  return _Color2 * tex2D(_MainTex, i.uv);
  }
else if (pixelPos >= lowerBound3 && pixelPos <= upperBound3) {
 return _Color3 * tex2D(_MainTex, i.uv);
}

return tex2D(_MainTex, i.uv);
}
ENDCG
}
		}
			FallBack "Diffuse"
}
