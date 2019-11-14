Shader "Custom/SnowCoverShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0

			//Snow texture
			_SnowTex("Snow Albedo (RGB)", 2D) = "white" {}

			_SnowDirection("Snow Direction", Vector) = (0,1,0,0)
			_SnowAmount("Snow Amount", Range(0,1)) = 0.1

	}

		SubShader
			{
				//Tags { "RenderType"="Opaque" }
				LOD 200

				CGPROGRAM
				// Physically based Standard lighting model, and enable shadows on all light types
				#pragma surface surf Standard fullforwardshadows

				// Use shader model 3.0 target, to get nicer looking lighting
				#pragma target 3.0

				sampler2D _MainTex;
				sampler2D _SnowTex;

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



				// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
				// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
				// #pragma instancing_options assumeuniformscaling
				UNITY_INSTANCING_BUFFER_START(Props)
					// put more per-instance properties here
				UNITY_INSTANCING_BUFFER_END(Props)

				void surf(Input IN, inout SurfaceOutputStandard o)
				{
					//Get the dot product between worldNormal and _SnowDirection, dots return the co sine from two normalized vectors
					float snowCoverage = (dot(IN.worldNormal, _SnowDirection) + 1) / 2;// Get a range from 0 to 1 for snow coverage (0 = no snow/ 1 = full snow)
					snowCoverage = 1 - snowCoverage;

					float snowStrength = snowCoverage < _SnowAmount;

					// Albedo comes from a texture tinted by color
					fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;

					//Use snow texture
					fixed4 snowColor = tex2D(_SnowTex, IN.uv_MainTex) * _Color;
					o.Albedo = c * (1 - snowStrength) + snowColor * snowStrength;

					// Metallic and smoothness come from slider variables
					o.Metallic = _Metallic;
					o.Smoothness = _Glossiness;
					o.Alpha = c.a;
				}
				ENDCG
			}
				FallBack "Diffuse"
}
