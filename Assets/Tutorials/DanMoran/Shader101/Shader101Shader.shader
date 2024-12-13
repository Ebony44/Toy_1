// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// https://www.youtube.com/watch?app=desktop&v=T-HXmQAMhG0&t=1s&ab_channel=DanMoran

// 0,1 1,1
// 0,0 1,0

Shader "Custom/Shader101Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SecondTex("Second Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Tween("Tween", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }
        Pass
        {
            // how each pixel will blend with?
            Blend srcAlpha OneMinusSrcAlpha
            // srcColor * srcAlpha + destColor * (1 - srcAlpha)

            //
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
				float4 vertex : POSITION;
				float2 uv: TEXCOORD0;
			};
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv: TEXCOORD0;
            };
            v2f vert(appdata v)
			{
				v2f o;
				// o.vertex = UnityObjectToClipPos(v.vertex);
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv;
				return o;
			};

            sampler2D _MainTex;

            float4 frag(v2f i) : SV_Target
			{
                // v2f i, potential pixels which are passed by 
				// return float4(1,1,1,1);
                // return float4(1,0.6,0,1);

                // return float4(i.uv.r, 
                //     i.uv.b,
                //     i.uv.g,
                //     i.uv.a);

                // return float4(i.uv.r, 
                //     i.uv.g,
                //     0,
                //     1);

            //     half4 c = frac( i.uv );
            // if (any(saturate(i.uv) - i.uv))
            // {
            //     c.b = 0.5;
            //     }

            // return c;

                // return float4((i.uv.x - 0.25), 
                // i.uv.y - 0.5,
                // i.uv.y + i.uv.x,
                // 1);

                // return float4((i.uv.x), 
                // i.uv.y,
                // (1 - i.uv.y / 4 + i.uv.x),
                // 1);

                // float4 color = float4((i.uv.x), i.uv.y,1,1);
                float4 color = tex2D(_MainTex,i.uv);
                return color;


                // i, potential pixel will be turned into color, in this case, white
                // return float4(1,1,1,1);//  red, green,blue alpha, 3 colors of light
			};

            // #pragma target 3.0
            ENDCG

            }
        }
    /*
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
    */
}
