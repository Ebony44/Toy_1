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
        public float bulletDistance = 2f;
        public float maxBulletSpreadAngle = 90f;
        public float bulletFireRate = 0.5f;

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

            // tempPos.y = tempDistance * Mathf.Cos(tempPos.y * Mathf.Deg2Rad);
            

            // sin is + in 1st and 2nd quadrant
            // cos is + in 1st and 4th quadrant

            tempEulerAngle.y += -maxBulletSpreadAngle / 2;
            for (int i = 0; i < bulletCount; i++)
            {
                
                tempPos.x = tempDistance * Mathf.Sin(tempEulerAngle.y * Mathf.Deg2Rad);
                tempPos.z = tempDistance * Mathf.Cos(tempEulerAngle.y * Mathf.Deg2Rad);
                var tempObject = Instantiate(bulletIndicator, tempPos, tempRot);
                tempObject.name = "Bullet_" + i;

                Debug.Log(" Bullet: " + i + "'s "
                    + " temp pos is " + tempPos
                    + " temp angle is " + tempEulerAngle.y);

                // tempEulerAngle.y += 360f / bulletCount;
                // at one of second, bullet is on middle
                var tempCount = bulletCount / 2;
                
                tempEulerAngle.y += maxBulletSpreadAngle / bulletCount;

                //if(i == bulletCount / 2 )
                //{
                //    tempDistance += bulletDistance;
                //}

            }

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

        public IEnumerator SpawnBulletRoutine(Vector3 srcPos, Vector3 dstPos, int bulletCount, int loopCount)
        {
            var paramPos = dstPos - srcPos;
            paramPos.y = 0f;
            paramPos.Normalize();
            var tempRot = Quaternion.LookRotation(dstPos - srcPos, Vector3.up);
            var tempRot2 = Quaternion.LookRotation(paramPos, Vector3.up);
            var tempDistance = Vector3.Distance(srcPos, dstPos);
            var tempPos = new Vector3(0, 0, 0);
            var tempEulerAngle = tempRot.eulerAngles;

            // if cone shape
            tempEulerAngle.y += -maxBulletSpreadAngle / 2;
            for (int i = 0; i < loopCount; i++)
            {
                for (int k = 0; k < bulletCount; k++)
                {
                    tempPos.x = tempDistance * Mathf.Sin(tempEulerAngle.y * Mathf.Deg2Rad);
                    tempPos.z = tempDistance * Mathf.Cos(tempEulerAngle.y * Mathf.Deg2Rad);
                    var tempObject = Instantiate(bulletIndicator, srcPos, tempRot);
                    tempObject.name = "Bullet_" + k;
                    BulletInfo tempComp;
                    bulletIndicator.TryGetComponent<BulletInfo>(out tempComp);
                    if(tempComp != null)
                    {
                        tempComp.progressDirection = tempPos;
                        tempComp.speed = 0.05f;
                    }
                    else
                    {
                        tempComp = tempObject.AddComponent<BulletInfo>();
                        tempComp.progressDirection = tempPos;
                        tempComp.speed = 0.05f;

                    }
                    

                    Debug.Log(" Bullet: " + k + "'s "
                        + " temp pos is " + tempPos
                        + " temp angle is " + tempEulerAngle.y);

                    tempEulerAngle.y += maxBulletSpreadAngle / bulletCount;
                    yield return new WaitForSeconds(bulletFireRate);

                }
            }

            //

            
            yield return null;

        }

        public IEnumerator UpdateEveryBulletTick()
        {
            yield return null;
        }

    }

}
