Shader "Custom/SnowCoverShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_Displacement ("Displacement", Range(0, 1.0)) = 0.3

		//Snow texture
		_SnowTex("Snow Albedo (RGB)", 2D) = "white" {}

		_SnowDirection ("Snow Direction", Vector) = (0,1,0,0)
		_SnowAmount ("Snow Amount", Range(0,1)) = 0.1

    }

    SubShader
    {
        //Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        //#pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        //#pragma target 3.0
//=======================================================================================================================================
		#pragma surface surf BlinnPhong addshadow fullforwardshadows vertex:disp nolightmap
        #pragma target 4.6
//=======================================================================================================================================

        sampler2D _MainTex;
		sampler2D _SnowTex;

//=======================================================================================================================================
		 struct appdata {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _DispTex;
            float _Displacement;

            void disp (inout appdata v)
            {
                float d = tex2Dlod(_DispTex, float4(v.texcoord.xy,0,0)).r * _Displacement;
                v.vertex.xyz += v.normal * d;
            }
//=======================================================================================================================================
        struct Input
        {
            float2 uv_MainTex;
			float3 worldNormal;
			INTERNAL_DATA
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

		float4 _SnowDirection;
		float _SnowAmount; 


        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
			/*
			//Get the dot product between worldNormal and _SnowDirection, dots return the co sine from two normalized vectors
			float snowCoverage = (dot(IN.worldNormal, _SnowDirection) + 1) / 2;// Get a range from 0 to 1 for snow coverage (0 = no snow/ 1 = full snow)
			snowCoverage = 1 - snowCoverage;

			float snowStrength = snowCoverage < _SnowAmount;
			*/

			//Get the dot product between worldNormal and _SnowDirection, dots return the co sine from two normalized vectors
			float snowCoverage = (dot(IN.worldNormal, _SnowDirection) + 1) /2;// Get a range from 0 to 1 for snow coverage (0 = no snow/ 1 = full snow)			
				snowCoverage = 1 - snowCoverage;

			//float snowStrength = snowCoverage <  _SnowAmount  / 2;// < _SnowAmount;
			float snowStrength = snowCoverage < _SnowAmount / 2;// < _SnowAmount;
			//float snowStrength = (_SnowAmount / 2) / snowCoverage  ;// < _SnowAmount;



            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			//Use snow texture
			fixed4 snowColor = tex2D (_SnowTex, IN.uv_MainTex) * _Color;
            o.Albedo = c * (1 - snowStrength) + snowColor * snowStrength;

            // Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
