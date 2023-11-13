using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public GameObject prefab;
    public float damage;
    public float projectileSpeed;
    public float baseCooldownTime;
    public float currentCooldown;
    public int punchThorough;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = baseCooldownTime;
    }
}
