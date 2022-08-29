using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// every projectile's velocity, position update here.
public class ProjectileManager : MonoBehaviour
{

    public static ProjectileManager Instance { get; private set; }
    private static ProjectileManager instance { get; set; }
    // List<IProjectile> projectiles = new List<IProjectile>(256);
    List<Bullet> bullets = new List<Bullet>(256);

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
        
    }

    public void MoveProjectile()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            var currentBulletSpeed = bullets[i].travelSpeed;
            var velocity = transform.forward;
            velocity.y = 0;
            velocity = velocity.normalized * currentBulletSpeed;

            var prevPosition = transform.position;
            bullets[i].transform.position = velocity * Time.deltaTime;
            var distance = (transform.position - prevPosition).magnitude;
            


        }
    }

    public static void AddToList(Bullet bullet)
    {
        
    }

}
