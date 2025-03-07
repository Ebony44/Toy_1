Shader "Custom/EdgeDetection_Outline"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0, 10)) = 1
        _SensitivityDepth ("Depth Sensitivity", Range(0, 1)) = 0.1
        _SensitivityNormals ("Normal Sensitivity", Range(0, 1)) = 0.1
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
        // Second pass for edge detection
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float2 uv0 : TEXCOORD1;
                float2 uv1 : TEXCOORD2;
                float2 uv2 : TEXCOORD3;
                float2 uv3 : TEXCOORD4;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _CameraDepthTexture;
            sampler2D _CameraDepthNormalsTexture;
            fixed4 _OutlineColor;
            float _OutlineWidth;
            float _SensitivityDepth;
            float _SensitivityNormals;
            
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                // Calculate UV coordinates for neighboring pixels
                float2 texelSize = _OutlineWidth * float2(1.0 / _ScreenParams.x, 1.0 / _ScreenParams.y);
                o.uv0 = o.uv + float2(-texelSize.x, -texelSize.y);
                o.uv1 = o.uv + float2(texelSize.x, -texelSize.y);
                o.uv2 = o.uv + float2(-texelSize.x, texelSize.y);
                o.uv3 = o.uv + float2(texelSize.x, texelSize.y);
                
                return o;
            }
            
            // Function to compare normals and depth for edge detection
            float CheckSame(float4 center, float4 sample)
            {
                // Decode normal and depth
                float2 centerNormal = center.xy;
                float2 sampleNormal = sample.xy;
                float centerDepth = DecodeFloatRG(center.zw);
                float sampleDepth = DecodeFloatRG(sample.zw);
                
                // Compare normals
                float2 normalDiff = abs(centerNormal - sampleNormal);
                float normalCheck = step(_SensitivityNormals, max(normalDiff.x, normalDiff.y));
                
                // Compare depth
                float depthDiff = abs(centerDepth - sampleDepth);
                float depthCheck = step(_SensitivityDepth, depthDiff);
                
                return max(normalCheck, depthCheck);
            }
            
            fixed4 frag(v2f i) : SV_Target
            {
                // Sample depth and normals at current pixel
                float4 center = tex2D(_CameraDepthNormalsTexture, i.uv);
                
                // Sample depth and normals at neighboring pixels
                float4 sample0 = tex2D(_CameraDepthNormalsTexture, i.uv0);
                float4 sample1 = tex2D(_CameraDepthNormalsTexture, i.uv1);
                float4 sample2 = tex2D(_CameraDepthNormalsTexture, i.uv2);
                float4 sample3 = tex2D(_CameraDepthNormalsTexture, i.uv3);
                
                // Check for edges by comparing with neighbors
                float edge = 0;
                edge += CheckSame(center, sample0);
                edge += CheckSame(center, sample1);
                edge += CheckSame(center, sample2);
                edge += CheckSame(center, sample3);
                
                // Original color
                fixed4 col = tex2D(_MainTex, i.uv);
                
                // Apply outline if edge detected
                // col = lerp(col, _OutlineColor, min(edge, 1.0));
                fixed4 colorInfo = col * _OutlineColor;
                
                // return col;
                return colorInfo;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}