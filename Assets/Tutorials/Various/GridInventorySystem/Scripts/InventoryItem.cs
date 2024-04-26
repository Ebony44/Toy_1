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
        }
    }

}
