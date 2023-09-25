Shader "Unlit/HexUnlit"
{
    Properties
    {
        _VisibilityType("VisibilityType", Int) = 0
        [MainTexture] _MainTex ("Texture", 2D) = "white" {}
        [MainColor] _Color ("MainColor", Color) = (1,1,1,1) 
        _WarFogTex ("WarFog", 2D) = "white" {}
        _HiddenHexTex ("HiddenHex (RGB)", 2D) = "white" {}
        _BiomTex ("Biom", 2D) = "white" {}
        _ReliefTex ("Relief", 2D) = "white" {}
        _BuildTex ("Build", 2D) = "white" {}
        _ResourceTex("Resource", 2D) = "white" {}
        _PlayerColor("PLayerColor", Color) = (1,1,1,1) 
        _HasBuild ("HasBuildKeydown", Int) = 0
        _HasResource("HasResource", Int) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;
            float4 _PlayerColor;
            sampler2D _WarFogTex;
            sampler2D _HiddenHexTex;
            sampler2D _BiomTex;
            sampler2D _ReliefTex;
            sampler2D _BuildTex;
            sampler2D _ResourceTex;
            int _VisibilityType;
            float4 _MainTex_ST;
            bool _HasBuild;
            bool _HasResource;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target 
            {
                fixed4 col;
                fixed4 biomTex =  tex2D(_BiomTex, i.uv);             
                fixed4 reliefTex = tex2D(_ReliefTex, i.uv);
                fixed4 hexTex = tex2D(_BiomTex, i.uv) * (1 - reliefTex.a) + reliefTex * reliefTex.a; 
                
                if(_HasBuild)
                {                    
                    fixed4 buildTex = tex2D(_BuildTex, i.uv) * _PlayerColor;
                    hexTex = hexTex * (1 - buildTex.a) + buildTex * buildTex.a;
                }
                if(_HasResource)
                {                    
                    fixed4 resourceTex = tex2D(_ResourceTex, i.uv);
                    hexTex = hexTex * (1 - resourceTex.a) + resourceTex * resourceTex.a;
                }
                switch(_VisibilityType)
                {
                    case(0) : 
                        col = tex2D(_HiddenHexTex, i.uv); 
                    break;
                    case(1) : 
                        col = hexTex * tex2D(_WarFogTex, i.uv) * _Color;
                    break;
                    case(2) : 
                        col = hexTex * tex2D(_MainTex, i.uv);
                    break;
                    default:
                        col = tex2D(_MainTex, i.uv) * _Color;                             
                    break;
                }                
                col.a *= tex2D(_MainTex, i.uv).a; 
                return col;
            }
            ENDCG
        }
    }
}
