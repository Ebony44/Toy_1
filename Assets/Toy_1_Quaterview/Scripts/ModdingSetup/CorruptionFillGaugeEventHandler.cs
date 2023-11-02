using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CorruptionFillGaugeEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject[] setActiveForEnterExitObjs;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("[OnPointerEnter]");
        foreach (var item in setActiveForEnterExitObjs)
        {
            item.SetActive(true);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("[OnPointerExit]");
        foreach (var item in setActiveForEnterExitObjs)
        {
            item.SetActive(false);
        }

    }

}
