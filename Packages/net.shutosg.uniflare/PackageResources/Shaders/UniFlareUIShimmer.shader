// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "UniFlare/UI/Shimmer"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        [Header(Blending Factor)]
        [Enum(UnityEngine.Rendering.BlendMode)] _SrcFactor("Src Factor", Float) = 5 // SrcAlpha
        [Enum(UnityEngine.Rendering.BlendMode)] _DstFactor("Dst Factor", Float) = 10 // OneMinusSrcAlpha

        [Header(Stencil)]
        _CutoffAlpha ("cutoff alpha threthold [0.0-1.0]", Range(0.0, 1.0)) = 0
        _Stencil ("Stencil ID [0-255]", Range(0, 255)) = 0
        _StencilReadMask ("ReadMask [0-255]", Range(0, 255)) = 255
        _StencilWriteMask ("WriteMask [0-255]", Range(0, 255)) = 255
        [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("Stencil Comparison", Int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilOp ("Stencil Operation", Int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilFail ("Stencil Fail", Int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilZFail ("Stencil ZFail", Int) = 0

        [Header(General)]
        [Enum(UnityEngine.Rendering.ColorWriteMask)] _ColorMask ("Color Mask", Int) = 15
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
            Fail [_StencilFail]
            ZFail [_StencilZFail]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend [_SrcFactor] [_DstFactor]
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
            CGPROGRAM
            // Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members intensity,size)
            #pragma exclude_renderers d3d11
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"
            #include "UniFlareUI.cginc"

            #pragma multi_compile __ UNITY_UI_CLIP_RECT
            #pragma multi_compile __ UNITY_UI_ALPHACLIP

            fixed4 _TextureSampleAdd;
            fixed _CutoffAlpha;
            float4 _ClipRect;

            #define PI 3.14159265358979

            float rand(float n, float seed)
            {
                return frac(sin(n + seed) * 100000.);
            }

            float noise1d(float p, float seed)
            {
                float i = floor(p); // integer
                float f = frac(p); // fraction
                return lerp(rand(i, seed), rand(i + 1.0, seed), smoothstep(0.0, 1.0, f));
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                half4 color = IN.color;
                float2 pos = IN.texcoord * 2.0 - 1.0;
                float angle = atan2(pos.y, pos.x) / PI * 0.5 + 0.5;
            	float noise = noise1d(angle * 50.0 * IN.flareParam.y, 0);
                color *= noise - pow(length(pos), 0.5);
                color *= IN.flareParam.x;

                #ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip(color.a - 0.001);
                #endif

                clip(color.a - _CutoffAlpha);

                return color;
            }
            ENDCG
        }
    }
}