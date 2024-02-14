using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;
using LlamAcademy;

namespace VFXLightningTargeting
{
    public class VFXTargetTesting : MonoBehaviour
    {
        // public List<Transform> targetTransform;

        public Transform srcTransform;
        public Transform dstTransform;

        public CircleCollider2D tempCollider;

        public Transform strokePoint;

        public float chainingRadius = 2;
        public float intervalTimeBetweenLightning = 0.3f;
        public LayerMask targetingLayerMask;

        public VisualEffect lightningFX;

        // when above test done, finish FX related works with list
        // private List<VisualEffect> lightningFXList = new List<VisualEffect>(16);

        public PoolableObject lightningFXPrefab;
        public ObjectPool lightningFXPools;
        

        // public VFXPropertyBinder positionInfo; // https://github.com/Unity-Technologies/Graphics/blob/master/Packages/com.unity.visualeffectgraph/Runtime/Utilities/PropertyBinding/Implementation/VFXMultiplePositionBinder.cs

        public MyMultiplePositionBinder myMultiplePositionBinder;



        // public readonly ExposedProperty testProp = "Targets";



        private void Awake()
        {
            lightningFXPools = new ObjectPool(lightningFXPrefab, 16);
        }
        private void Start()
        {
            myMultiplePositionBinder.Targets[0] = srcTransform.gameObject;
            myMultiplePositionBinder.Targets[1] = dstTransform.gameObject;

        }

        public void FireLightning()
        {
            // tempCollider.
        }

        [TestMethod(false)]
        public void startFindingRoutine(int iterationCount, float waitTime, bool bHitAgain)
        {
            StartCoroutine(FireLightningRoutine(iterationCount, waitTime, strokePoint.position,bHitAgain));
        }

        //[TestMethod(false)]
        //public void GetPositionBinding()
        //{
        //    Debug.Log("asdf");
        //    var temp = positionInfo.GetPropertyBinders<VFXBinderBase>();
        //    // var temp2 = positionInfo.GetParameterBinders<VFXBinderBase>();
        //    var temp2 = positionInfo.m_Bindings[0];
        //    // temp2

        //    var isValid = temp2.IsValid(lightningFX);
        //    // var temp3 = temp2.getpro

        //    VisualEffect tempFx = new VisualEffect();
            
        //}

        public IEnumerator FireLightningRoutine(int iterationCount, float waitTime, Vector3 endPoint, bool bHitAgain)
        {
            int currentIteration = 0;
            List<Collider> cachedColliders = new List<Collider>(iterationCount + 1);
            
            var tempColliders = GetAllColliderWithinArea(endPoint, chainingRadius, targetingLayerMask);

            Vector3 startPoint = strokePoint.position;

            while (iterationCount >= currentIteration)
            {
                // yield return new WaitForSeconds(waitTime);
                
                var selectedIndex = Random.Range(0, tempColliders.Count);
                var selectedCollider = tempColliders[selectedIndex];

                Debug.Log("[FindingRoutine], iteration is " + currentIteration
                    + " selected object name is " + tempColliders[selectedIndex].gameObject.name);
                cachedColliders.Add(selectedCollider);

                endPoint = selectedCollider.transform.position;

                // setting up fx start/end point
                srcTransform.position = startPoint;
                dstTransform.position = endPoint;
                Debug.Log("[FireLightningRoutine], start pos is " + startPoint);
                
                // lightningFX.Play();
                lightningFX.Reinit();

                yield return new WaitForSeconds(waitTime);
                lightningFX.Stop();
                //lightningFX.Reinit();

                startPoint = endPoint;
                tempColliders = GetAllColliderWithinArea(selectedCollider.gameObject.transform.position, chainingRadius, targetingLayerMask);
                
                if(bHitAgain == false)
                {
                    HelperFunctions.RemoveListFromList<Collider>(cachedColliders, tempColliders);
                }
                if(tempColliders.Count == 0)
                {
                    Debug.LogError("no more target to hit");
                    break;
                }
                currentIteration++;



            }
        }

        List<Collider> GetAllColliderWithinArea(Vector3 center, float radius)
        {
            
            return GetAllColliderWithinArea(center,radius,null);
        }
        List<Collider> GetAllColliderWithinArea(Vector3 center, float radius, LayerMask? includingLayer)
        {
            // var hitColliders = Physics.OverlapSphere(center, radius).ToList();
            List<Collider> hitColliders = null;
            if (includingLayer != null)
            {

                hitColliders = Physics.OverlapSphere(center, radius).Where(
                    (x) => (1 << x.gameObject.layer & includingLayer) != 0).ToList();
            }
            else
            {
                hitColliders = Physics.OverlapSphere(center, radius).ToList();
                hitColliders.Capacity = 16;
            }


            Debug.Log("objects in colliders' count is " + hitColliders.Count);
            var removeList = new List<Collider>(16);
            
            foreach (var hitCollider in hitColliders)
            {
                // hitCollider.SendMessage("AddDamage");
            }
            return hitColliders;
        }


    }

}

