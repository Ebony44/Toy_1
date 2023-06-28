using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoTweenTestManager : MonoBehaviour
{
    public GameObject targetObject;

    [TestMethod(false)]
    public void TestMove(float moveX, float moveY, float duration)
    {
        Vector3 currentPos = targetObject.transform.localPosition;
        Vector3 endPos = new Vector3(currentPos.x + moveX,
            currentPos.y + moveY,
            0f);
        targetObject.transform.DOLocalMove(endPos, duration, true);
    }
    
}
