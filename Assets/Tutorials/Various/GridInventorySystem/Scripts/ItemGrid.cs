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

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();

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
    }

}

