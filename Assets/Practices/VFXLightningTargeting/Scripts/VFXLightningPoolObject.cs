using LlamAcademy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class VFXLightningPoolObject : PoolableObject
{
    public Transform srcTransform;
    public Transform dstTransform;
    
    public VisualEffect lightningFX;

    public MyMultiplePositionBinder myMultiplePositionBinder;

    [Header("Adjustable variables")]
    public float chainingRadius = 2;
    public float intervalTimeBetweenLightning = 0.3f;
    public LayerMask targetingLayerMask;

    public float dissipateTime = 1f;



    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnDisable()
    {
        base.OnDisable();
        lightningFX.Stop();
        // Agent.enabled = false;
    }

}
