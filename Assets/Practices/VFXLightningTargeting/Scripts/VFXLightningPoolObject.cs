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
    private float currentPassedTime = 0f;



    private void OnEnable()
    {
        currentPassedTime = 0;
    }
    private void Update()
    {
        if(gameObject.activeInHierarchy)
        {
            currentPassedTime += Time.deltaTime;
        }
        if(currentPassedTime >= dissipateTime)
        {
            gameObject.SetActive(false);
        }
    }
    public override void OnDisable()
    {
        if(Parent != null)
        {
            base.OnDisable();
        }
        lightningFX.Stop();
        // Agent.enabled = false;
    }

}
