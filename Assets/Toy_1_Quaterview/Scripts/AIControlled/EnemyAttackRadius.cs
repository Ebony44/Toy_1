using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(SphereCollider))]
public class EnemyAttackRadius : MonoBehaviour
{

    public SphereCollider Collider; // use as radius
    protected List<IDamageable> Damageables = new List<IDamageable>();
    public float AttackDelay = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
