using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;


[System.Serializable]
public class MaterialCreateWindow : EditorWindow
{
    //public GameObject assetSrcFolderPath;
    //public GameObject assetDstPath;

    private enum eCurrentTexture
    {
        BASEMAP = 0,
        METALLIC = 1,
        HEIGHT = 2,
        NORMAL = 3,

        NOT_FOUND,
        COUNT_MAX,

    };

    public Object assetSrcFolderPath;
    public Object assetDstPath;

    #region for 3dtexture
    public bool bIsFrom3dTexture;
    public const string BASEMAP_NAME_3D_TEXTURE = "_basecolor";
    public const string METALLIC_NAME_3D_TEXTURE = "_metallic";
    public const string HEIGHT_NAME_3D_TEXTURE = "_height";
    public const string NORMAL_NAME_3D_TEXTURE = "_normal";
    #endregion
    #region for poliigon
    public bool bIsFromPoliigon;
    public const string BASEMAP_NAME_POLIIGON = "_Flat";
    public const string METALLIC_NAME_POLIIGON = "_gloss_";
    public const string HEIGHT_NAME_POLIIGON = "_disp_";
    public const string NORMAL_NAME_POLIIGON = "_nrm_";
    // _NRM
    // _GLOSS
    // _DISP
    #endregion

    public Object baseMap;
    // public SerializedProperty metallicGlossMap;
    public Object metallicGlossMap; // succeed
    public Object heightDisplacementMap;
    
    public Object NormalMap;

    public SerializedObject tempMap;

    SerializedObject currentMapSOSetting; // currently not used

    public TextureMapSO textureMapSO;
    // public Texture2D tempMap2;
    [SerializeField] [ReadOnly] private string currentMatName;

    private List<Object> maps = new List<Object>(4);


    [MenuItem("Tools/MaterialCreator")]
    public static void Open()
    {
        MaterialCreateWindow window = EditorWindow.CreateInstance<MaterialCreateWindow>();

        window.Show();

    }
    private void OnEnable()
    {
        // EditorToolTextureMap
        textureMapSO = AssetDatabase.FindAssets("EditorToolTextureMap")
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
            .Select(path => AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path))
            .First() as TextureMapSO;

