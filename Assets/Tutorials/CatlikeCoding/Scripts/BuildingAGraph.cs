using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatlikeCoding
{
    public class BuildingAGraph : MonoBehaviour
    {
        [SerializeField, Range(10, 100)]
        public int drawDistance = 10;

        public GameObject parentObject;

        private void Start()
        {
            DrawLine();
        }

        public void DrawLine()
        {
            // Vector3 lineDir = endPoint.transform.position - startPoint.transform.position;
            // y = mx + c
            // 
            // every draw distance, instantiate cube?
            // example, y = x + 1
            // first cube will be (1,0,0)
            // second cube will be (2,1,0)
            // .. so on?
            // if draw distance is 5, then 5 cubes will be instantiated
            // float scaleMod = 0.5f;
            
            float step = 2f / drawDistance;
            Vector3 tempPos = Vector3.zero;
            for (int i = 0; i < drawDistance; i++)
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                
                cube.transform.SetParent(parentObject.transform);

                tempPos.x = (i + 0.5f) * step - 1f;
                // tempPos.y = tempPos.x;
                tempPos.y = tempPos.x * tempPos.x;
                cube.transform.localPosition = tempPos;

                cube.transform.localScale = Vector3.one * step;
            }
        }

        public void CalculateEquation()
        {
            // slope is
            // rise / run
            // y2 - y1 / x2 - x1
        }



    }

}

