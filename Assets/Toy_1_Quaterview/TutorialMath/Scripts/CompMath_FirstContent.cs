using System.Collections;
using System.Collections.Generic;
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
            if (result > 0)
            {
                Debug.Log("forward");
            }
            else if (result == 0)
            {
                Debug.Log("perpendicular");
            }
            else
            {
                Debug.Log("behind");
            }

        }
        
        private float DotProductForTelling(Vector3 src, Vector3 dst)
        {
            var tempX = src.x * dst.x;
            var tempY = src.y * dst.y;
            var tempZ = src.z * dst.z;

            return tempX + tempY + tempZ;

        }


    }

}

