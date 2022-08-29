using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour, IProjectile
{
    #region appearance variables
    public MeshRenderer bulletMesh;
    #endregion

    #region ingame needed variables
    public float disappearTime;

    public float travelSpeed;
    public float damageType;
    public float damageAmout;

    public int pierceCount;
    public int bounceCount;

    public float pierceDistance;
    public float bounceDistance;

    #endregion


    #region position related variables
    public Vector3 direction;
    #endregion



    public void DamageCalc()
    {
        throw new System.NotImplementedException();
    }

    public void OnPop()
    {
        throw new System.NotImplementedException();
    }
}
