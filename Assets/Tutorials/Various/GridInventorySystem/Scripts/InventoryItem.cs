using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryLab
{
    public class InventoryItem : MonoBehaviour
    {

        public GridItemData itemData;

        public int sizeWidth = 1;
        public int sizeHeight = 1;

        internal void Set(GridItemData gridItemData)
        {
            itemData = gridItemData;
            GetComponent<Image>().sprite = itemData.itemIcon;

            Vector2 size = new Vector2();
            size.x = itemData.width * ItemGrid.tileSizeWidth;
            size.y = itemData.height * ItemGrid.tileSizeHeight;
            // ..
            var currentRect = GetComponent<RectTransform>();
            currentRect.sizeDelta = size;

        }
    }

}