        Debug.Log("[OnEnable], is texturemap null? " + (textureMapSO == null));
        currentMapSOSetting = new SerializedObject(textureMapSO);
        // m_packetPrefabProp = m_setting.FindProperty("packetLogPrefab");

    }
    private void OnDisable()
    {
        maps.Clear();
        Debug.Log("map list count " + maps.Count);
    }

    private void OnGUI()
    {
        GUILayout.Space(5f);
        EditorGUILayout.LabelField("Setting up the resource is come from, incorrect setting will cause unidentified result");
        GUILayout.Space(5f);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Poliigon", GUILayout.Width(100f));
        EditorGUILayout.LabelField("3DTexture", GUILayout.Width(100f));
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(5f);
        EditorGUILayout.BeginHorizontal();
        bIsFromPoliigon = EditorGUILayout.Toggle(bIsFromPoliigon, GUILayout.Width(100f));
        bIsFrom3dTexture = EditorGUILayout.Toggle(bIsFrom3dTexture, GUILayout.Width(100f));
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(20f);

        // EditorGUILayout.ObjectField()
        if (GUILayout.Button("ShowSelection"))
        {
            TestSelectionShow(); // dst folder path

            Debug.Log(GetSelectedFilePathOrFallback()); // project folder's current file
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

        GUILayout.Space(40f);

        if (GUILayout.Button("SetMapsFromSrc"))
        {
            if (assetDstPath != null)
            {
                var path = AssetDatabase.GetAssetPath(assetSrcFolderPath);
                Debug.Log("path is " + path);
                SetSourceMaps(path);
            }
        }
        GUILayout.Space(20f);

        if (GUILayout.Button("CreateMaterial"))
        {
            if (assetDstPath != null)
            {
                var path = AssetDatabase.GetAssetPath(assetDstPath);
                Debug.Log("path is " + path);
                CreateMaterial(path);
            }

            // Debug.Log(GetSelectedFilePathOrFallback());
        }

        GUILayout.Space(20f);


        #region current map setting
        EditorGUILayout.BeginVertical();
        
        if(string.IsNullOrEmpty(currentMatName))
        {
            currentMatName = "currently no map is set";
        }
        // new Rect(0f,0f,250f,35f)
        // GUILayout.BeginArea(new Rect(0f, 0f, 50f, 55f));
        EditorGUILayout.BeginVertical("box");
        GUIStyle richStyle = new GUIStyle(GUI.skin.label);
        richStyle.richText = true;
        EditorGUILayout.LabelField(currentMatName, richStyle);
        EditorGUILayout.EndVertical();
        // GUILayout.EndArea();

        GUILayout.Space(5f);
        EditorGUILayout.LabelField("Basemap", GUILayout.Width(200f));
        GUILayout.Space(5f);
        baseMap = EditorGUILayout.ObjectField(baseMap, typeof(UnityEngine.Object), false, GUILayout.Width(200f));

        EditorGUILayout.LabelField("MetallicGloss", GUILayout.Width(200f));
        GUILayout.Space(5f);
        // metallicGlossMap = EditorGUILayout.ObjectField(metallicGlossMap, typeof(UnityEngine.Object), false, GUILayout.Width(200f));
        // EditorGUILayout.ObjectField(tempMap, GUILayout.Width(200f));

        //if(metallicGlossMap != null)
        //{
        //    // EditorGUILayout.ObjectField(metallicGlossMap, GUIContent.none, GUILayout.Width(200f));
        //    metallicGlossMap = currentMapSOSetting.FindProperty("metallicGlossMap");
        //    metallicGlossMap.objectReferenceValue =
        //        EditorGUILayout.ObjectField(metallicGlossMap.objectReferenceValue, typeof(UnityEngine.Object), false, GUILayout.Width(300f));
        //}

        // metallicGlossMap = EditorGUILayout.ObjectField(metallicGlossMap, typeof(UnityEngine.Object), false, GUILayout.Width(200f));
        EditorGUILayout.ObjectField(metallicGlossMap, typeof(UnityEngine.Object), false, GUILayout.Width(200f));

        EditorGUILayout.LabelField("HeightDisplacement", GUILayout.Width(200f));
        GUILayout.Space(5f);
        // heightDisplacementMap = EditorGUILayout.ObjectField(heightDisplacementMap, typeof(UnityEngine.Object), false, GUILayout.Width(200f));
        EditorGUILayout.ObjectField(heightDisplacementMap, typeof(UnityEngine.Object), false, GUILayout.Width(200f));

        EditorGUILayout.LabelField("Normalmap", GUILayout.Width(200f));
        GUILayout.Space(5f);

        // NormalMap = EditorGUILayout.ObjectField(NormalMap, typeof(UnityEngine.Object), false, GUILayout.Width(200f));
        EditorGUILayout.ObjectField(NormalMap, typeof(UnityEngine.Object), false, GUILayout.Width(200f));

        // NormalMap = (Texture)EditorGUILayout.ObjectField(NormalMap, typeof(UnityEngine.Texture), false, GUILayout.Width(200f));
        EditorGUILayout.EndVertical();
        #endregion

        GUILayout.Space(5f);

        // currentMapSOSetting.ApplyModifiedProperties();
        // currentMapSOSetting.Update();
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

    private void SetSourceMaps(string path)
    {
        currentMapSOSetting.Update();
        Dictionary<eCurrentTexture, Object> tempDic = new Dictionary<eCurrentTexture, Object>(capacity: 5);
        // 0. grab folder's file's base

        if (bIsFrom3dTexture)
        {
            
            // var baseString = new DirectoryInfo(path);
            var dirInfo = new DirectoryInfo(path);
            var sb = new StringBuilder();
            Dictionary<string, eCurrentTexture> textureTypeDic = new Dictionary<string, eCurrentTexture>(capacity: 5);
            foreach (var item in dirInfo.GetFiles())
            {
                if (item.Name.Contains(".meta"))
                {
                    continue;
                }

                var compareStringData = item.Name.ToLower();

                Debug.Log("item name is " + item.Name
                    + "\n" + " full name is " + item.FullName);

                //if (item.Name.Contains(METALLIC_NAME_3D_TEXTURE))
                //{
                //    GetSubstringPath(item.FullName, "Asset");
                //}

                //metallicGlossMap = item.Name.Contains(METALLIC_NAME_3D_TEXTURE)
                ////? AssetDatabase.LoadAssetAtPath(item.FullName, typeof(UnityEngine.Object))
                //? AssetDatabase.LoadAssetAtPath<SerializedProperty>(GetSubstringPath(item.FullName, "Asset"))
                //    : null;

                // if (currentMapSOSetting != null)
                if (true) // temp statement
                {
                    var assetPath = GetSubstringPath(item.FullName, "Asset");
                    var currentTextureType = compareStringData.Contains(BASEMAP_NAME_3D_TEXTURE) ? eCurrentTexture.BASEMAP :
                        compareStringData.Contains(METALLIC_NAME_3D_TEXTURE) ? eCurrentTexture.METALLIC :
                        compareStringData.Contains(HEIGHT_NAME_3D_TEXTURE) ? eCurrentTexture.HEIGHT :
                        compareStringData.Contains(NORMAL_NAME_3D_TEXTURE) ? eCurrentTexture.NORMAL :
                        eCurrentTexture.NOT_FOUND;
                    if(currentTextureType == eCurrentTexture.BASEMAP)
                    {
                        currentMatName = compareStringData.Replace(BASEMAP_NAME_3D_TEXTURE, string.Empty);
                        currentMatName = char.ToUpper(currentMatName[0]) + currentMatName.Substring(1);
                        currentMatName = HelperFunctions.ReplaceStringToEmpty(currentMatName, new string[] { ".png", ".jpg" });
                        // currentMatName = currentMatName.Replace(".jpg", string.Empty).Replace(".png", string.Empty);
                    }


                    //var currentTexture = compareStringData.Contains(BASEMAP_NAME_3D_TEXTURE) ? AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object)) :
                    //    compareStringData.Contains(METALLIC_NAME_3D_TEXTURE) ? AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object)) :
                    //    compareStringData.Contains(HEIGHT_NAME_3D_TEXTURE) ? AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object)) :
                    //    compareStringData.Contains(NORMAL_NAME_3D_TEXTURE) ? AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object)) :
                    //    null;

                    string[] stringPatterns = new string[] { BASEMAP_NAME_3D_TEXTURE, METALLIC_NAME_3D_TEXTURE, HEIGHT_NAME_3D_TEXTURE, NORMAL_NAME_3D_TEXTURE };

                    var currentTexture = GetCurrentTexture(assetPath, compareStringData, stringPatterns);

                    switch (currentTextureType)
                    {
                        case eCurrentTexture.BASEMAP:
                            baseMap = currentTexture;
                            tempDic.Add(eCurrentTexture.BASEMAP, baseMap);
                            break;
                        case eCurrentTexture.METALLIC:
                            metallicGlossMap = currentTexture;
                            tempDic.Add(eCurrentTexture.METALLIC, metallicGlossMap);
                            break;
                        case eCurrentTexture.HEIGHT:
                            heightDisplacementMap = currentTexture;
                            tempDic.Add(eCurrentTexture.HEIGHT, heightDisplacementMap);
                            break;
                        case eCurrentTexture.NORMAL:
                            NormalMap = currentTexture;
                            tempDic.Add(eCurrentTexture.NORMAL, NormalMap);
                            break;
                        case eCurrentTexture.NOT_FOUND:
                            break;
                        case eCurrentTexture.COUNT_MAX:
                            break;
                        default:
                            Debug.LogError("Something goes wrong");
                            break;



                            //item.Name.Contains(METALLIC_NAME_3D_TEXTURE);
                    }


                    // var temp = AssetDatabase.LoadAssetAtPath(item.FullName, typeof(UnityEngine.Object));

                }
            }



        }
        else if (bIsFromPoliigon)
        {

        }
        if (!bIsFrom3dTexture && !bIsFromPoliigon)
        {
            Debug.LogError("no booleans set to true, set error");
        }
        
        currentMapSOSetting.ApplyModifiedProperties();
        #region is there any smart way for this
        if (tempDic.ContainsKey(eCurrentTexture.BASEMAP) == false)
        {
            
            baseMap = null;
        }
        if (tempDic.ContainsKey(eCurrentTexture.METALLIC) == false)
        {
            
            metallicGlossMap = null;
        }
        if (tempDic.ContainsKey(eCurrentTexture.HEIGHT) == false)
        {
            
            heightDisplacementMap = null;
        }
        if (tempDic.ContainsKey(eCurrentTexture.NORMAL) == false)
        {
            
            NormalMap = null;
        }
        
        #endregion


        // throw new System.NotImplementedException();
    }

    private Object GetCurrentTexture(string assetPath,string compareStringData, string[] searchPattern)
    {
        Object currentTexture = null;
        
        foreach (var item in searchPattern)
        {
            currentTexture = compareStringData.Contains(item) ? AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object)) : null;
            if (currentTexture != null)
            {
                break;
            }
        }
        return currentTexture;
        // return null;
    }
    private eCurrentTexture GetCurrentTextureType(string assetPath, string compareStringData, Dictionary<string,eCurrentTexture> searchPattern)
    {
        eCurrentTexture currentTextureType = eCurrentTexture.NOT_FOUND;

        //var currentTextureType = compareStringData.Contains(BASEMAP_NAME_3D_TEXTURE) ? eCurrentTexture.BASEMAP :
        //                compareStringData.Contains(METALLIC_NAME_3D_TEXTURE) ? eCurrentTexture.METALLIC :
        //                compareStringData.Contains(HEIGHT_NAME_3D_TEXTURE) ? eCurrentTexture.HEIGHT :
        //                compareStringData.Contains(NORMAL_NAME_3D_TEXTURE) ? eCurrentTexture.NORMAL :
        //                eCurrentTexture.NOT_FOUND;

        foreach (var item in searchPattern)
        {
            // currentTexturetype = compareStringData.Contains(item) ? (eCurrentTexture)item : null;
            currentTextureType = compareStringData.Contains(item.Key) ? item.Value : eCurrentTexture.NOT_FOUND;
            if (currentTextureType != eCurrentTexture.NOT_FOUND)
            {
                break;
            }
        }
        return currentTextureType;
        // return null;
    }


    private string GetSubstringPath(string path, string subStringOption)
    {
        var tempStartIndex = path.IndexOf(subStringOption);
        //Debug.Log("index is " + tempStartIndex
        //    + " substring is " + path.Substring(tempStartIndex, path.Length - tempStartIndex));
        // path.Substring(0, tempEndIndex);
        return path.Substring(tempStartIndex, path.Length - tempStartIndex);
    }

    public void CreateMaterial(string folderPath)
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
        if (string.IsNullOrEmpty(currentMatName) == false)
        {
            tempMatches = HelperFunctions.FindMatchAfterCertainString(tempFileNames, currentMatName + "_");
        }
        var tempNumber = HelperFunctions.ParseAndReturnNumber(true, tempMatches);
        Debug.Log("highest number " + tempNumber);
        //foreach (var item in tempMatches)
        //{
        //    Debug.Log("Match is " + item);
        //}




        //// 2. Create a simple material asset

        Material material = new Material(Shader.Find("Standard"));
        ++tempNumber;
