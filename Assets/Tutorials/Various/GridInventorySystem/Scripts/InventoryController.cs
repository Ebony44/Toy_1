using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InventoryLab
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] ItemGrid selectedItemGrid;


        private void Update()
        {
            if (selectedItemGrid == null) { return; }

            Vector3 readValue = Mouse.current.position.ReadValue();
            

            if (Application.isFocused && Mouse.current.leftButton.isPressed)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    Debug.Log(selectedItemGrid.GetTileGridPosition(readValue));
                }
            }


        }
    }

}
