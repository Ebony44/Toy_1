using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LlamAcademy
{
    public class ObjectPool
    {
        private PoolableObject prefab;
        private List<PoolableObject> availableObjects;
        public int ExpandValue = 4;

        public Transform poolParent;

        public ObjectPool(PoolableObject prefab, int size)
        {
            this.prefab = prefab;
            availableObjects = new List<PoolableObject>(size);
        }

        public static ObjectPool CreateInstance(PoolableObject prefab, int size)
        {
            ObjectPool pool = new ObjectPool(prefab, size);
            GameObject poolObject = new GameObject(prefab.name + " Pool");

            pool.CreateObject(poolObject.transform, size);
            return pool;

            // this.availableObjects = availableObjects;
        }


        private void CreateObject(Transform parent, int size)
        {
            for (int i = 0; i < size; i++)
            {
                PoolableObject poolableObject = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent.transform);
                poolableObject.Parent = this;
                poolableObject.gameObject.SetActive(false);
            }
        }
        private PoolableObject GetObjectWhenPoolFilled()
        {
            PoolableObject poolableObject = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            poolableObject.Parent = this;
            poolableObject.gameObject.SetActive(true);
            
            if(poolParent != null)
            {
                poolableObject.transform.SetParent(poolParent);
            }
            return poolableObject;
        }

        public void ReturnObjectToPool(PoolableObject poolableObject)
        {
            if(availableObjects.Contains(poolableObject) == false)
            {
                availableObjects.Add(poolableObject);
            }
            poolableObject.gameObject.SetActive(false);
        }

        public PoolableObject GetObject()
        {
            if (availableObjects.Count > 0)
            {
                PoolableObject instance = availableObjects[0];
                availableObjects.RemoveAt(0);
                instance.gameObject.SetActive(true);
                return instance;
            }
            else
            {
                // 1. return null - if you do not want to auto expand the size of the pool
                // 2. expand the size of the pool
                if(prefab != null)
                {
                    Debug.LogError("[GetObject] pool size empty, expand value " + ExpandValue);
                    //CreateInstance(prefab, ExpandValue);

                    // var currentObject = GetObject();
                    var currentObject = GetObjectWhenPoolFilled();
                    return currentObject;
                }
                else
                {
                    Debug.LogError("[GetObject] prefab is null ");
                    return null;
                }
                // availableObjects.Add()
                // return currentObject;
            }
            
        }

        public T GetObject<T>()
        {
            var currentObject = GetObject();
            var result = currentObject.GetComponent<T>(); // this one somewhat expensive?
            return result;
        }
        
        
    }
}