#if !DEBUG_DISABLED
        if(string.IsNullOrEmpty(currentMatName) == false)
        {
            AssetDatabase.CreateAsset(material, folderPath +"/" + currentMatName + "_" + tempNumber + ".mat");
        }
        else
        {
            AssetDatabase.CreateAsset(material, folderPath + "/Mat_" + tempNumber + ".mat");
        }
        // AssetDatabase.CreateAsset(material, folderPath + "/Mat_" + tempNumber + ".mat");
#endif
        
        
        //// path is Assets/TempAssetSources/Materials/TestMatFolder

        //// Print the path of the created asset


        Debug.Log(AssetDatabase.GetAssetPath(material));


        // 3. set that asset's property...

        // var tempTexture = (Texture)baseMap;

        if (baseMap != null)
        {
            material?.SetTexture("_MainTex", (Texture)baseMap);
        }
        var temp = material?.GetTexture("_MainTex");
        if (metallicGlossMap != null)
        {
            material?.SetTexture("_MetallicGlossMap", (Texture)metallicGlossMap);
        }
        var temp2 = material?.GetTexture("_MetallicGlossMap");

        if (heightDisplacementMap != null)
        {
            material?.SetTexture("_ParallaxMap", (Texture)heightDisplacementMap);
        }
        var temp3 = material?.GetTexture("_ParallaxMap");
        if (NormalMap != null)
        {
            material?.SetTexture("_BumpMap", (Texture)NormalMap);
        }
        var temp4 = material?.GetTexture("_BumpMap");


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
