https://docs.unity3d.com/2020.3/Documentation/Manual/SL-Reference.html


https://developer.unity.cn/projects/613acaceedbc2a0021ab14f8


1、appdata_base： 包含顶点位置，法线和一个纹理坐标。
2、appdata_tan：包含顶点位置，切线，法线和一个纹理坐标。
3、appdata_full：包含位置、法线、切线、顶点色和两个纹理坐标。



struct appdata_base {
    float4 vertex : POSITION; //顶点坐标
    float3 normal : NORMAL;//法线
    float4 texcoord : TEXCOORD0;//UV
};
struct appdata_tan {

    float4 vertex : POSITION;
    float4 tangent : TANGENT;
    float3 normal : NORMAL;
    float4 texcoord : TEXCOORD0;
};
struct appdata_full {
    float4 vertex : POSITION;//顶点坐标
    float4 tangent : TANGENT;//正切
    float3 normal : NORMAL;//法线
    float4 texcoord : TEXCOORD0;//第一层UV
    float4 texcoord1 : TEXCOORD1; //第二层UV
    fixed4 color : COLOR; //颜色
};


https://www.cnblogs.com/leeplogs/p/7339097.html



