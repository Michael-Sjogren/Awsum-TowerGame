Shader "Unlit/Outline"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_OutlineColor("Outline Color" , Color) = (0.0 , 0.0 , 0.0 , 1.0)
		_OutlineThickness("Outline Thickness" , Range(1.0 , 5.0)) = 1.0
	}

	CGINCLUDE
	#include "UnityCG.cginc"
		struct appdata 
		{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
		};

		struct v2f 
		{
			float4 pos : POSITION;	
			float3 normal : NORMAL;	
		};

		float _OutlineThickness;
		float4 _OutlineColor;

		v2f vert(appdata v) 
		{
			v.vertex.xyz *= _OutlineThickness;
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			return o;
		}

	ENDCG

	SubShader
	{
		Pass  // render outline
		{
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			half4 frag(v2f i) : COLOR
			{
				return _OutlineColor;
			}
			ENDCG
		}

		Pass  // normal render
		{
			ZWrite On


			Lighting On

			SetTexture[_MainTex]
			{
				ConstantColor[_OutlineColor]
			}

			SetTexture[_MainTex]
			{
				Combine previous * primary DOUBLE
			}
		}
	}
}
