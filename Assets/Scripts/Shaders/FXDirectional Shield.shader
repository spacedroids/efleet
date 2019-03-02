// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Shield" {
    Properties {
        _Offset ("Time", Range (0, 1)) = 0.0
        _Color ("Tint (RGBA)", Color) = (1,1,1,1)
        _MainTex ("Texture (RGB)", 2D) = "white" {}
        _AlphaOverride("Alpha Override", Range(0, 1)) = 0.0
    }
    SubShader {
        ZWrite Off
        Tags { "Queue" = "Transparent" }
        Blend One One
        Cull Off

        Pass { 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 vertex : TEXCOORD1;
                float vColor : COLOR0;
            };

            const static int MAX_SHIELD_HITS = 10;
            uniform float _Offset;
            uniform float _RadialFactors[MAX_SHIELD_HITS];
            uniform float4 _Positions[MAX_SHIELD_HITS];
            uniform float _Alphas[MAX_SHIELD_HITS];
            uniform float _AlphaOverride;
            uniform float4 _Color;
            uniform sampler2D _MainTex : register(s1);
            
            v2f vert (appdata_base v) {
                v2f o;
                o.pos = UnityObjectToClipPos (v.vertex);
                v.texcoord.x = v.texcoord.x;
                v.texcoord.y = v.texcoord.y + _Offset;
                o.uv = TRANSFORM_UV (1);
                o.vertex = v.vertex;
                return o;
            }

            float4 frag (v2f f) : COLOR {
            	f.vColor = 0;
            	for (int i=0;i<MAX_SHIELD_HITS;i++) {
            		if (_Positions[i].x != 0 && _Positions[i].y != 0 && _Positions[i].z != 0) {
            			f.vColor += (_RadialFactors[i]/(distance (f.vertex.xyz, _Positions[i].xyz) * distance (f.vertex.xyz, _Positions[i].xyz))) * _Alphas[i];
            		}
            		f.vColor = max(f.vColor, _AlphaOverride);
            	}
            	float4 tex = tex2D (_MainTex, f.uv)*f.vColor*_Color*_Color.a ;
                return float4 (tex.r, tex.g, tex.b, tex.a);
            }
            ENDCG
        }
    }
    Fallback "Transparent/VertexLit"
}