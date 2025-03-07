Shader "Unlit/TestOutlineExtrudeOutline_2"
{
    Properties
    {
        // _MainTex ("Texture", 2D) = "white" {}
        
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0, 0.1)) = 0.03

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass {

            // Outline pass
            Cull Front
            // Cull Off

            CGPROGRAM

            #pragma vertex VertexProgram
            #pragma fragment FragmentProgram

            half _OutlineWidth;
            half4 _OutlineColor;

            struct appdata {
                float4 position : POSITION;
                float3 normal : NORMAL;
                float4 color : COLOR;    // Vertex color for smoothed normals
            };

            struct v2f
            {
    //            float4 position : SV_POSITION;
				//float4 color : COLOR;
                float4 vertex : SV_POSITION;
			};
            

            v2f VertexProgram(appdata v) {
                v2f o;
                v.position.xyz += v.normal * _OutlineWidth;
                o.vertex = UnityObjectToClipPos(v.position);

                return o;

            }

            half4 FragmentProgram(v2f i) : SV_TARGET {
                half4 col = _OutlineColor.rgba;
                return col;
            }

            ENDCG

        }
    }
}
