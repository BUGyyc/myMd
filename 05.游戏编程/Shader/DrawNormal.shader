Shader "Custom/DrawNormal"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader
		{
		   Pass
			{
				CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
	#include "UnityCG.cginc"
				struct v2f
		{
			float4 pos:SV_POSITION;
			fixed4 color : COLOR;
		};

		v2f vert(appdata_base o) 
		{
			v2f v;
			v.pos = UnityObjectToClipPos(o.vertex);
			v.color.xyz = o.normal * 0.5 + 0.5;
			v.color.w = 1;
			return v;
		}

		fixed4 frag(v2f i) :SV_TARGET
		{
			return i.color;
		}
				ENDCG
			}
		}
			FallBack "Diffuse"
}
