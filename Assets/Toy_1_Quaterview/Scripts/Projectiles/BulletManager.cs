using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.PlayerSettings;


namespace Toy_1
{
    public class BulletManager : MonoBehaviour
    {
        ObjectPool<GameObject> bulletPool;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Transform srcTrans;
        public Transform dstTrans;
        public GameObject bulletIndicator;
        public float bulletDistance = 20f;

        [TestMethod(false)]
        public void TestSpawnBullet(int bulletCount)
        {
            SpawnBullet(srcTrans.position, dstTrans.position, bulletCount);
        }

        public void SpawnBullet(Vector3 srcPos, Vector3 dstPos, int bulletCount)
        {
            var paramPos = dstPos - srcPos;
            paramPos.y = 0f;
            paramPos.Normalize();
            var tempRot = Quaternion.LookRotation(dstPos - srcPos, Vector3.up);
            var tempRot2 = Quaternion.LookRotation(paramPos, Vector3.up);
            var tempDistance = Vector3.Distance(srcPos, dstPos);
            var tempPos = new Vector3(0, 0, 0);

            // tempRot.y
            var tempEulerAngle = tempRot.eulerAngles; 
            
            // tempRot.z += bulletDistance;

            tempPos.x = tempDistance * Mathf.Sin(tempEulerAngle.y * Mathf.Deg2Rad);
            // tempPos.y = tempDistance * Mathf.Cos(tempPos.y * Mathf.Deg2Rad);
            tempPos.z = tempDistance * Mathf.Cos(tempEulerAngle.y * Mathf.Deg2Rad);

            // sin is + in 1st and 2nd quadrant
            // cos is + in 1st and 4th quadrant

            for (int i = 0; i < bulletCount; i++)
            {
                
            }

            var tempObject = Instantiate(bulletIndicator, tempPos, tempRot);

            Debug.Log("tempPos: " + tempPos
                + " tempRot: " + tempRot
                + " src pos: " + srcPos
                + " dst pos: " + dstPos
                + " tempRot2: " + tempRot2
                + " paramPos: " + paramPos
                + " tempDist: " + tempDistance
                + " parampos mag: " + paramPos.magnitude);


            Debug.Log("rot x is " + tempRot.x
                + " rot y is " + tempRot.y
                + " rot z is " + tempRot.z);


            // var tempRot = Mathf.Cos()
        }

    }

}
