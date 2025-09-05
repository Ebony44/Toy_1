using DG.Tweening;
using UnityEngine;

public class DoTweenManager : MonoBehaviour
{
    public Transform movingObjectTrans;
    public Transform targetTrans;

    public float movingSpeed = 4f;
    public float distantModifier = 2f;

    private void Start()
    {
        DOTween.Init();
        MoveTo();
    }

    public void MoveTo()
    {
        var distantPosition = targetTrans.position - movingObjectTrans.position;
        Debug.Log($"Distant Position: {distantPosition}");
        distantPosition = targetTrans.position - distantPosition.normalized * distantModifier;
        Debug.Log($"Distant Position: {distantPosition}, target position: {targetTrans.position}, mover position: {movingObjectTrans.position}");
        movingObjectTrans.DOMove(distantPosition, movingSpeed)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() => Debug.Log("Movement Complete!"));

        //movingObjectTrans.DOMove(targetTrans.position, 1f)
        //    .SetEase(Ease.InOutQuad)
        //    .OnComplete(() => Debug.Log("Movement Complete!"));
    }

}
