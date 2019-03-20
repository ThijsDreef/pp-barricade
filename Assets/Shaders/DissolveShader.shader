Shader "Shaders/Dissolve3D"
{
    Properties
    {
        _MainTex("Base texture", 2D) = "white" {}
        _OcclusionMap("Occlusion", 2D) = "white" {}
        _OcclusionIntensity("Occlussion Intensity", Float) = 0
        _BumpMap("Normal Map", 2D) = "bump" {}
		_EmissionMap("EmmisionMap", 2D) = "white"
        _DissolveTexture("Disolve Texture", 2D) = "white" {}
        _DissolveY("Current Y of the disolve effect", Float) = 0
        _DissolveSize("Size of the effect", Float) = 2
        _StartingY("Starting point of the effect", Float) = -10
		_Glow("Intensity", Range(0, 10)) = 1
		_BlendAlpha("Blend Alpha", float) = 0
    }
 
    SubShader
    {
        Tags { "LightMode"="ForwardBase" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD4;
                half3 tspace0 : TEXCOORD1;
                half3 tspace1 : TEXCOORD2;
                half3 tspace2 : TEXCOORD3;
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                fixed4 diff : COLOR0;
            };

            sampler2D _MainTex;
			sampler2D _EmissionMap;
            sampler2D _OcclusionMap;
            float _OcclusionIntensity;
            sampler2D _BumpMap;
            float4 _MainTex_ST;
            sampler2D _DissolveTexture;
            float _DissolveY;
            float _DissolveSize;
            float _StartingY;
			half _Glow;

            v2f vert (float4 vertex : POSITION, float3 normal : NORMAL, float4 tangent : TANGENT, float2 uv : TEXCOORD0)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(vertex);
                o.uv = uv;
                o.worldPos = mul(unity_ObjectToWorld, vertex).xyz;
                half3 wNormal = UnityObjectToWorldNormal(normal);
                half nl = max(0, dot(wNormal, _WorldSpaceLightPos0.xyz));
                o.diff = nl * _LightColor0;
                half3 wTangent = UnityObjectToWorldDir(tangent.xyz);
                half tangentSign = tangent.w * unity_WorldTransformParams.w;
                half3 wBitangent = cross(wNormal, wTangent) * tangentSign;
                o.tspace0 = half3(wTangent.x, wBitangent.x, wNormal.x);
                o.tspace1 = half3(wTangent.y, wBitangent.y, wNormal.y);
                o.tspace2 = half3(wTangent.z, wBitangent.z, wNormal.z);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 c = 1;

                float transition = _DissolveY / 5;
                clip(_StartingY + (transition + (tex2D(_DissolveTexture, i.uv)) * _DissolveSize));
				fixed4 o = _Glow * tex2D(_EmissionMap, i.uv).r;
                fixed3 baseColor = tex2D(_MainTex, i.uv).rgb;
                fixed occlusion = tex2D(_OcclusionMap, i.uv).r;
                c.rgb *= baseColor;
                c.rgb *= (occlusion * _OcclusionIntensity);
                c *= i.diff;
				c += o;
                
                return c;
            }
            ENDCG
        }
    }
}