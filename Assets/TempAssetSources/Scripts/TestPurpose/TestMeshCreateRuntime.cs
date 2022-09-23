using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class TestMeshCreateRuntime : MonoBehaviour
{
    Material quadMat;

    private void Start()
    {
        SingleSidedQuad();
    }

    private void SingleSidedQuad()
    {
        ProBuilderMesh quad = ProBuilderMesh.Create
            (
            new Vector3[]
            {
                new Vector3(0f,0f,0f ),
                new Vector3(1f,0f,0f ),
                new Vector3(0f,1f,0f ),
                new Vector3(1f,1f,0f ),

            },
            new Face[]
            {
                new Face(new int[] { 0,2,1}),
                new Face(new int[] { 2,3,1}),
            }
            );
        quad.SetMaterial(quad.faces, quadMat);
        quad.Refresh();
        quad.ToMesh();
        throw new NotImplementedException();
    }
}
