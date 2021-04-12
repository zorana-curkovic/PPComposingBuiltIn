Shader "Custom/BlitCopy" {
	Properties
	{
		_MainTex("Env Texture", any) = "" {}
		_CharacterTex("Char Texture", any) = "" {}
		_Color("Multiplicative color", Color) = (1.0, 1.0, 1.0, 1.0)
	}
		SubShader{
			Pass {
				ZTest Always Cull Off ZWrite Off
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex);
				uniform float4 _MainTex_ST;
				uniform float4 _CharacterTex_ST;
				uniform float4 _Color;
				sampler2D _CharacterTex;

				struct appdata_t {
					float4 vertex : POSITION;
					float2 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};
				struct v2f {
					float4 vertex : SV_POSITION;
					float2 texcoord : TEXCOORD0;
					UNITY_VERTEX_OUTPUT_STEREO
				};
				v2f vert(appdata_t v)
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.texcoord = TRANSFORM_TEX(v.texcoord.xy, _MainTex);
					return o;
				}
				fixed4 frag(v2f i) : SV_Target
				{
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
					float4 e = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, i.texcoord);
					float4 c = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_CharacterTex, i.texcoord);
					float4 r = c * c.a + e * (1 - c.a);
					return r;
				}
				ENDCG
			}
		}
			Fallback Off
}
