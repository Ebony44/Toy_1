using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryLab
{
    public class ItemGrid : MonoBehaviour
    {

        const float tileSizeWidth = 32;
        const float tileSizeHeight = 32;
        RectTransform rectTransform;

        Vector2 positionOnTheGrid = new Vector2();
        Vector2Int tileGridPosition = new Vector2Int();

        InventoryItem[,] inventoryItemSlot;

        [SerializeField] int gridSizeWidth = 20;
        [SerializeField] int gridSizeHeight = 10;

        [SerializeField] GameObject itemPrefab;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            Init(gridSizeWidth, gridSizeHeight);

            var currentPrefab = Instantiate(itemPrefab);
            currentPrefab.name = "createdItem";
            var currentInventoryItem = currentPrefab.GetComponent<InventoryItem>();
            PlaceItem(currentInventoryItem, 4, 5);

            var currentInventoryItem2 = Instantiate(itemPrefab).GetComponent<InventoryItem>();
            PlaceItem(currentInventoryItem2, 2, 1);

            var currentInventoryItem3 = Instantiate(itemPrefab).GetComponent<InventoryItem>();
            PlaceItem(currentInventoryItem3, 1, 4);


        }

        public InventoryItem PickUpItem(int x, int y)
        {
            Debug.Log("x and y is " + x + " " + y);
            InventoryItem result = inventoryItemSlot[x, y];
            inventoryItemSlot[x, y] = null;
            return result;
            
        }

        private void Init(int width, int height)
        {
            inventoryItemSlot = new InventoryItem[width,height];
            Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
            rectTransform.sizeDelta = size;
        }

        public Vector2Int GetTileGridPosition(Vector2 mousePos)
        {
            // Vector2Int result = new Vector2Int();
            positionOnTheGrid.x = mousePos.x - rectTransform.position.x;
            positionOnTheGrid.y = mousePos.y - rectTransform.position.y;

            tileGridPosition.x = (int)(positionOnTheGrid.x / tileSizeWidth);
            tileGridPosition.y = (int)(positionOnTheGrid.y / tileSizeHeight);

            return tileGridPosition;
        }

        // if 20 x 10
        // left top is 0,0
        // left bottom is 0,-9
        // right most is 19,0
        // right bottom is 19,-9

        public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
        {
            RectTransform currentRectTransform = inventoryItem.GetComponent<RectTransform>();
            currentRectTransform.SetParent(rectTransform);
            inventoryItemSlot[posX, posY] = inventoryItem;


            Vector2 currentPosition = new Vector2();
            // currentPosition.x = posX * tileSizeWidth + gridSizeWidth / 2;
            currentPosition.x = Math.Clamp(posX * tileSizeWidth, 1 * tileSizeWidth, gridSizeWidth * tileSizeWidth);
            // currentPosition.y = posY * tileSizeHeight + gridSizeHeight / 2;
            currentPosition.y = posY * tileSizeHeight * -1;

            Debug.Log("pos x and y are " + posX + " " + posY
                + " current x and y " + currentPosition.x + " " + currentPosition.y);

            currentRectTransform.localPosition = currentPosition;



            
        }

    }

}

