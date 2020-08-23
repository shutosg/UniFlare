#include "UnityCG.cginc"
#include "UnityUI.cginc"

struct appdata_t
{
    float4 vertex : POSITION;
    float4 color : COLOR;
    float2 texcoord : TEXCOORD0;
    float2 uv : TEXCOORD2;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct v2f
{
    float4 vertex : SV_POSITION;
    fixed4 color : COLOR;
    float2 texcoord : TEXCOORD0;
    float4 worldPosition : TEXCOORD1;
    float2 flareParam : TEXCOORD2; // x: Intensity, y: Size
    UNITY_VERTEX_OUTPUT_STEREO
};

sampler2D _MainTex;
fixed4 _Color;
float4 _MainTex_ST;

v2f vert(appdata_t v)
{
    v2f OUT;
    UNITY_SETUP_INSTANCE_ID(v);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
    OUT.worldPosition = v.vertex;
    OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

    OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);

    OUT.color = v.color * _Color;

    OUT.flareParam = v.uv;
    return OUT;
}
