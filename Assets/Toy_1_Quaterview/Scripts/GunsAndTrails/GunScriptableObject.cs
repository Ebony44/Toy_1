using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.pool;


[CreateAssetMenu(fileName ="Gun", menuName ="Guns/Gun", order = 0)]
public class GunScriptableObject : ScriptableObject
{
    public GunType Type;
    public string Name;
    public GameObject ModelPrefab;
    public Vector3 SpawnPoint;
    public Vector3 SpawnRotation;

    public ShootConfigurationScriptableObject shootConfig;
    public TrailConfigScriptableObject TrailConfig;

    private MonoBehaviour activeMonoBehaviour;
    private GameObject model;
    private float lastShootTime;
    private ParticleSystem shootSystem;
    // private objectpool; // object pool script
    //private MonoBehaviour activeMonoBehaviour;





}
