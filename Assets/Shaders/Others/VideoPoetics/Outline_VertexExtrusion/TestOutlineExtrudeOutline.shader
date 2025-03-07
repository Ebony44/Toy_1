Shader "Unlit/TestOutlineExtrudeOutline"
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

            // 

            struct appdata {
                float4 position : POSITION;
                float3 normal : NORMAL;
                float4 color : COLOR;    // Vertex color for smoothed normals
            };

            float4 VertexProgram(
                    //float4 position : POSITION,
                    //float3 normal : NORMAL
                    appdata v
                    ) : SV_POSITION {

                // position.xyz += normal * _OutlineWidth;

                // float4 positonWithColor = 

                // Move vertex along normal vector in object space.
                // positionOS += vertexColor * width;

                float4 tweakedObjectSpacePos = v.position + (v.color * _OutlineWidth);

                // float4 clipPosition = UnityObjectToClipPos(v.position);
                float4 clipPosition = UnityObjectToClipPos(tweakedObjectSpacePos);
                float3 clipNormal = mul((float3x3) UNITY_MATRIX_VP, mul((float3x3) UNITY_MATRIX_M, v.normal));
                // float3 clipNormal = mul((float3x3) UnityObjectToClipPos(position), mul((float3x3) UNITY_MATRIX_M, normal));

                clipPosition.xyz += normalize(clipNormal) * _OutlineWidth;

                // float2 offset = normalize(clipNormal.xy) * _OutlineWidth;
                float2 offset = normalize(clipNormal.xy) * _OutlineWidth * clipPosition.w;
                clipPosition.xy += offset;

                // return UnityObjectToClipPos(position);
                return clipPosition;

            }

            // 


            //
            //struct appdata {
            //    float4 position : POSITION;
            //    float3 normal : NORMAL;
            //    float4 color : COLOR;    // Vertex color for smoothed normals
            //};

            //float4 VertexProgram(appdata v) : SV_POSITION {
            //    float4 position = v.position;
            //    float3 normal = v.normal;
                
            //    // Use vertex color's rgb channels as the smoothed normal
            //    // If vertex color is black (0,0,0) use the geometric normal
            //    float3 smoothedNormal = lerp(normal, v.color.rgb * 2 - 1, v.color.a);
                
            //    float4 clipPosition = UnityObjectToClipPos(position);
            //    float3 clipNormal = mul((float3x3)UNITY_MATRIX_VP, mul((float3x3)UNITY_MATRIX_M, smoothedNormal));

            //    float2 offset = normalize(clipNormal.xy) * _OutlineWidth * clipPosition.w;
            //    clipPosition.xy += offset;

            //    return clipPosition;
            //}
            //



            half4 FragmentProgram() : SV_TARGET {
                return _OutlineColor;
            }

            ENDCG

        }
    }
}
