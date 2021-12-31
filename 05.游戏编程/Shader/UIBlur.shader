Shader "Custom/UIBlur"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {} // ���������ǽ���ģ������Ķ��������
		_BlurSize("BlurSize", Range(0, 127)) = 1.0 // ���ܱ߲�����ƫ����
	}
		SubShader
		{
			 CGINCLUDE
					#include "UnityCG.cginc"

					sampler2D _MainTex;
					half4 _MainTex_TexelSize;
					fixed _BlurSize;

					struct v2f {
						float4 pos : SV_POSITION;
						half2 uv[5] : TEXCOORD0;
					};

					// ˮƽuv������չ����
					v2f vert_hor(appdata_img v)
					{
						v2f o;
						o.pos = UnityObjectToClipPos(v.vertex);
						half2 uv = v.texcoord;

						// �±��0��ʼ,�ֱ�ȡ����ǰ���أ�ƫ��1��λ��2��λ��uvλ��
						o.uv[0] = uv;
						o.uv[1] = uv + half2(_MainTex_TexelSize.x * 1.0, 0.0) * _BlurSize;
						o.uv[2] = uv - half2(_MainTex_TexelSize.x * 1.0, 0.0) * _BlurSize;

						o.uv[3] = uv + half2(_MainTex_TexelSize.x * 2.0, 0.0) * _BlurSize;
						o.uv[4] = uv - half2(_MainTex_TexelSize.x * 2.0, 0.0) * _BlurSize;

						return o;
					}

					// ˮƽuv������չ����
					v2f vert_ver(appdata_img v)
					{
						v2f o;
						o.pos = UnityObjectToClipPos(v.vertex);
						half2 uv = v.texcoord;

						// �±��0��ʼ,�ֱ�ȡ����ǰ���أ�ƫ��1��λ��2��λ��uvλ��
						o.uv[0] = uv;
						o.uv[1] = uv + half2(0.0, _MainTex_TexelSize.y * 1.0) * _BlurSize;
						o.uv[2] = uv - half2(0.0, _MainTex_TexelSize.y * 1.0) * _BlurSize;

						o.uv[3] = uv + half2(0.0, _MainTex_TexelSize.y * 2.0) * _BlurSize;
						o.uv[4] = uv - half2(0.0, _MainTex_TexelSize.y * 2.0) * _BlurSize;

						return o;
					}

					// ����ģ��ƬԪ
					fixed4 frag(v2f i) : SV_TARGET
					{
						// ģ�����ӣ��ֱ�����˵�ǰ���أ����£����ң�ƫ��1����λ��2����λ�ļ���Ȩ��
						// ���Ӿ�����ģ��������������Խ��Խ����Ч��Խ�ã���Ȼ�����Ͼ�Ҫ��һЩ
						half weight[3] = {0.4026, 0.2442, 0.0545};

					// ��ǰ����ƬԪ��ɫ������Ȩ�أ�
					fixed3 color = tex2D(_MainTex, i.uv[0]).rgb * weight[0];
					// ����Ȩ�ص������£����ң�������ɫ
					color += tex2D(_MainTex, i.uv[1]).rgb * weight[1];
					color += tex2D(_MainTex, i.uv[2]).rgb * weight[1];
					color += tex2D(_MainTex, i.uv[3]).rgb * weight[2];
					color += tex2D(_MainTex, i.uv[4]).rgb * weight[2];

					return fixed4(color, 1.0);
				}
			ENDCG

			Cull Off
			ZWrite Off
			Pass // 0:����ˮƽģ��
			{
				Name "BLUR_HORIZONTAL"
				CGPROGRAM
				#pragma vertex vert_hor
				#pragma fragment frag
				ENDCG
			}

			Pass // 1:����ֱģ��
			{
				Name "BLUR_VERTICAL"
				CGPROGRAM
				#pragma vertex vert_ver
				#pragma fragment frag
				ENDCG
			}
		}

		Fallback Off
}