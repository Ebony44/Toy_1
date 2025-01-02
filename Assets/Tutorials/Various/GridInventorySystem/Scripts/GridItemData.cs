using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Test/ScriptableObject/Enemy Configuration")]
public class GridItemData : ScriptableObject
{
    public int width = 1;
    public int height = 1;

    // public Texture2D itemIcon;
    public Sprite itemIcon;

}
