#include "UnityCG.cginc"
#include "UnityUI.cginc"

struct appdata_t
{
    float4 vertex : POSITION;
    float4 color : COLOR;
    float2 texcoord : TEXCOORD0;
    float2 flareParam : TEXCOORD2;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct v2f
{
    float4 vertex : SV_POSITION;
    fixed4 color : COLOR;
    float2 texcoord : TEXCOORD0;
    float4 worldPosition : TEXCOORD1;
    half4 flareParam : TEXCOORD2; // x: Intensity, y: Param1, z: Param2, w: Param3
    UNITY_VERTEX_OUTPUT_STEREO
};

sampler2D _MainTex;
fixed4 _Color;
float4 _MainTex_ST;

// Ref: http://emmettmcquinn.com/blog/graphics/2012/11/07/float-packing.html
half2 UnpackToVec2(float packed)
{
    const int Precision = 0xFFFF;
    const int Divider = Precision + 1;
    half2 unpacked;
    unpacked.x = packed % Divider;
    unpacked.y = packed / Divider;
    return unpacked;
}

half4 UnpackToVec4(float2 packed)
{
    half4 unpacked;
    unpacked.xy = UnpackToVec2(packed.x);
    unpacked.zw = UnpackToVec2(packed.y);
    return unpacked;
}

half UnpackNormalizedLog(half normalizedLog, half epsilon, half minLog, half maxLog)
{
    const int Precision = 0xFFFF;
    return pow(2.0, normalizedLog / Precision * (maxLog - minLog) + minLog) - epsilon;
}

half UnpackIntensity(half normalizedLog)
{
    return UnpackNormalizedLog(normalizedLog, 0.0001, -14, 14) / 100;
}

v2f vert(appdata_t v)
{
    v2f OUT;
    UNITY_SETUP_INSTANCE_ID(v);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
    OUT.worldPosition = v.vertex;
    OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

    OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

    OUT.color = v.color * _Color;

    OUT.flareParam = UnpackToVec4(v.flareParam);
    // unpack Intensity from normalized log to value
    OUT.flareParam.x = UnpackIntensity(OUT.flareParam.x);
    return OUT;
}
