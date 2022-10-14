using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;

    public Vector3 DirFromAngle(float angleInDegrees)
    {
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    [SerializeField] LayerMask layerMask;
    private Mesh mesh;
    Vector3 origin;
    private float fov;
    private float startingAngle;

    [SerializeField] private Material matForRunTimeMesh;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        fov = 120f;
        origin = Vector3.zero;

        // Mesh mesh = new Mesh();
        // GetComponent<MeshFilter>().mesh = mesh;

        #region
        //Vector3[] vertices = new Vector3[3];
        //Vector2[] uv = new Vector2[3];
        //int[] triangles = new int[3];

        //vertices[0] = Vector3.zero;
        //vertices[1] = new Vector3(50, 0);
        //vertices[2] = new Vector3(0,-50);

        //triangles[0] = 0;
        //triangles[1] = 1;
        //triangles[2] = 2;

        //mesh.vertices = vertices;
        //mesh.uv = uv;
        //mesh.triangles = triangles;

        #endregion


    }

    private void Update()
    {
        // float fov = 120f;
        // Vector3 origin = Vector3.zero;
        int rayCount = 2;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        float viewDistance = 50f;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 tempDir = CodeMonkey.Utils.UtilsClass.GetVectorFromAngle((int)angle);// worth to see function...
            // Vector3 vertex = origin + tempDir * viewDistance; 
            Vector3 vertex;

            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, tempDir, viewDistance,layerMask);

            if (raycastHit2D.collider == null)
            {
                // hit nothing
                vertex = origin + tempDir * viewDistance;
            }
            else
            {
                // hit object..
                vertex = raycastHit2D.point;
            }


            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }


            angle -= angleIncrease;
        }

        // vertices[1] = new Vector3(50, 0);
        // vertices[2] = new Vector3(0, -50);
        // 
        // triangles[0] = 0;
        // triangles[1] = 1;
        // triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        // TODO: apply texture to mesh
        // like this

        // Renderer renderer = GetComponent<MeshRenderer>();
        // renderer.material = matForRunTimeMesh;

        // above code maybe doesn't work properly...

    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }
    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = CodeMonkey.Utils.UtilsClass.GetAngleFromVectorFloat(aimDirection) - fov / 2f;
    }
}
