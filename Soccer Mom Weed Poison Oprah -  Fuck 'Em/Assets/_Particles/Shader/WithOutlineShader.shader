Shader "Custom/WithOutlineShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
                float4 vertex : SV_POSITION;
				fixed2 uvOffset[8] : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			fixed4 _EdgeColor;
			fixed _AlphaThreshold;
			fixed _OffsetUV;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				
				o.uvOffset[0] = v.uv + fixed2(-_OffsetUV, -_OffsetUV);
				o.uvOffset[1] = v.uv + fixed2(-_OffsetUV, 0);
				o.uvOffset[2] = v.uv + fixed2(-_OffsetUV, _OffsetUV);
				
				o.uvOffset[3] = v.uv + fixed2(0, -_OffsetUV);
				o.uvOffset[4] = v.uv + fixed2(0, _OffsetUV);

				o.uvOffset[5] = v.uv + fixed2(_OffsetUV, -_OffsetUV);
				o.uvOffset[6] = v.uv + fixed2(_OffsetUV, 0);
				o.uvOffset[7] = v.uv + fixed2(_OffsetUV, _OffsetUV);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed alpha = 0;
				
				fixed a1 = tex2D(_MainTex, i.uvOffset[0]).a;
				fixed a2 = tex2D(_MainTex, i.uvOffset[1]).a;
				fixed a3 = tex2D(_MainTex, i.uvOffset[2]).a;
				fixed a4 = tex2D(_MainTex, i.uvOffset[3]).a;
				fixed a5 = tex2D(_MainTex, i.uvOffset[4]).a;
				fixed a6 = tex2D(_MainTex, i.uvOffset[5]).a;
				fixed a7 = tex2D(_MainTex, i.uvOffset[6]).a;
				fixed a8 = tex2D(_MainTex, i.uvOffset[7]).a;

				fixed alphaX = -a1 - 2 * a2 - a3 + a6 + 2 * a7 + a8;
				fixed alphaY = a1 + 2 * a4 + a6 - a3 - 2 * a5 - a8;
				//alpha = a1 + a2 + a3 + a4 + alpha;
				//alpha /= 5.0f;
				alpha = abs(alphaX) + abs(alphaY);

				col = alpha > _AlphaThreshold ? _EdgeColor : col;
				return col;
            }
            ENDCG
        }
    }
}
