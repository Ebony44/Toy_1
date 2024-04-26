using System;
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

        [SerializeField] List<GridItemData> items;

        [SerializeField] GameObject itemPrefab;
        [SerializeField] Transform canvasTransform;


        private void Update()
        {
            ItemIconDrag();

            if(Application.isFocused && Keyboard.current.qKey.wasPressedThisFrame)
            {
                Debug.Log("Q key pressed");
                CreateRandomItem();
            }

            if (selectedItemGrid == null) { return; }

            Vector3 readValue = Mouse.current.position.ReadValue();


            if (Application.isFocused && Mouse.current.leftButton.isPressed)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    LeftMouseButtonPress(readValue);
                }
            }


        }

        private void CreateRandomItem()
        {
            InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
            selectedItem = inventoryItem;

            currentItemRect = inventoryItem.GetComponent<RectTransform>();
            currentItemRect.SetParent(canvasTransform);

            int selectedItemID = UnityEngine.Random.Range(0, items.Count);
            inventoryItem.Set(items[selectedItemID]);

        }

        private void LeftMouseButtonPress(Vector3 readValue)
        {
            Vector2Int tileGridPos = selectedItemGrid.GetTileGridPosition(readValue);
            
            Debug.Log("tileGridPos is " + tileGridPos.ToString());
            if (selectedItem == null)
            {
                PickUpItem(tileGridPos);
            }
            else
            {
                PlaceItem(tileGridPos);
            }

            Debug.Log(selectedItemGrid.GetTileGridPosition(readValue));
        }

        private void PlaceItem(Vector2Int tileGridPos)
        {
            selectedItemGrid.PlaceItem(selectedItem, tileGridPos.x, tileGridPos.y);
            selectedItem = null;
        }

        private void PickUpItem(Vector2Int tileGridPos)
        {
            selectedItem = selectedItemGrid.PickUpItem(tileGridPos.x, tileGridPos.y);
            if (selectedItem != null)
            {
                currentItemRect = selectedItem.GetComponent<RectTransform>();
            }
        }

        private void ItemIconDrag()
        {
            if (selectedItem != null)
            {
                currentItemRect.position = Mouse.current.position.ReadValue();
            }
        }
    }

}
