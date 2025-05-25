using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FreeCourse_ProjectileShooter : MonoBehaviour
{
    ObjectPool<FreeCourse_Projectile> projectilePool;
    [SerializeField] FreeCourse_Projectile projectilePrefab;
    [SerializeField] Transform shootPoint;
    [SerializeField] float shootForce = 10f;
    [SerializeField] float shootRate = 0.5f;
    private float shootTimer;

    List<FreeCourse_Projectile> bulletsForUpdating = new List<FreeCourse_Projectile>();

    private void Awake()
    {
        projectilePool = new ObjectPool<FreeCourse_Projectile>(
                       CreateProjectile,
                       OnGetProjectile,
                       OnReleaseProjectile,
                       OnDestroyProjectile,
                       collectionCheck: true);
    }

    private void OnDestroyProjectile(FreeCourse_Projectile projectile)
    {
        Debug.Log("OnDestroyProjectile");
        //projectile.gameObject.SetActive(false);
        //projectile.transform.SetParent(null);
        //projectile.transform.position = Vector3.zero;
        // throw new NotImplementedException();
    }

    private void OnReleaseProjectile(FreeCourse_Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectile.transform.SetParent(null);
        projectile.transform.position = Vector3.zero;
        // throw new NotImplementedException();
    }

    private void OnGetProjectile(FreeCourse_Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
        projectile.transform.SetParent(shootPoint);
        projectile.transform.position = shootPoint.position;
        projectile.transform.rotation = shootPoint.rotation;
        // projectile.Setup(shootPoint.forward * shootForce);
        // throw new NotImplementedException();
    }

    private FreeCourse_Projectile CreateProjectile()
    {
        FreeCourse_Projectile projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        projectile.transform.SetParent(shootPoint);
        projectile.gameObject.SetActive(false);
        return projectile;
        // throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // fire related
    // [SerializeField] private GameObject bulletPrefab;

    [TestMethod(false)]
    public void ShootRandomPos()
    {
        Vector3 shootPosition = shootPoint.position;
        Vector3 targetPosition = new Vector3(UnityEngine.Random.Range(-5f, 5f), 0, UnityEngine.Random.Range(-5f, 5f));
        Shoot(shootPosition, targetPosition);
    }

    private void Shoot(Vector3 shootPosition, Vector3 targetPosition)
    {
        // GameObject bullet = Instantiate(bulletPrefab, shootPosition, Quaternion.identity);
        // Vector3 shootDirection = (targetPosition - shootPosition).normalized;

        // bullet.GetComponent<FreeCourse_Projectile>().Setup(shootDirection, projectilePool);
        
        var tempBullet = projectilePool.Get();
        tempBullet.gameObject.name = "Bullet";
        tempBullet.transform.position = shootPosition;
        tempBullet.transform.rotation = Quaternion.identity;
        tempBullet.Setup((targetPosition - shootPosition).normalized, projectilePool);

    }

    public List<GameObject> targets = new List<GameObject>(8);
    private void CheckHit()
    {
        if(targets.Count == 0)
        {
            return;
        }
        // foreach (Target target in Target.targetsList)
        foreach (GameObject target in targets)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < 0.5f)
            {
                // target.Damage();
                Destroy(gameObject);
            }
        }
    }

    // fire related end
}
