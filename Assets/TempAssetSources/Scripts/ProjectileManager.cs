using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// every projectile's velocity, position update here.
public class ProjectileManager : MonoBehaviour
{
    List<IProjectile> projectiles = new List<IProjectile>(256);

    // Update is called once per frame
    void Update()
    {
        
    }
}
