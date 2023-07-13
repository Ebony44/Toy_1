using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace LlamAcademy
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Guns/Gun", order = 0)]
    public class GunScriptableObject : ScriptableObject
    {
        public ImpactType ImpactType;
        public GunType Type;
        public string Name;
        public GameObject ModelPrefab;
        public Vector3 SpawnPoint;
        public Vector3 SpawnRotation;

        public ShootConfigurationScriptableObject ShootConfig;
        public TrailConfigScriptableObject TrailConfig;

        private MonoBehaviour activeMonoBehaviour;
        private GameObject model;
        private float lastShootTime;
        private ParticleSystem shootSystem;

        private ObjectPool<TrailRenderer> trailPool;
        // private objectpool; // object pool script
        //private MonoBehaviour activeMonoBehaviour;

        public void Spawn(Transform parent, MonoBehaviour ActiveMonobehaviour)
        {
            this.activeMonoBehaviour = activeMonoBehaviour;
            lastShootTime = 0;
            trailPool = new ObjectPool<TrailRenderer>(CreateTrail);
            model = Instantiate(ModelPrefab);
            model.transform.SetParent(parent, false);
            model.transform.localPosition = SpawnPoint;
            model.transform.localRotation = Quaternion.Euler(SpawnRotation);

            shootSystem = model.GetComponentInChildren<ParticleSystem>();
        }

        public void Shoot()
        {
            if (Time.time > ShootConfig.FireRate + lastShootTime)
            {
                lastShootTime = Time.time;
                shootSystem.Play();

                Vector3 shootDirection = shootSystem.transform.forward
                    + new Vector3(
                        Random.Range(
                            -ShootConfig.Spread.x,
                            ShootConfig.Spread.x
                            ),
                        Random.Range(
                            -ShootConfig.Spread.y,
                            ShootConfig.Spread.y
                            ),
                        Random.Range(
                            -ShootConfig.Spread.z,
                            ShootConfig.Spread.z
                            )
                        );
                shootDirection.Normalize();

                if (Physics.Raycast(
                    shootSystem.transform.position,
                    shootDirection,
                    out RaycastHit hit,
                     float.MaxValue,
                     ShootConfig.HitMask
                    ))
                {
                    activeMonoBehaviour.StartCoroutine(
                        playTrail(
                            shootSystem.transform.position,
                            hit.point,
                            hit
                            )
                        );
                }
                else // hit missed
                {
                    activeMonoBehaviour.StartCoroutine(
                        playTrail(
                            shootSystem.transform.position,
                            shootSystem.transform.position + (shootDirection * TrailConfig.MissDistance),
                            new RaycastHit()
                            )
                        );
                }

            }
        }

        private IEnumerator playTrail(Vector3 StartPoint, Vector3 EndPoint, RaycastHit Hit)
        {
            TrailRenderer instance = trailPool.Get();
            instance.gameObject.SetActive(true);
            instance.transform.position = StartPoint;
            yield return null;

            instance.emitting = true;
            float distance = Vector3.Distance(StartPoint, EndPoint);
            float remainingDistance = distance;
            while (remainingDistance > 0)
            {
                instance.transform.position = Vector3.Lerp(
                    StartPoint,
                    EndPoint,
                    Mathf.Clamp01(1 - (remainingDistance / distance))
                    );

                remainingDistance -= TrailConfig.SimulationSpeed * Time.deltaTime;

                yield return null;
            }

            instance.transform.position = EndPoint;

            if (Hit.collider != null) // 
            {
                // hit impact handle
                // surfacemanager
            }

            yield return new WaitForSeconds(TrailConfig.Duration);
            yield return null;
            instance.emitting = false;
            instance.gameObject.SetActive(false); // remaining distance 0 or minus... need to be disappear
            trailPool.Release(instance);

            throw new System.NotImplementedException();
        }

        private TrailRenderer CreateTrail()
        {
            GameObject instnace = new GameObject("Bullet Trail");
            TrailRenderer trail = instnace.AddComponent<TrailRenderer>();
            trail.colorGradient = TrailConfig.Color;
            trail.material = TrailConfig.Material;
            trail.widthCurve = TrailConfig.WidthCurve;
            trail.time = TrailConfig.Duration;
            trail.minVertexDistance = TrailConfig.MinVertexDistance;

            trail.emitting = false;
            trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            return trail;

        }


    }
}

