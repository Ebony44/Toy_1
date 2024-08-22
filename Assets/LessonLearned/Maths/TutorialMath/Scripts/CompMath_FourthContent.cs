using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinearAlgebra
{
    public class CompMath_FourthContent : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public Transform youTrans;
        public Transform plainTrans;

        // Update is called once per frame
        void Update()
        {
            // CheckIntersectEach();

        }

        [TestMethod(false)]
        public void CheckIntersectEach()
        {
            //Math from http://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-plane-and-ray-disk-intersection
            // A plane can be defined as:
            // a point representing how far the plane is from the world origin

            Vector3 p_0 = plainTrans.position;
            // a normal (defining the orientation of the plane), should be negative if we are firing the ray from above
            // Vector3 n = -plainTrans.up;
            Vector3 n = plainTrans.up; // I will fire this up

            // We are intrerested in calculating a point in this plane called p
            // The vector between p and p0 and the normal is always perpendicular: (p - p_0) . n = 0

            // ((l_0 + l * t) - p_0) . n = 0
            // (l_0 + l* t) . n - p_0.n
            // l_0.n + (l * t).n - p_0.n = 0
            // (l*t).n = p_0.n - l_0.n
            // (l * t . n) = (p_0 - l_0) . n

            // t = ((p_0 - l_0) . n) / (l . n)

            // A ray to point p can be defined as: l_0 + l * t = p, where:
            // the origin of the ray is l_0
            Vector3 l_0 = youTrans.position;
            // l is the direction of the ray
            Vector3 l = youTrans.forward;
            // t is the length of the ray, which we can get by combining the above equations:
            // t = ((p_0 - l_0) . n) / (l . n)

            var temp = p_0 - l_0;
            var firstValue = Vector3.Dot(temp, n);
            var denominator = Vector3.Dot(l, n);

            Debug.Log("ray forward pos " + l
                + " plane up pos " + n);

            if (denominator > 1e-6)
            {
                // do something
                var t = firstValue / denominator;
                Debug.Log("first value is " + firstValue
                    + " denominator is " + denominator);

                // where the ray intersects with a plane
                Vector3 p = l_0 + l * t;

                // display the ray with a line renderer
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, l_0);
                lineRenderer.SetPosition(1, p);
                Debug.Log("Intersection point is " + p
                    + " start of ray point " + l_0);

            }
            else
            {
                // no intersection
                Debug.Log("No intersection");
            }


            // exception, if l.n == 0, the ray is parallel to the plane


        }

        [TestMethod(false)]
        public void CheckIntersectPlaneArea()
        {

        }


    }



}
