using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModTestGroundScripts : MonoBehaviour
{
    private void Start()
    {
        GameObject emptyObj = new GameObject();
        emptyObj.name = "Merchandise List(Window)";
        GameObject childCanvas = new GameObject();
        childCanvas.name = "Canvas";
        
        childCanvas.transform.SetParent(emptyObj.transform);
        var compCanvas = childCanvas.AddComponent<Canvas>();
        compCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        compCanvas.worldCamera = Camera.main;
        compCanvas.sortingOrder = 115;
        compCanvas.sortingLayerName = "UI";
        // compCanvas.renderOrder = 14;

        #region
        #region 
        // C:\Users\user\Downloads\FFF(FapForFun)
        // \Monster_Black_Market_v2.0.15.1DLC\Monster Black Market v2.0.15.1+DLC
        // \BepInEx\plugins\MBM_CustomPlayer_Mod
        var prefix = "file:";
        var folderPath = "C:\\Users\\user\\Documents\\TempImage";
        // folderPath = folderPath.Remove(folderPath.Length - 4, 4);
        if (!System.IO.Directory.Exists(folderPath))
        {
            System.IO.Directory.CreateDirectory(folderPath);
        }
        var fileName = "GoblinMerchant.png";
        var filePath = System.IO.Path.Combine(prefix, folderPath, fileName);
        Debug.Log("filePath " + filePath);
        // var filePath = "file:C:/ImageTest/ShadowIntro.PNG";
        Texture2D outTexture = null;
        CreateTextureFromFile(filePath, out outTexture);
        #endregion
        GameObject gameObject = new GameObject();
        gameObject.transform.position = Vector3.zero;
        gameObject.name = "empty created";
        var tempSprite = gameObject.AddComponent<SpriteRenderer>();
        tempSprite.sprite = Sprite.Create(
            outTexture,
            new Rect(0, 0, outTexture.width, outTexture.height),
            new Vector2(outTexture.width / 2, outTexture.height / 2),
            50f,
            0,
            SpriteMeshType.FullRect
            // Vector2.zero
            );
        #endregion
    }


    public void CreateTextureFromFile(string filePath, out Texture2D outTexture)
    {
        Texture2D tex;
        // outTexture = null;
        tex = new Texture2D(64, 64, TextureFormat.BGRA32, false);
        outTexture = tex;
        // WWW www = new WWW(filePath);
        // www.LoadImageIntoTexture(outTexture);
        var tempBool = System.IO.File.Exists(filePath);
        if (System.IO.File.Exists(filePath))
        {
            byte[] fileData = System.IO.File.ReadAllBytes(filePath);
            outTexture = new Texture2D(64, 64, TextureFormat.BGRA32, false);

            outTexture.LoadImage(fileData);

        }


    }


    public GameObject testParent;
    public GameObject testPrefab;
    public ModTestGroundScripts testScript;
    public Button testButton;
    public GameObject testButtonObj;

    [TestMethod(false)]
    public void TestGetChildren(string gameObjectName)
    {
        // Image
        var childList = testParent.GetComponentsInChildren<Transform>(); // also include parent at 0 index
        GameObject testParentObj = null;
        string findParentName = "TestParent";
        foreach (var item in childList)
        {
            if (item.gameObject.name.Contains("TestChild (1)"))
            {
                Debug.Log("found");
            }

            if (item.gameObject.name.Contains(findParentName))
            {
                testParentObj = GameObject.Find(findParentName);
                var childList_2 = testParentObj.GetComponentsInChildren<Transform>();
            }
        }

        var testFind = GameObject.Find("asdf");
        Debug.Log("is find? " + (testFind != null));


    }

    [TestMethod(false)]
    public void TestDuplicateAndRemoveChildren()
    {
        // var findObejct = GameObject.Find(gameObjectName);
        var dupObj = GameObject.Instantiate(testParent);


        var childList = dupObj.GetComponentsInChildren<Transform>(); // also include parent at 0 index
        GameObject testParentObj = null;
        string findParentName = "TestParent";
        foreach (var item in childList)
        {
            if (item.gameObject.name.Contains("TestChild (1)"))
            {
                Debug.Log("found");
            }

            if (item.gameObject.name.Contains(findParentName))
            {

                // testParentObj = GameObject.Find(findParentName);
                // testParentObj = item.gameObject;
                // var childList_2 = testParentObj.GetComponentsInChildren<Transform>();
                Destroy(item.gameObject);
            }
        }





    }

    [TestMethod(false)]
    public void TestPrefabModifying()
    {
        // this 2 line of code will effect exist prefab.
        // var tempImage = testPrefab.GetComponent<Image>();
        // tempImage.color = Color.black;

        // clone and modify
        var tempObj = GameObject.Instantiate(testPrefab);
        tempObj.SetActive(false);
        var tempImage = tempObj.GetComponent<Image>();
        tempImage.color = Color.black;
        // testPrefab = tempObj;
        // Destroy(tempObj);


    }

    [TestMethod(false)]
    public void TestDestroyComponent(GameObject obj)
    {
        var tempList = obj.GetComponents<Component>();
        var compCount = tempList.Length;
        for (int i = 0; i < tempList.Length; i++)
        {
            var currentComp = tempList[i];
            if (currentComp.ToString().Contains("PoolItemManager"))
            {
                GameObject.Destroy(currentComp);
            }
        }
        

    }

    [TestMethod(false)]
    public void ShowLog()
    {
        Debug.Log("Show log " + (testScript == null));

        var temp = testParent.GetComponents<Component>();
        foreach (var item in temp)
        {
            Debug.Log("temp is " + item.GetType());
        }
    }


    public void ShowOnClickLog()
    {
        Debug.Log("asdf");
    }

    [TestMethod(false)]
    public void AssignButtonEvent()
    {
        // testButton.onClick.AddListener(ShowOnClickLog);

        var currentButton = testButtonObj.AddComponent<Button>();
        currentButton.transition = Selectable.Transition.None;
        currentButton.onClick.AddListener(ShowOnClickLog);

        // StartCoroutine(AssignButtonEventRoutine());

    }

    public IEnumerator AssignButtonEventRoutine()
    {
        var currentButton = testButtonObj.AddComponent<Button>();
        yield return new WaitForSeconds(0.1f);
        currentButton.transition = Selectable.Transition.None;
        currentButton.onClick.AddListener(ShowOnClickLog);
    }


}

