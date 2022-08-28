using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    
    public GameObject bullet;

    public float shootTimer;

    public float timeBetweenBullets = 0.15f;


    #region  visible part
    Ray shootRay;
    RaycastHit shootHit;
    LineRenderer gunLine;

    public Transform bulletSpawnPos;

    #endregion


    public void Shoot(int numberOfBullets)
    {
        if(shootTimer >= timeBetweenBullets 
        && Time.timeScale != 0 ) // this should change something like boolean? from any manager..?
        {
            shootTimer = 0f; // reset it
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;

            // for (int i = 0; i < numberOfBullets; i++)
            // {
                
            // }
            GameObject bulletObject = Instantiate(bullet,bulletSpawnPos.position,Quaternion.identity);

        }
    }

}
