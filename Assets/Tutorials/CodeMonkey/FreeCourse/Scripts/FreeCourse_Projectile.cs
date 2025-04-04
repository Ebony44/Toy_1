using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCourse_Projectile : MonoBehaviour
{
    private Vector3 direction;
    private float speed = 10f;

    public void Setup(Vector3 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, 5f); // Clean up after 5 seconds
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
