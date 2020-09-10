


    //应用阶段数据传入几何阶段
    struct appdata
	{
		float4 vertex : POSITION;		//顶点
		float4 tangent : TANGENT;		//切线
		float3 normal : NORMAL;			//法线
		float4 texcoord : TEXCOORD0;	        //UV1
		float4 texcoord1 : TEXCOORD1;	        //UV2
		float4 texcoord2 : TEXCOORD2;	        //UV3
		float4 texcoord3 : TEXCOORD3;	        //UV4
		fixed4 color : COLOR;			//顶点色
	};




    