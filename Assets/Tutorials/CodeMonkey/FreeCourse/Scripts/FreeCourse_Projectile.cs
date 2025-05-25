using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FreeCourse_Projectile : MonoBehaviour
{
    private Vector3 direction;
    private float speed = 10f;
    private ObjectPool<FreeCourse_Projectile> mPool;
    private const string DISABLE_METHOD_NAME = "DestroySelf";
    public bool bIsReleased = false;

    public void Setup(Vector3 dir, ObjectPool<FreeCourse_Projectile> paramPool  )
    {
        mPool = paramPool;
        
        direction = dir.normalized;
        Invoke("DestroySelf", 5f); // Destroy after 5 seconds
        // Destroy(gameObject, 5f); // Clean up after 5 seconds
    }

    private void DestroySelf()
    {
        if (bIsReleased)
        {
            return;
        }
        if (mPool == null)
        {
            Debug.LogError("mPool is null");
            Destroy(gameObject);
            return;
        }
        mPool.Release(this);
        CancelInvoke("DestroySelf");
        // CancelInvoke("DestroySelf");
        // gameObject.SetActive(false);

    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
