#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderVariablesFunctions.hlsl"
        struct Attributes
        {
        float3 positionOS : POSITION; // Position in object space
        float2 uv : TEXCOORD0;
        };

        float4 _ColorTint;
        // float4 _ColorMap; -> this variable is removed, below variable(?) will be used
        TEXTURE2D(_ColorMap); SAMPLER(sampler_ColorMap);
        //
        // sampler_ColorMap -> sampler_xxx
        // would match TEXTURE2D(xxx);
        float4 _ColorMap_ST; // automatically set by unity

        // from Core.hlsl
        //struct VertexPositionInputs
        //{
        //    float3 positionWS; // World space position
        //    float3 positionVS; // View space position
        //    float4 positionCS; // Homogeneous clip space position
        //    float4 positionNDC;// Homogeneous normalized device coordinates
        //};
        // from ShaderVariablesFunctions.hlsl
        //VertexPositionInputs GetVertexPositionInputs(float3 positionOS)
        //{
        //    // from position object space
        //    VertexPositionInputs input;
        //    input.positionWS = TransformObjectToWorld(positionOS);
        //    input.positionVS = TransformWorldToView(input.positionWS);
        //    input.positionCS = TransformWorldToHClip(input.positionWS);
        
        //    float4 ndc = input.positionCS * 0.5f;
        //    input.positionNDC.xy = float2(ndc.x, ndc.y * _ProjectionParams.x) + ndc.w;
        //    input.positionNDC.zw = input.positionCS.zw;
        
        //    return input;
        //}

        struct Interpolators
        {
        // this value should contain the position in clip space (which is similar to a position on screen)
        // when output from the vertex function. It will be transformed into pixel position of the current
        // fragment on the screen when read from the fragment function

            float4 positionCS : SV_POSITION;

            float2 uv : TEXCOORD0;
        };

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

        output.positionCS = positionCS;
        output.uv = TRANSFORM_TEX(input.uv, _ColorMap);
        // compute it once per vertex instead of once per pixel

        // generally vertex function runs fewer times than fragment function


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


            float2 uv = input.uv;

            // SAMPLE_TEXTURE2D takes 3 arguments, the texture, the sampler, and the uv coordinates
            float4 colorSample = SAMPLE_TEXTURE2D(_ColorMap,sampler_ColorMap, uv);

            // can't grab uv out of the fragment stage...
            // so make it variable to use it

            // return float4(1, 1, 1, 1); // white
            // return _ColorTint;
            return colorSample * _ColorTint;

        }

        // etc
        // what is clip space?
        // clip space is the space where the GPU decides which vertices are visible
