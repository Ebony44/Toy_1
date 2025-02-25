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
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        // Tags { "RenderPipeline"="UniversalPipeline" }

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
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "MyLitForwardLitPass.hlsl"
        #pragma vertex Vertex
        #pragma fragment Fragment

        struct Attributes
        {
        float3 position : POSITION; // Position in object space
        float2 uv : TEXCOORD0;
        }

        // from Core.hlsl
        struct VertexPositionInputs
        {
            float3 positionWS; // World space position
            float3 positionVS; // View space position
            float4 positionCS; // Homogeneous clip space position
            float4 positionNDC;// Homogeneous normalized device coordinates
        };
        // from ShaderVariablesFunctions.hlsl
        VertexPositionInputs GetVertexPositionInputs(float3 positionOS)
        {
            // from position object space
            VertexPositionInputs input;
            input.positionWS = TransformObjectToWorld(positionOS);
            input.positionVS = TransformWorldToView(input.positionWS);
            input.positionCS = TransformWorldToHClip(input.positionWS);
        
            float4 ndc = input.positionCS * 0.5f;
            input.positionNDC.xy = float2(ndc.x, ndc.y * _ProjectionParams.x) + ndc.w;
            input.positionNDC.zw = input.positionCS.zw;
        
            return input;
        }

        struct Interpolators
        {
        // this value should contain the position in clip space (which is similar to a position on screen)
        // when output from the vertex function. It will be transformed into pixel position of the current
        // fragment on the screen when read from the fragment function


            float4 positionCS : SV_POSITION;
        }

        Interpolators Vertex(Attributes input)
		{
        // the render pipeline will call this method multiple times
        // calls will run in parallel on the GPU
        // each this function call is effectively isolated from all the others

        // URP provides a nice function to convert
        // object space position to clip space and world space
        // -> GetVertexPositionInputs(float3)

        Interpolators output;
        
        // pass position and orientation data to the frament function
        VertexPositionInputs positionInputs = GetVertexPositionInputs(input.positionOS); // parameter is object space
        // float4 positionCS = float4(input.position, 1.0);
        float4 positionCS = positionInputs.positionCS;

        output = positionCS;

        return output;
        
        }

        // takes a struct as an argument which contains data output from vertex function
        // clip space would be the position of the pixel on the screen modified by the rasterizer...
        // clip space -> pixel position
        // I can also pass other data from the vertex function to the fragment function
        float4 Fragment(Interpolators input) : SV_TARGET
		{
            
            // This runs once per fragment, as a pixel on the screen
            // It must output the final color of this pixel

            return float4(1, 1, 1, 1); // white

        }

        // etc
        // what is clip space?
        // clip space is the space where the GPU decides which vertices are visible

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
