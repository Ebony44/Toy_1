using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DoTweenTestObject : MonoBehaviour
{
    public void TweenForThisObj()
    {
        this.transform.DOLocalMove(new Vector3(2, 3, 4), 1f);
        // DOTween.To(()=> this.transform, x=> )
    }
}
