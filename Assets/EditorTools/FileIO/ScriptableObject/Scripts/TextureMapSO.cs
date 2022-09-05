using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "TextureMap", menuName = "EditorToolsSO")]
public class TextureMapSO : ScriptableObject
{
    public Object baseMap;
    public Object metallicGlossMap;
    public Object heightDisplacementMap;
    public Object NormalMap;
}
