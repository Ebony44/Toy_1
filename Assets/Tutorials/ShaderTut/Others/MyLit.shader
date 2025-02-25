Shader "Unlit/MyLit"
{
// links : https://www.youtube.com/watch?v=KVWsAL37NGw&ab_channel=NedMakesGames
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
    // allow for different behaviour and options for different pipelines and platforms
    // like HDRP, URP version each.
        // Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        Tags { "RenderPipeline"="UniversalPipeline" }

        LOD 100

        Pass
        {
        Name "ForwardLit" // For debugging
        Tags { "LightMode" = "UniversalForward" } // pass specific Tags
        // "UniversalForward" tells Unity, this is the main lighting pass of this shader

        HLSLPROGRAM // begin HLSL code

        // Input assembler
        // essentially the vertex shader
        // each vertex's position, normal and uv0
        // as POSITION and TEXCOORD0
        // #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        
        // Register programmable stage functions
        #pragma vertex Vertex 
        #pragma fragment Fragment
        // Vertex and Fragment
        // function names must match inside of "MyLitForwardLitPass.hlsl"

        
        // Include code file
        #include "MyLitForwardLitPass.hlsl"

        ENDHLSL

            //CGPROGRAM
            //#pragma vertex vert
            //#pragma fragment frag
            //// make fog work
            //#pragma multi_compile_fog

            //#include "UnityCG.cginc"

            //struct appdata
            //{
            //    float4 vertex : POSITION;
            //    float2 uv : TEXCOORD0;
            //};

            //struct v2f
            //{
            //    float2 uv : TEXCOORD0;
            //    UNITY_FOG_COORDS(1)
            //    float4 vertex : SV_POSITION;
            //};

            //sampler2D _MainTex;
            //float4 _MainTex_ST;

            //v2f vert (appdata v)
            //{
            //    v2f o;
            //    o.vertex = UnityObjectToClipPos(v.vertex);
            //    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            //    UNITY_TRANSFER_FOG(o,o.vertex);
            //    return o;
            //}

            //fixed4 frag (v2f i) : SV_Target
            //{
            //    // sample the texture
            //    fixed4 col = tex2D(_MainTex, i.uv);
            //    // apply fog
            //    UNITY_APPLY_FOG(i.fogCoord, col);
            //    return col;
            //}
            //ENDCG
        }
    }
}
