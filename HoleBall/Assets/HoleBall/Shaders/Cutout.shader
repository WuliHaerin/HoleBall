Shader "HolloBall/CutoutDiffuse" {
  Properties {
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
    _HolePosition("Hole Position", Vector) = (0, 0, 0)
    _HoleRadius("Hole Radius", Range(0, 5)) = 1
    [HideInInspector] _Cutout("Cutout", Range(0, 1)) = 0.5
  }

  SubShader {
    Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
    LOD 200

    CGPROGRAM
    #pragma surface surf Lambert alphatest:_Cutout
    #include "UnityCG.cginc"

    sampler2D _MainTex;
    float3 _HolePosition;
    fixed _HoleRadius;

    struct Input {
      float2 uv_MainTex;
      float3 worldPos;
    };

    void surf (Input IN, inout SurfaceOutput o) {
      fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
      o.Albedo = c.rgb;

      float d = distance(_HolePosition, IN.worldPos);
      if(d < _HoleRadius) {
        o.Alpha = 0;
      }
      else {
        o.Alpha = c.a;
      }
    }
    ENDCG
  }
  Fallback "Legacy Shaders/Transparent/Cutout/VertexLit"
}
