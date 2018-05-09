// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/GrayScale"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0
		_EffectAmount("Effect Amount", Range(0, 1)) = 1.0

		// required for UI.Mask
		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255
		_ColorMask("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
		}

		// required for UI.Mask
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp]
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}
		
		Cull Off
		Lighting Off
		ZWrite Off
		ZTest[unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask[_ColorMask]
			
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile DUMMY PIXELSNAP_ON
			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			// required for UI.Mask
			#pragma multi_compile __ UNITY_UI_CLIP_RECT

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

		struct appdata_t
		{
			float4 vertex   : POSITION;
			float4 color    : COLOR;
			float2 texcoord : TEXCOORD0;
		};

		struct v2f
		{
			float4 vertex   : SV_POSITION;
			fixed4 color	: COLOR;
			half2 texcoord  : TEXCOORD0;
			float4 worldPosition : TEXCOORD1;
		};

		sampler2D _MainTex;
		uniform float _EffectAmount;	// grayscale amount 
		fixed4 _Color;
		fixed4 _TextureSampleAdd;
		float4 _ClipRect;
		float4 _MainTex_ST;

		v2f vert(appdata_t IN)
		{
			v2f OUT;

			OUT.vertex = UnityObjectToClipPos(IN.vertex);
			OUT.texcoord = IN.texcoord;
			OUT.color = IN.color * _Color;
			OUT.worldPosition = IN.vertex;

			#ifdef PIXELSNAP_ON
			OUT.vertex = UnityPixelSnap(OUT.vertex);
			#endif
			
			return OUT;
		}


		fixed4 frag(v2f IN) : COLOR
		{
			// unsure what texture sample add
			// half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

			half4 color = tex2D(_MainTex, IN.texcoord);
			color.rgb = lerp(color.rgb, dot(color.rgb, float3(0.3, 0.59, 0.11)), _EffectAmount);
			color = color * IN.color;
			
			// required for UI.Mask
			#ifdef UNITY_UI_CLIP_RECT
			color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
			#endif

			#ifdef UNITY_UI_ALPHACLIP
			clip(color.a - 0.001);
			#endif
			
			return color;
		}
			ENDCG
		}
	}
		Fallback "Sprites/Default"
}
