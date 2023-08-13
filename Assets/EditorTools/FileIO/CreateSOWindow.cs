#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

public class CreateSOWindow : MonoBehaviour
{
    public void GetActiveProfileFolder()
    {
        var path = string.Empty;
        var _tryGetActiveFolderPath = typeof(ProjectWindowUtil).GetMethod("TryGetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic);

        object[] args = new object[] { null };
        bool found = (bool)_tryGetActiveFolderPath.Invoke(null, args);
        path = (string)args[0];

        Debug.Log("current path is " + path);


    }
}
#endif