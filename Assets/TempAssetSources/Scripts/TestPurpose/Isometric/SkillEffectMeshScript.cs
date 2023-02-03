using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectMeshScript : MonoBehaviour
{
    Mesh currentMesh;

    private void Initialize()
    {
        currentMesh = GetComponent<MeshFilter>().mesh;
    }

    // TODO: mesh shape that circle
    private void CircleAroundMesh()
    {
        Vector3[] vertices;
        Vector3[] normals;
        // Vector3[] vertices;

        // currentMesh.
    }

}
