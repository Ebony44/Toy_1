using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using Unity.Mathematics;
using UnityEngine;

namespace LinearAlgebra
{
    public class CompMath_FirstContent : MonoBehaviour
    {
        public Transform youTrans;
        public Transform enemyTrans;


        // TODO: check enemy trans is behind or front...
        private void Update()
        {
            
        }

        [TestMethod(false)]
        private void TestDot()
        {
            Debug.Log(" TestDot, result is  " + DotProductForTelling(youTrans.position, enemyTrans.position));
            
        }

        [TestMethod(false)]
        private void ShowPos()
        {
            Vector3 yourForward = youTrans.forward;
            Vector3 yourPos = youTrans.position;
            Vector3 yoursToEnemyPos = enemyTrans.position - youTrans.position;
            //Vector3 yourForwardModified = new Vector3(youTrans.position.x, youTrans.position.y, youTrans.position.z * yourForward.z);
            Debug.Log("[ShowPos], forward " + yourForward
                + " pos is " + yourPos
                // + " modified your pos " + yourForwardModified
                + " enemy pos " + enemyTrans.position
                + " pos toward yours and enemy's" + yoursToEnemyPos);

            Debug.Log("distance between yours and enemy's " + Vector3.Distance(youTrans.position, enemyTrans.position));
            


            Debug.Log(" TestDot, result is  " + DotProductForTelling(yourForward, enemyTrans.position));

            var result = DotProductForTelling(yourForward, yoursToEnemyPos);
            Debug.Log("Telling result " + result);
            if (result > 0)
            {
                // angle between the vectors is acute(90-, less than 90)
                Debug.Log("forward");
            }
            else if (result == 0)
            {
                
                Debug.Log("perpendicular");
            }
            else
            {
                // angle between the vectors is obtuse(90+, more than 90)
                Debug.Log("behind");
            }

        }

        [TestMethod(false)]
        private void TestFindOutAngle()
        {
            //Vector3 yourPos = youTrans.position;
            //Vector3 yoursToEnemyPos = enemyTrans.position - youTrans.position;
            Debug.Log("[TestFindOutAngle], you position is " + youTrans.position
                + " enemy position is " + enemyTrans.position);

            var temp = FindOutAngleBetweenTwoVector(youTrans.position, enemyTrans.position);

        }

        
        
        private float FindOutAngleBetweenTwoVector(Vector3 firstPoint, Vector3 secondPoint)
        {
            // formula is 
            // A . B = |A| * |B| * cos(theta)
            // cos(theta) = A . B / |A| * |B|

            // example is 
            // A = (1, 2, 3)
            // B = (4, 5, 6)
            // A . B = 1*4 + 2*5 + 3*6 = 4 + 10 + 18 = 32
            // |A| = sqrt(1*1 + 2*2 + 3*3) = sqrt(1 + 4 + 9) = sqrt(14)
            // |B| = sqrt(4*4 + 5*5 + 6*6) = sqrt(16 + 25 + 36) = sqrt(77)
            // cos(theta) = 32 / sqrt(14) * sqrt(77)
            // theta = acos(cos(theta))
            // degree is approx 41.8103 degree
            // 32 / sqrt(14) * sqrt(77) = 32 / sqrt(1078) = 32 / 32.8 = 0.978
            // acos(0.978) = 0.41


            var result = 0;
            var firstValue = math.sqrt(firstPoint.x * firstPoint.x + firstPoint.y * firstPoint.y + firstPoint.z * firstPoint.z);
            var secondValue = math.sqrt(secondPoint.x * secondPoint.x + secondPoint.y * secondPoint.y + secondPoint.z * secondPoint.z);

            var dotProduct = DotProductForTelling(firstPoint, secondPoint);

            var currentCosTheta = dotProduct / (firstValue * secondValue);
            var currentTheta = math.acos(currentCosTheta);
            Debug.Log("current first value " + firstValue
                + " current second value " + secondValue
                + " current dot product " + dotProduct
                + " current cosTheta " + currentCosTheta
                + " currentTheta " + currentTheta);
            var temp = Mathf.Rad2Deg * currentTheta;
            Debug.Log("current degree " + temp);
            //Debug.Log(" current first "
            //    + "currentTheta is " + currentTheta
            //    + " current cosTheta is " + currentCosTheta);
            return currentTheta;
            
        }

        private float DotProductForTelling(Vector3 src, Vector3 dst)
        {
            var tempX = src.x * dst.x;
            var tempY = src.y * dst.y;
            var tempZ = src.z * dst.z;

            return tempX + tempY + tempZ;

        }

        [TestMethod(false)]
        private void FindOutLeftOrRightWithCrossProduct(Transform target, Transform origin)
        {
            target = enemyTrans;
            origin = youTrans;
            var direction = target.position - origin.position;

            var temp = Vector3.right;

            var rightVector = Quaternion.Euler(0, 90, 0) * origin.forward;
            var crossValue = Vector3.Cross(rightVector, direction);

            if(crossValue.y > 0)
            {
                Debug.Log("right");
            }
            else if(crossValue.y == 0)
            {
                Debug.Log("straight");
            }
            else
            {
                Debug.Log("left");
            }
            Debug.Log("direction " + direction + " right vector " + rightVector + " cross value " + crossValue);

            // var rightVector = Vector3.Cross(direction, Vector3.up);
        }


    }

}

