using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTestBulletRepeater : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnBullet = Instantiate(prefab);
        spawnBullet.transform.position = transform.position;
    }

}
