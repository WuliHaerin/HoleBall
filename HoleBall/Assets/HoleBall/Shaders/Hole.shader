Shader "HolloBall/Hole" {
  Properties {
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    _Color ("Color", Color) = (1, 1, 1, 1)
    _ColorSaturation ("Color saturation", Range(0, 1)) = 0
    [HideInInspector] _Cutout("Cutout", Range(0, 1)) = 0.5
  }

  SubShader {
    Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
    LOD 200

    CGPROGRAM
    #pragma surface surf Lambert alphatest:_Cutout
    #include "UnityCG.cginc"

    sampler2D _MainTex;
    float3 _Color;
    float _ColorSaturation;

    struct Input {
      float2 uv_MainTex;
      float3 worldPos;
    };

    void surf (Input IN, inout SurfaceOutput o) {
      fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
      o.Albedo = lerp(c.rgb, _Color, _ColorSaturation);
      o.Alpha = c.a;
    }
    ENDCG
  }
  Fallback "Legacy Shaders/Transparent/Cutout/VertexLit"
}
