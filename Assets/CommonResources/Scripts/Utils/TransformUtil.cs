using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformUtil
{
    public static void SetParentWithDefaultLocal(this Transform child, Transform parent,Vector3 scale){
        child.SetParent(parent);
        child.localPosition = Vector3.zero;
        child.localScale = scale;
    }



}
