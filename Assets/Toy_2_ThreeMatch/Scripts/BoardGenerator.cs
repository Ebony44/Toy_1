using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toy2_ThreeMatch
{
    
    public class BoardGenerator : MonoBehaviour
    {

        public GameObject squareObj;
        public SpriteRenderer boardSprite;

        [TestMethod(false)]
        public void GenerateBoardWithGameobject(int rowCount = 2)
        {
            var width = Camera.main.aspect * 2f * Camera.main.orthographicSize;
            var height = 2f * Camera.main.orthographicSize;
            width = boardSprite.size.x;
            height = boardSprite.size.y;
            if(rowCount == 0)
            {
                rowCount = 1;

            }
            var cellXSize = width / rowCount;
            // var cellYSize = height / 

            var tempCount = rowCount;
            
            // var startPosX = -(int)width/2;
            // var startPosY = -(int)height/2;
            var startPosX = -width/2f + 0.5f;
            var startPosY = -height/2f + 0.5f;
            Debug.Log("width is " + width
            + " height is " + height
            + " aspect is " + Camera.main.aspect
            + " cell x is " + cellXSize
            + " temp count is " + tempCount
            + "start pos X " + startPosX
            + "start pos X " + startPosY
            );
            // even = 0.5f
            // odd = 1f
            
            var boardZ = boardSprite.transform.localPosition.z;
            
            for (int i = 0; i < tempCount; i++)
            {
                var xMod = i * cellXSize;
                var objPos = new Vector3(startPosX + i,
                startPosY, // + k when k for loop maded(row and column)
                boardZ);
                var go = Instantiate(squareObj);
                go.transform.SetParent(boardSprite.transform);
                go.transform.localPosition = objPos;
                // go.transform.localScale = 
            }
            // var temp = Instantiate(squareObj);

        }


        public void GenerateBoardWithMesh(int size)
        {
            Vector3[] vertices = new Vector3[0];
            Vector2[] uv  = new Vector2[0];
            int[] triangles  = new int[0];

            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            var tempSize = Camera.main.pixelWidth;
            GameObject gameObject = new GameObject("Mesh",typeof(MeshFilter),typeof(MeshRenderer));
            gameObject.transform.localScale = new Vector3(30,30,1);



        }
    }
}


