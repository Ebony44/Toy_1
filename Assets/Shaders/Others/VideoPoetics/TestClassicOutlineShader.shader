Shader "Tutorial/TestClassicOutline" {

    Properties {


        _MainTex ("Albedo (RGB)", 2D) = "white" {}

        _Color ("Color", Color) = (1, 1, 1, 1)
        _Glossiness ("Smoothness", Range(0, 1)) = 0.5
        _Metallic ("Metallic", Range(0, 1)) = 0

        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0, 0.1)) = 0.03

    }

    Subshader {

        Tags {
            "RenderType" = "Opaque"
        }
        LOD 200

        Pass
        {
            CGPROGRAM

        // #pragma surface surf Standard fullforwardshadows
        
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"

        #pragma target 3.0

        //Input {
        //    float4 color : COLOR
        //}

        sampler2D _MainTex;
        float4 _MainTex_ST;

        //struct Input {
        //    float4 mColor : COLOR;
        //    // float2 uv_MainTex : TEXCOORD0;
        //    float2 uv_MainTex;
        //};


        half4 _Color;
        half _Glossiness;
        half _Metallic;

        

        //void surf(Input IN, inout SurfaceOutputStandard o) 
        //{
        //    //o.Albedo = _Color.rgb * IN.mColor.rgb;
        //    //o.Smoothness = _Glossiness;
        //    //o.Metallic = _Metallic;
        //    //o.Alpha = _Color.a * IN.mColor.a;

        //    fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
        //    o.Albedo = c.rgb;
        //    // Metallic and smoothness come from slider variables
        //    o.Smoothness = _Glossiness;
        //    o.Metallic = _Metallic;
        //    o.Alpha = c.a;
        //}

        //

        struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };


        v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target {
                // Sample the texture and multiply by color
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                return col;
            }
        //


            ENDCG
        }
        

        
        Pass {

            // Outline pass
            Cull Front
            // Cull Off

            CGPROGRAM

            #pragma vertex VertexProgram
            #pragma fragment FragmentProgram

            half _OutlineWidth;
            half4 _OutlineColor;

            float4 VertexProgram(
                    float4 position : POSITION,
                    float3 normal : NORMAL) : SV_POSITION {

                // position.xyz += normal * _OutlineWidth;

                // float4 positonWithColor = 

                float4 clipPosition = UnityObjectToClipPos(position);
                float3 clipNormal = mul((float3x3) UNITY_MATRIX_VP, mul((float3x3) UNITY_MATRIX_M, normal));
                // float3 clipNormal = mul((float3x3) UnityObjectToClipPos(position), mul((float3x3) UNITY_MATRIX_M, normal));

                clipPosition.xyz += normalize(clipNormal) * _OutlineWidth;
    

                // float2 offset = normalize(clipNormal.xy) * _OutlineWidth;
                float2 offset = normalize(clipNormal.xy) * _OutlineWidth * clipPosition.w;
                clipPosition.xy += offset;

                // return UnityObjectToClipPos(position);
                return clipPosition;

            }



            

            half4 FragmentProgram() : SV_TARGET {
                return _OutlineColor;
            }

            ENDCG

        }
        
    }
    // FallBack "Diffuse"
    Fallback "Unlit/Texture"

}