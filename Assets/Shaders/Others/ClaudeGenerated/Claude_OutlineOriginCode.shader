Shader "Custom/TestFailedEdgeDetectionOutline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Range(0, 10)) = 1
        _DepthSensitivity ("Depth Sensitivity", Range(0, 1)) = 0.1
        // _NormalsSensitivity ("Normals Sensitivity", Range(0, 1)) = 0.1
        _ColorSensitivity ("Color Sensitivity", Range(0, 1)) = 0.1
    }
    
    SubShader
    {
        Tags 
        { 
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline" 
        }
        
        // First pass to render the object normally
        Pass
        {
            Name "NORMAL"
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _OutlineColor;
                float _OutlineThickness;
                float _DepthSensitivity;
                // float _NormalsSensitivity;
                float _ColorSensitivity;
            CBUFFER_END
            
            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                return output;
            }
            
            half4 frag(Varyings input) : SV_Target
            {
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
                return col;
            }
            ENDHLSL
        }
        
        // Edge detection pass (requires the camera depth and normal textures)
        Pass
        {
            Name "OUTLINE"
            ZTest Always
            ZWrite Off
            Cull Off
            // Cull Front
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareNormalsTexture.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 viewSpaceDir : TEXCOORD1;
            };
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_CameraColorTexture);
            SAMPLER(sampler_CameraColorTexture);
            
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _OutlineColor;
                float _OutlineThickness;
                float _DepthSensitivity;
                float _NormalsSensitivity;
                float _ColorSensitivity;
            CBUFFER_END
            
            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                
                // Calculate view space direction (needed for normal-based edge detection)
                float4 clipPos = output.positionCS;
                float3 viewVector = mul(unity_CameraInvProjection, float4(clipPos.xy * 2 - 1, 0, -1)).xyz;
                output.viewSpaceDir = viewVector;
                
                return output;
            }
            
            half4 frag(Varyings input) : SV_Target
            {
                float2 screenUV = input.positionCS.xy / _ScreenParams.xy;
                
                // Calculate UV offsets for sampling neighboring pixels
                float2 pixelSize = 1.0 / _ScreenParams.xy;
                float2 offset1 = float2(pixelSize.x * _OutlineThickness, 0);
                float2 offset2 = float2(0, pixelSize.y * _OutlineThickness);
                
                // Sample depth at the current pixel and four neighbors
                float depth = SampleSceneDepth(screenUV);
                float depthLeft = SampleSceneDepth(screenUV - offset1);
                float depthRight = SampleSceneDepth(screenUV + offset1);
                float depthTop = SampleSceneDepth(screenUV - offset2);
                float depthBottom = SampleSceneDepth(screenUV + offset2);
                
                // Calculate depth differences
                float depthDiff1 = abs(depth - depthLeft);
                float depthDiff2 = abs(depth - depthRight);
                float depthDiff3 = abs(depth - depthTop);
                float depthDiff4 = abs(depth - depthBottom);
                
                // Determine if pixel is on an edge based on depth differences
                float depthEdge = max(max(depthDiff1, depthDiff2), max(depthDiff3, depthDiff4));
                depthEdge = depthEdge > _DepthSensitivity ? 1 : 0;
                
                // Sample normals
                float3 normal = SampleSceneNormals(screenUV);
                float3 normalLeft = SampleSceneNormals(screenUV - offset1);
                float3 normalRight = SampleSceneNormals(screenUV + offset1);
                float3 normalTop = SampleSceneNormals(screenUV - offset2);
                float3 normalBottom = SampleSceneNormals(screenUV + offset2);
                
                //// Calculate normal differences
                //float3 normalDiff1 = abs(normal - normalLeft);
                //float3 normalDiff2 = abs(normal - normalRight);
                //float3 normalDiff3 = abs(normal - normalTop);
                //float3 normalDiff4 = abs(normal - normalBottom);
                
                //// Determine if pixel is on an edge based on normal differences
                //float normalEdge = max(max(length(normalDiff1), length(normalDiff2)), 
                //                      max(length(normalDiff3), length(normalDiff4)));
                //normalEdge = normalEdge > _NormalsSensitivity ? 1 : 0;
                
                // Sample colors
                half4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, screenUV);
                half4 colorLeft = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, screenUV - offset1);
                half4 colorRight = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, screenUV + offset1);
                half4 colorTop = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, screenUV - offset2);
                half4 colorBottom = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, screenUV + offset2);
                
                // Calculate color differences
                half3 colorDiff1 = abs(color.rgb - colorLeft.rgb);
                half3 colorDiff2 = abs(color.rgb - colorRight.rgb);
                half3 colorDiff3 = abs(color.rgb - colorTop.rgb);
                half3 colorDiff4 = abs(color.rgb - colorBottom.rgb);
                
                // Determine if pixel is on an edge based on color differences
                float colorEdge = max(max(length(colorDiff1), length(colorDiff2)), 
                                     max(length(colorDiff3), length(colorDiff4)));
                colorEdge = colorEdge > _ColorSensitivity ? 1 : 0;
                
                // Combine all edge detection methods
                // float edge = max(max(depthEdge, normalEdge), colorEdge);
                float edge = max(depthEdge,colorEdge);
                
                // Blend the original color with the outline color based on edge detection
                half4 finalColor = lerp(color, _OutlineColor, edge);
                return finalColor;
            }
            ENDHLSL
        }
    }
}