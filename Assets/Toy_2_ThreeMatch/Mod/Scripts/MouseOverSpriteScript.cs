using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverSpriteScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private SpriteRenderer targetSR;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("[OnPointerEnter]");
        targetSR.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("[OnPointerExit]");
        targetSR.color = Color.white;
    }

    private void OnEnable()
    {
        
    }
    public IEnumerator temp()
    {
        yield return null;
    }

    private void OnMouseEnter()
    {
        Debug.Log("[OnPointerEnter]");
        targetSR.color = Color.red;
    }
    private void OnMouseExit()
    {
        Debug.Log("[OnPointerExit]");
        targetSR.color = Color.white;
    }
    // onmouse
}
