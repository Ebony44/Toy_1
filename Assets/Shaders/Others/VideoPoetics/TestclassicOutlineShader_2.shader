Shader "Custom/EdgeDetectionOutline_NoNormals"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineThickness ("Outline Thickness", Range(0, 10)) = 1
        _DepthSensitivity ("Depth Sensitivity", Range(0, 1)) = 0.1
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
                float3 positionWS : TEXCOORD1;
            };
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _OutlineColor;
                float _OutlineThickness;
                float _DepthSensitivity;
                float _ColorSensitivity;
            CBUFFER_END
            
            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionWS = TransformObjectToWorld(input.positionOS.xyz);
                output.positionCS = TransformWorldToHClip(output.positionWS);
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
        
        // Edge detection pass using only depth and color
        Pass
        {
            Name "OUTLINE"
            
            // These settings will make sure the outline renders on top
            ZTest Always
            ZWrite Off
            Cull Off
            Blend SrcAlpha OneMinusSrcAlpha
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 screenUV : TEXCOORD1;
            };
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float4 _OutlineColor;
                float _OutlineThickness;
                float _DepthSensitivity;
                float _ColorSensitivity;
            CBUFFER_END
            
            Varyings vert(Attributes input)
            {
                Varyings output;
                // Get the vertex position in clip space
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);
                
                // Calculate screen UV for sampling screen textures
                output.screenUV = output.positionCS.xy / output.positionCS.w;
                output.screenUV = output.screenUV * 0.5 + 0.5;
                
                #if UNITY_UV_STARTS_AT_TOP
                output.screenUV.y = 1 - output.screenUV.y;
                #endif
                
                return output;
            }
            
            half4 frag(Varyings input) : SV_Target
            {
                // Calculate UV offsets for sampling neighboring pixels
                float2 pixelSize = 1.0 / _ScreenParams.xy;
                float2 offset1 = float2(pixelSize.x * _OutlineThickness, 0);
                float2 offset2 = float2(0, pixelSize.y * _OutlineThickness);
                
                // Sample depth at current pixel and neighbors
                float depth = SampleSceneDepth(input.screenUV);
                float depthLeft = SampleSceneDepth(input.screenUV - offset1);
                float depthRight = SampleSceneDepth(input.screenUV + offset1);
                float depthTop = SampleSceneDepth(input.screenUV - offset2);
                float depthBottom = SampleSceneDepth(input.screenUV + offset2);
                
                // Calculate depth differences
                float depthDiff1 = abs(depth - depthLeft);
                float depthDiff2 = abs(depth - depthRight);
                float depthDiff3 = abs(depth - depthTop);
                float depthDiff4 = abs(depth - depthBottom);
                
                // Determine if pixel is on an edge based on depth differences
                float depthEdge = max(max(depthDiff1, depthDiff2), max(depthDiff3, depthDiff4));
                depthEdge = depthEdge > _DepthSensitivity ? 1 : 0;
                
                // Sample colors
                half4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
                half4 colorLeft = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv - offset1 * 10);
                half4 colorRight = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv + offset1 * 10);
                half4 colorTop = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv - offset2 * 10);
                half4 colorBottom = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv + offset2 * 10);
                
                // Calculate color differences
                half3 colorDiff1 = abs(color.rgb - colorLeft.rgb);
                half3 colorDiff2 = abs(color.rgb - colorRight.rgb);
                half3 colorDiff3 = abs(color.rgb - colorTop.rgb);
                half3 colorDiff4 = abs(color.rgb - colorBottom.rgb);
                
                // Determine if pixel is on an edge based on color differences
                float colorEdge = max(max(length(colorDiff1), length(colorDiff2)), 
                                     max(length(colorDiff3), length(colorDiff4)));
                colorEdge = colorEdge > _ColorSensitivity ? 1 : 0;
                
                // Combine edge detection methods (depth and color only)
                float edge = max(depthEdge, colorEdge);
                
                // Apply the outline
                half4 mainColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
                half4 finalColor = lerp(mainColor, _OutlineColor, edge * _OutlineColor.a);
                
                return finalColor;
            }
            ENDHLSL
        }
    }
    
    // Fallback for older rendering pipelines
    FallBack "Hidden/Universal Render Pipeline/FallbackError"
}