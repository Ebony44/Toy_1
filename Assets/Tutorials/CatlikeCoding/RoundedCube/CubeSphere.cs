using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CatlikeCoding
{
    public class CubeSphere : MonoBehaviour
    {

        public int gridSize;
        // No more roundness.

        private Mesh mesh;
        private Vector3[] vertices;
        private Vector3[] normals;
        private Color32[] cubeUV;



        private void Generate()
        {
            GetComponent<MeshFilter>().mesh = mesh = new Mesh();
            mesh.name = "Procedural Sphere";
            //CreateVertices();
            //CreateTriangles();
            //CreateColliders();
        }

        private void CreateColliders()
        {
            gameObject.AddComponent<SphereCollider>();
        }
        
        public float radius = 1;

        private void SetVertex(int i, int x, int y, int z)
        {
            Vector3 v = new Vector3(x, y, z) * 2f / gridSize - Vector3.one;
            normals[i] = v.normalized;
            vertices[i] = normals[i];
            cubeUV[i] = new Color32((byte)x, (byte)y, (byte)z, 0);
        }


    }
}

