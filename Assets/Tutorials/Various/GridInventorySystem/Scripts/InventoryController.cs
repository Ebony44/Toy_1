using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InventoryLab
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] ItemGrid selectedItemGrid;

        InventoryItem selectedItem;

        RectTransform currentItemRect;


        private void Update()
        {
            if(selectedItem != null)
            {
                currentItemRect.position = Mouse.current.position.ReadValue();
            }
            if (selectedItemGrid == null) { return; }

            Vector3 readValue = Mouse.current.position.ReadValue();


            
            

            if (Application.isFocused && Mouse.current.leftButton.isPressed)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    Vector2Int tileGridPos = selectedItemGrid.GetTileGridPosition(readValue);
                    if(tileGridPos.x < 0)
                    {
                        tileGridPos.x *= -1;
                    }
                    if(tileGridPos.y < 0)
                    {
                        tileGridPos.y *= -1;
                    }
                    Debug.Log("tileGridPos is " + tileGridPos.ToString());
                    if(selectedItem == null)
                    {
                        selectedItem = selectedItemGrid.PickUpItem(tileGridPos.x, tileGridPos.y);
                        if(selectedItem != null)
                        {
                            currentItemRect = selectedItem.GetComponent<RectTransform>();
                        }
                    }
                    else
                    {
                        selectedItemGrid.PlaceItem(selectedItem,tileGridPos.x,tileGridPos.y);
                        selectedItem = null;
                    }

                    Debug.Log(selectedItemGrid.GetTileGridPosition(readValue));
                }
            }


        }
    }

}
