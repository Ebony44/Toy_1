#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderVariablesFunctions.hlsl"
        struct Attributes
        {
        float3 positionOS : POSITION; // Position in object space
        float2 uv : TEXCOORD0;
        };

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
