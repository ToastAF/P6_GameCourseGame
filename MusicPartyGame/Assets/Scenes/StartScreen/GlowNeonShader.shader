Shader "Custom/GlowNeonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EmissionColor ("Emission Color", Color) = (0, 1, 1, 1)
        _EmissionIntensity ("Emission Intensity", Float) = 5.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _EmissionColor;
            float _EmissionIntensity;

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb += _EmissionColor.rgb * _EmissionIntensity;
                return col;
            }
            ENDCG
        }
    }
}
