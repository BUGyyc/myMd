Shader "Custom/InvertColor"
{
	Properties
	{

	}
	SubShader
	{
		Tags { "RenderType" = "Transparent" }
		LOD 200

		GrabPass
	{
		"_GrabTexture"
	}

	Pass
	{
			CGPROGRAM
	#pragma vertex vert
#pragma fragment frag
		#include "UnityCG.cginc"
			struct v2f
			{
				float4 grabPos : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

	v2f vert(appdata_base o)
	{
		v2f v;
		v.pos = UnityObjectToClipPos(o.vertex);
		v.grabPos = ComputeGrabScreenPos(v.pos);
		return v;
	}

	sampler2D _GrabTexture;

	half4 frag(v2f i) :SV_Target
	{
		half4 bkColor = tex2Dproj(_GrabTexture, i.grabPos);
		return 1 - bkColor;
	}



		ENDCG
	}



	}
		FallBack "Diffuse"
}
