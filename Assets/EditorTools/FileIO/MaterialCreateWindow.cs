using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

public class MaterialCreateWindow : EditorWindow
{
    //public GameObject assetSrcFolderPath;
    //public GameObject assetDstPath;

    public Object assetSrcFolderPath;
    public Object assetDstPath;

    public bool bIsFrom3dTexture;
    public bool bIsFromPoliigon;

    [MenuItem("Tools/MaterialCreator")]
    public static void Open()
    {
        MaterialCreateWindow window = EditorWindow.CreateInstance<MaterialCreateWindow>();

        window.Show();

    }

    private void OnGUI()
    {
        GUILayout.Space(20f);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Poliigon", GUILayout.Width(100f));
        EditorGUILayout.LabelField("3DTexture", GUILayout.Width(100f));
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(5f);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Toggle(bIsFromPoliigon, GUILayout.Width(100f));
        EditorGUILayout.Toggle(bIsFrom3dTexture, GUILayout.Width(100f));
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(20f);

        // EditorGUILayout.ObjectField()
        if (GUILayout.Button("ShowSelection"))
        {
            TestSelectionShow();

            Debug.Log(GetSelectedFilePathOrFallback());
        }
        GUILayout.Space(20f);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Folder(destination)", GUILayout.Width(200f));
        EditorGUILayout.LabelField("Folder(that contains textures)", GUILayout.Width(200f));
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(5f);
        #region
        EditorGUILayout.BeginHorizontal();

        // assetDstPath = EditorGUILayout.ObjectField(assetDstPath, typeof(UnityEngine.Object), false, GUILayout.Width(200f));
        // assetSrcFolderPath = EditorGUILayout.ObjectField(assetSrcFolderPath, typeof(UnityEngine.Object), false, GUILayout.Width(200f));

        assetDstPath = EditorGUILayout.ObjectField(assetDstPath, typeof(UnityEngine.Object), false, GUILayout.Width(200f));
        assetSrcFolderPath = EditorGUILayout.ObjectField(assetSrcFolderPath, typeof(UnityEngine.Object), false, GUILayout.Width(200f));

        EditorGUILayout.EndHorizontal();
        #endregion

        GUILayout.Space(20f);
        
        if (GUILayout.Button("CreateMaterial"))
        {
            if(assetDstPath != null)
            {
                var path = AssetDatabase.GetAssetPath(assetDstPath);
                Debug.Log("path is " + path);
                CreateMaterial(path);
            }

            // Debug.Log(GetSelectedFilePathOrFallback());
        }

    }

    private void UpdateFolderPath()
    {
        throw new System.NotImplementedException();
    }

    [TestMethod(false)]
    private void TestSelectionShow()
    {
        foreach (var item in Selection.gameObjects)
        {

            Debug.Log("current is " + item.name);
        }
        if (assetDstPath != null)
        {
            DisplayPath(assetDstPath);
        }
    }
    public string GetSelectedPathOrFallback()
    {
        string path = "Assets";

        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        return path;
    }
    public string GetSelectedFilePathOrFallback()
    {
        string path = "Assets";

        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetFileName(path);
                break;
            }
        }
        return path;
    }

    static void CreateMaterial(string folderPath)
    {
        // 0. check directory path and folder
        HelperFunctions.CreateDirectory(folderPath);

        // 1. check any file name and make it count
        var tempFiles = Directory.GetFiles(folderPath);

        string[] tempFileNames = new string[tempFiles.Length];
        var tempIndex = 0;
        foreach (var file in tempFiles)
        {
            if (file.Contains(".meta"))
            {
                continue;
            }
            Debug.Log("file name is " + Path.GetFileName(file));
            tempFileNames[tempIndex] = Path.GetFileName(file);
            ++tempIndex;
        }

        var recentFileName = string.Empty;

        var tempMatches = HelperFunctions.FindMatchAfterCertainString(tempFileNames, "Mat_");
        var tempNumber = HelperFunctions.ParseAndReturnNumber(true, tempMatches);
        Debug.Log("highest number " + tempNumber);
        foreach (var item in tempMatches)
        {
            Debug.Log("Match is " + item);
        }




        //// 2. Create a simple material asset
        
        Material material = new Material(Shader.Find("Standard"));
        ++tempNumber;
        AssetDatabase.CreateAsset(material, folderPath + "/Mat_" + tempNumber + ".mat");

        //// path is Assets/TempAssetSources/Materials/TestMatFolder

        //// Print the path of the created asset

        Debug.Log(AssetDatabase.GetAssetPath(material));

        // 3. set that asset's property...

        // material.SetTexture("Albedo",)



    }

    private void DisplayPath(Object folderReference)
    {
        // var temp = AssetDatabase.FindAssets("PacketLogInfoItem");
        // var guid = property.FindPropertyRelative("GUID");
        // var obj = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GUIDToAssetPath(guid.stringValue));

        var path = AssetDatabase.GetAssetPath(folderReference);
        Debug.Log("path is " + path);
    }

    private void GetFileFromPaths()
    {

    }

}
