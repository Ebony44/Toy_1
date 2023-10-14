using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ModdingUIPracs : MonoBehaviour
{
    public Slider corruptionSourceSlider;
    public RawImage flowRawImage;
    public float flowScrollSpeed = 1f;

    public const int MAX_CORRUPT_POINT = 100;
    public float currentCorruptionPoint;
    public float corruptIncreaseAmount = 1;

    public Canvas parentCanvas;

    public RectTransform sampleBackgroundImage;

    public UnityEvent onClickEvent;


    private void Awake()
    {
        #region event handler init
        var EventChannelContainerObj = new GameObject();
        EventChannelContainerObj.name = typeof(EventChannelContainer).Name;
        EventChannelContainerObj.AddComponent<EventChannelContainer>();
        #endregion
    }

    #region functions for fill bar related


    private void Update()
    {
        if (flowRawImage != null)
        {
            Rect uvRect = flowRawImage.uvRect;
            uvRect.y += flowScrollSpeed * Time.deltaTime;
            flowRawImage.uvRect = uvRect;
        }
        if(corruptionSourceSlider != null)
        {
            if(corruptIncreaseAmount <= 0)
            {
                
            }
            else
            {
                if (currentCorruptionPoint >= MAX_CORRUPT_POINT)
                {
                    currentCorruptionPoint = MAX_CORRUPT_POINT;
                }
                else if (currentCorruptionPoint < MAX_CORRUPT_POINT)
                {
                    currentCorruptionPoint += corruptIncreaseAmount * Time.deltaTime;
                }
                corruptionSourceSlider.value = NormalizeCorrupt();
            }
        }
    }
    [TestMethod(false)]
    private void PurgeCorruption(int purgePoint)
    {
        if(purgePoint > currentCorruptionPoint)
        {
            currentCorruptionPoint = 0;
        }
        else
        {
            currentCorruptionPoint -= purgePoint;
        }
    }

    private void FillUpSlider()
    {

    }
    private float NormalizeCorrupt()
    {

        return currentCorruptionPoint / MAX_CORRUPT_POINT;
    }

    #endregion

    #region variables for created

    public Sprite backGroundSprite;
    public Sprite fillSprite;
    public Texture fillMaskedGaugeSprite;

    public Texture2D fillMaskedGaugeSpriteForTwoDimention;

    #endregion

    [Header("UIForPermaUpgrade")]
    public Image parentBackgroundForPermaUpgrade;
    public List<Button> upgradeValueModButtons;

    // public Canvas testDebugDisplayCanvas;
    public RectTransform testDebugDisplayRect;
    public RectTransform testDebugDisplayRect_2;
    public TextMeshProUGUI testDebugDisplayTMPRO;
    public Sprite loadingImageSprite;

    public enum EPermaUpgradeTitle
    {
        Basic = 0,
        Advanced,
        Expert,
    }

    private void Start()
    {
        // InitSliderUI();
        // InitBlockingCanvas();

        var currentRect = testDebugDisplayRect_2;

        InitPermaUpgrade();

    }

    #region value, status related
    public void SetupStatus()
    {

    }
    #endregion

    #region create UIs

    #region perma upgrade ui

    public Sprite upgradeBackgroundSprite;
    public Sprite tabLayoutSprite;
    public Sprite tabButtonSprite;
    public Sprite upItemBackSprite;
    public Sprite IncDecButtonSprite;

    public void InitPermaUpgrade()
    {
        (GameObject, Canvas, CanvasScaler, GraphicRaycaster) permaUpgradeTuple 
            = CreateDefaultCanvas("PermaUpgradeCanvas", 2000, true);

        GameObject windowObj = permaUpgradeTuple.Item1;
        Canvas upgradeCanvas = permaUpgradeTuple.Item2;
        CanvasScaler currentScaler = permaUpgradeTuple.Item3;

        upgradeCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        upgradeCanvas.worldCamera = Camera.main;
        currentScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        currentScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
        currentScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        currentScaler.referencePixelsPerUnit = 100f;

        #region child 1 to 3
        // child, background image
        GameObject backgroundImageObj = new GameObject();
        backgroundImageObj.transform.SetParent(windowObj.transform);
        backgroundImageObj.gameObject.name = "PeramUpgradeBackground";
        var backgroundImage = backgroundImageObj.AddComponent<Image>();
        backgroundImage.sprite = upgradeBackgroundSprite;
        backgroundImage.type = Image.Type.Sliced;
        // blockingImage.color = new Color(0f, 0f, 0f, 0.4f);
        var currentBackgroundRect = AddAndGetRectComp(backgroundImageObj);

        currentBackgroundRect.anchorMin = new Vector2(0.5f, 0.5f);
        currentBackgroundRect.anchorMax = new Vector2(0.5f, 0.5f);
        currentBackgroundRect.pivot = new Vector2(0.5f, 0.5f);

        currentBackgroundRect.anchoredPosition = new Vector2(0f, 0f);
        currentBackgroundRect.sizeDelta = new Vector2(1000f, 900f);
        currentBackgroundRect.localScale = Vector3.one;

        // child_2, mask
        var maskBackgroundObj = CreateDefaultGameObject("BackgroundMask", currentBackgroundRect);
        var currentMaskImage = maskBackgroundObj.AddComponent<Image>();
        currentMaskImage.raycastTarget = false;
        
        var currentMaskRect = AddAndGetRectComp(maskBackgroundObj);
        currentMaskRect.anchorMin = Vector2.zero;
        currentMaskRect.anchorMax = Vector2.one;
        currentMaskRect.anchoredPosition = new Vector2(0f, -21f); // -21f for title text area
        currentMaskRect.offsetMax = new Vector2(-8f, -50f);
        currentMaskRect.offsetMin = new Vector2(8f, 8f);
        currentMaskRect.pivot = new Vector2(0.5f, 0.5f);
        currentMaskRect.localScale = Vector3.one;

        // currentMaskRect.sizeDelta = new Vector2(-16f, -58f); // 52 + 6
        // TODO for offset setting

        var currentMask = maskBackgroundObj.AddComponent<Mask>();
        currentMask.showMaskGraphic = false;

        // child_2, title
        var titleObj = CreateDefaultGameObject("PermaTitle", currentBackgroundRect);
        var currentTitleRect = AddAndGetRectComp(titleObj);
        currentTitleRect.anchorMin = new Vector2(0.5f, 1f);
        currentTitleRect.anchorMax = new Vector2(0.5f, 1f);
        currentTitleRect.pivot = new Vector2(0.5f, 1f);
        currentTitleRect.anchoredPosition = Vector2.zero;
        currentTitleRect.offsetMax = new Vector2(50f, 0f);
        currentTitleRect.offsetMin = new Vector2(-50f, -50f);
        currentTitleRect.localScale = Vector3.one;

        // child_3 title text
        var titleTextObj = CreateDefaultGameObject("PermaTitle_Text", currentTitleRect);
        var currentTitleTextRect = AddAndGetRectComp(titleTextObj);
        currentTitleTextRect.anchorMin = new Vector2(0.5f, 1f);
        currentTitleTextRect.anchorMax = new Vector2(0.5f, 1f);
        currentTitleTextRect.pivot = new Vector2(0.5f, 1f);
        currentTitleTextRect.anchoredPosition = Vector2.zero;
        currentTitleTextRect.offsetMax = new Vector2(50f, 0f);
        currentTitleTextRect.offsetMin = new Vector2(-50f, -50f);
        currentTitleTextRect.sizeDelta = new Vector2(200f, 50f);
        currentTitleTextRect.localScale = Vector3.one;

        var currentTitleTextUGUI = titleTextObj.AddComponent<TextMeshProUGUI>();
        currentTitleTextUGUI.fontSize = 36f;
        currentTitleTextUGUI.alignment = TextAlignmentOptions.Center;

        // var currentTitle


        // child_3, tabs , it's empty gameobject
        GameObject currentTabs = CreateDefaultGameObject("PermaTabs", currentMaskRect);
        var currentTabRect = AddAndGetRectComp(currentTabs);
        currentTabRect.anchorMin = new Vector2(0f, 1f);
        currentTabRect.anchorMax = new Vector2(1f, 1f);
        
        currentTabRect.offsetMin = new Vector2(0f, -50f);
        currentTabRect.offsetMax = new Vector2(0f, 50f);
        currentTabRect.pivot = new Vector2(0f, 1f);
        currentTabRect.sizeDelta = new Vector2(0f, 100f);

        currentTabRect.anchoredPosition = new Vector2(0f, 50f); // always adjust anchoredPosition at last step
        currentTabRect.localScale = Vector2.one;

        #endregion

        // child_4 tab layout
        GameObject currentTabLayoutObj = CreateDefaultGameObject("TabLayout", currentTabRect);
        var currentTabLayoutRect = AddAndGetRectComp(currentTabLayoutObj);

        currentTabLayoutRect.anchorMin = new Vector2(0f, 0.5f);
        currentTabLayoutRect.anchorMax = new Vector2(1f, 0.5f);

        currentTabLayoutRect.offsetMin = new Vector2(0f, -100f);
        currentTabLayoutRect.offsetMax = new Vector2(0f, 0f);
        currentTabLayoutRect.pivot = new Vector2(0f, 1f);
        currentTabLayoutRect.sizeDelta = new Vector2(0f, 100f);
        
        currentTabLayoutRect.anchoredPosition = new Vector2(0f, 0f); // always adjust anchoredPosition at last step
        currentTabLayoutRect.localScale = Vector2.one;

        var currentTabLayoutImage = currentTabLayoutObj.AddComponent<Image>();
        currentTabLayoutImage.raycastTarget = false;
        currentTabLayoutImage.type = Image.Type.Sliced;
        currentTabLayoutImage.sprite = tabLayoutSprite;

        var currentTabLayoutGroup = currentTabLayoutObj.AddComponent<HorizontalLayoutGroup>();
        currentTabLayoutGroup.childAlignment = TextAnchor.MiddleLeft;
        currentTabLayoutGroup.padding.left = 15;
        currentTabLayoutGroup.padding.right = 15;
        currentTabLayoutGroup.padding.bottom = 4;
        currentTabLayoutGroup.childForceExpandWidth = true;
        currentTabLayoutGroup.childControlWidth = true;

        currentTabLayoutGroup.childForceExpandHeight = false;
        currentTabLayoutGroup.childControlHeight = false;

        // child_5 tab 1 breeding
        //GameObject firstTabButtonObj = CreateDefaultGameObject("Tab_1_Breeding", currentTabLayoutRect);
        //var firstTabButtonRect = AddAndGetRectComp(firstTabButtonObj);
        //firstTabButtonRect.sizeDelta = new Vector2(235f, 80f);
        //firstTabButtonRect.localScale = Vector2.one;

        //var firstTabImage = firstTabButtonObj.AddComponent<Image>();
        //firstTabImage.sprite = tabButtonSprite;
        //firstTabImage.type = Image.Type.Sliced;
        //var firstTabButton = firstTabButtonObj.AddComponent<Button>();
        //firstTabButton.targetGraphic = firstTabImage;

        var tabButtonList = CreateButtonsWithParam("Tab_", 4, currentTabLayoutRect);
        // add listener from above list

        //

        // child_3, contents , there should be translucent script
        GameObject currentContents = CreateDefaultGameObject("PermaContents", currentMaskRect);
        var currentContentsRect = AddAndGetRectComp(currentContents);
        currentContentsRect.anchorMax = new Vector2(1f, 1f);
        currentContentsRect.anchorMin = new Vector2(0f, 0f);
        currentContentsRect.offsetMax = new Vector2(0f, -100f);
        currentContentsRect.offsetMin = new Vector2(0f, 80f);
        currentContentsRect.pivot = new Vector2(0.5f, 0.5f);
        currentContentsRect.sizeDelta = new Vector2(0f, -180f);
        currentContentsRect.anchoredPosition = new Vector2(0f, -10f); // always adjust anchoredPosition at last step
        currentContentsRect.localScale = Vector2.one;
        // child_4, contents_1, content
       

        var contentList = CreateContentsWithParam("Content_", 4, currentContentsRect);
        // add listener from above list


        // child_5, contents_1, upgrade lists

        Dictionary<int, List<GameObject>> currentUpgradeListDic = new Dictionary<int, List<GameObject>>(32);
        List<GameObject> currentUpgradeList = new List<GameObject>(32);
        for (int i = 0; i < contentList.Count; i++)
        {
            var currentParentRect = AddAndGetRectComp(contentList[i]);
            // currentUpgradeList = CreateContentsUpListWithParam("Content_" + (i + 1), 3, currentParentRect);
            var upName = "Content_" + (i + 1);
            currentUpgradeListDic.Add(i, CreateContentsUpListWithParam(upName, 3, currentParentRect));
            // currentUpgradeList.AddRange(CreateContentsUpListWithParam(upName, 3, currentParentRect));
        }

        // child_6, contents_1_1_1, upgrade list's item
        // title, text with buttons... etc
        // var currentUpItem = currentUpgradeList[0];
        // var currentParrentRect = AddAndGetRectComp(currentUpgradeList[0]);
        Dictionary<int, List<GameObject>> currentUpgradeItemListDic = new Dictionary<int, List<GameObject>>(32);
        // List<GameObject> currentUpgradeItemList = new List<GameObject>(32);

        
        for (int i = 0; i < currentUpgradeListDic.Count; i++)
        {
            for (int k = 0; k < currentUpgradeListDic[i].Count; k++)
            {
                var currentParentRect = AddAndGetRectComp(currentUpgradeListDic[i][k]);
                var upName = "Content_" + (i + 1);
                upName += "_" + (k + 1);
                var keyValue = i * 100 + k * 1; // 0 to 100...200 + k
                currentUpgradeItemListDic.Add(keyValue, CreateContentsUpItemListWithParam(upName, 3, currentParentRect));
            }
            
            // var currentParentRect = AddAndGetRectComp(currentUpgradeListDic[i]);
            //var upName = "Content_" + (i + 1);
            //upName += "_" + (i + 1);
            //currentUpgradeItemListDic.Add(i, CreateContentsUpItemListWithParam(upName, 3, currentParentRect));
            
        }
        // child_6, contents_1_1_x, upgrade list item's text, button...etc
        // first is only text for title

        Dictionary<EPermaUpgradeTitle,string> permaUpgradeTitleLevelTexts = new Dictionary<EPermaUpgradeTitle, string>(3);
        permaUpgradeTitleLevelTexts.Add(EPermaUpgradeTitle.Basic, "Basic Each Upgrade Costs 1 point		Unused Point: ");
        permaUpgradeTitleLevelTexts.Add(EPermaUpgradeTitle.Advanced, "Advanced Each Upgrade Costs 2 points	Unused Point: ");
        permaUpgradeTitleLevelTexts.Add(EPermaUpgradeTitle.Expert, "Expert Each Upgrade Costs 3 points	Unused Point: ");
        // Expert Each Upgrade Costs 3 points	Unused Point: 

        var tempIndexForTitleText = 0;
        foreach (var item in currentUpgradeItemListDic)
        {
            if (item.Key % 100 == 3)
            {
                continue;
                
            }
            else
            {
                var tempList = item.Value;
                
                for (int i = 0; i < tempList.Count; i++)
                {
                    var currentObj = tempList[i];
                    var currentParentRect = AddAndGetRectComp(currentObj);
                    if (currentObj.name.Contains("Title"))
                    {
                        
                        // init title object and rect
                        var tempTitleTextObj = CreateDefaultGameObject("TitleText", currentParentRect);
                        var tempTitleTextRect = AddAndGetRectComp(tempTitleTextObj);
                        tempTitleTextRect.anchorMax = new Vector2(1f, 1f);
                        tempTitleTextRect.anchorMin = new Vector2(0f, 1f);
                        tempTitleTextRect.offsetMax = new Vector2(0f, 0f);
                        tempTitleTextRect.offsetMin = new Vector2(0f, -50f);
                        tempTitleTextRect.pivot = new Vector2(0.5f, 0.5f);

                        tempTitleTextRect.anchoredPosition = new Vector2(0f, -25f);
                        tempTitleTextRect.localScale = Vector2.one;

                        var tempTitleTextComp = tempTitleTextObj.AddComponent<TextMeshProUGUI>();
                        tempTitleTextComp.text = permaUpgradeTitleLevelTexts[(EPermaUpgradeTitle)tempIndexForTitleText];
                        Debug.Log("tempIndexForTitleText is " + tempIndexForTitleText);
                        tempTitleTextComp.fontSize = 30f;
                        tempTitleTextComp.color = Color.black;
                        tempTitleTextComp.alignment = TextAlignmentOptions.Center;
                        tempIndexForTitleText++;
                        if(tempIndexForTitleText % Enum.GetValues(typeof( EPermaUpgradeTitle)).Length == 0 )
                        {
                            tempIndexForTitleText = 0;
                        }
                        // if(tempIndexForTitleText)


                    }
                    else
                    {
                        // init other item which contains text and increase, decrease button...
                        CreateUpItemsWithParam(currentParentRect);
                        // upgrade description

                        // effect desc
                        // current point 0 / 0
                        // button dec
                        // button inc


                    }
                }
                
            }

        }

        // child_6 end



    }

    #endregion

    public void InitBlockingCanvas()
    {
        GameObject windowObj = new GameObject();
        windowObj.gameObject.name = "Bloking Window Canvas";
        var blockingCanvas = windowObj.AddComponent<Canvas>();
        blockingCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        blockingCanvas.sortingOrder = 2000;
        windowObj.AddComponent<CanvasScaler>();
        var currentRayCaster = windowObj.AddComponent<GraphicRaycaster>();
        currentRayCaster.blockingObjects = GraphicRaycaster.BlockingObjects.All;


        GameObject blockingImageObj = new GameObject();
        blockingImageObj.transform.SetParent(windowObj.transform);
        blockingImageObj.gameObject.name = "Blocking Image Object";
        var blockingImage = blockingImageObj.AddComponent<Image>();
        blockingImage.color = new Color(0f, 0f, 0f, 0.4f);
        var currentBackgroundRect = AddAndGetRectComp(blockingImageObj);

        currentBackgroundRect.anchorMin = new Vector2(0f, 0f);
        currentBackgroundRect.anchorMax = new Vector2(1f, 1f);
        currentBackgroundRect.pivot = new Vector2(0.5f, 0.5f);
        
        currentBackgroundRect.anchoredPosition = new Vector2(0f, 0f);


        var testSeeRect = testDebugDisplayRect;

        GameObject loadingImageObj = new GameObject();
        loadingImageObj.transform.SetParent(windowObj.transform);
        loadingImageObj.gameObject.name = "Blocking Loading Image Object";
        var loadingImage = loadingImageObj.AddComponent<Image>();
        loadingImage.sprite = loadingImageSprite;
        var loadingScript = loadingImageObj.AddComponent<Rotation>();
        loadingScript.SetRotate(Vector3.forward);
        loadingScript.SetSpeed(-100f, 0.1f);

        var currentLoadingRect = AddAndGetRectComp(loadingImageObj);
        currentLoadingRect.anchoredPosition = Vector2.zero;


    }

    #region slider UI creation
    public void InitSliderUI()
    {
        ProvideParamAndInit();
        return;
        

        var currentSample = sampleBackgroundImage;

        // Vector2 parentRect = new Vector2(255f, 0.5f);
        // Vector2 sliderRectPos = new Vector2(255f, 0.5f);

        var currentObject = new GameObject();
        currentObject.name = "createdSliderParent";
        currentObject.transform.parent = parentCanvas.transform;
        var parentRectTrans = currentObject.AddComponent<RectTransform>();
        if(parentRectTrans != null)
        {
            parentRectTrans.anchoredPosition = new Vector2(-12f, 0f);
        }
        // parentRectTrans.anchoredPosition = parentRect;

        parentRectTrans.anchorMin = new Vector2(1f, 0.5f);
        parentRectTrans.anchorMax = new Vector2(1f, 0.5f);



        var sliderObject = new GameObject();
        sliderObject.transform.parent = currentObject.transform;
        var sliderComponent = sliderObject.AddComponent<Slider>();
        sliderComponent.transition = Selectable.Transition.None;
        sliderComponent.direction = Slider.Direction.BottomToTop;
        sliderComponent.minValue = 0f;
        sliderComponent.maxValue = 1f;


        var currentSliderRect = sliderObject.GetComponent<RectTransform>();
        // currentSliderRect.anchoredPosition = sliderRectPos;
        currentSliderRect.anchoredPosition = Vector2.zero;
        currentSliderRect.sizeDelta = new Vector2(20f, 160f);
        // currentSliderRect. = new Rect(0f, 0f, 20f, 180f);
        sliderObject.name = "Slider";

        // background
        var backgroundObject = new GameObject();
        backgroundObject.transform.parent = sliderObject.transform;
        // backgroundObject.name = "Background";
        backgroundObject.name = "Fill Area";

        // var backgroundRect = backgroundObject.GetComponent<RectTransform>();
        var backgroundRect = backgroundObject.AddComponent<RectTransform>();
        backgroundRect.anchorMin = new Vector2(0.25f, 0f);
        backgroundRect.anchorMax= new Vector2(0.75f, 1f);
        backgroundRect.pivot = new Vector2(0.5f, 0.5f);
        // WaitAndCall(0.1f, () => backgroundRect.sizeDelta = new Vector2(10f, 160f));
        backgroundRect.anchoredPosition = Vector2.zero;
        // backgroundRect.sizeDelta = new Vector2(10f, 160f);
        backgroundRect.offsetMin = Vector2.zero;
        backgroundRect.offsetMax = Vector2.zero;


        var backgroundImage = backgroundObject.AddComponent<Image>();
        backgroundImage.sprite = backGroundSprite;
        backgroundImage.type = Image.Type.Sliced;
        
        var backgroundMask = backgroundObject.AddComponent<Mask>();


        // need to change background -> fill area

        var fillObject = new GameObject();
        fillObject.transform.parent = backgroundObject.transform;
        fillObject.name = "Fill";
        var fillImage = fillObject.AddComponent<Image>();
        fillImage.sprite = null;
        // fillImage.type = Image.Type.Sliced;

        var fillObjectMaskComp = fillObject.AddComponent<Mask>();
        // var fillRect = fillObject.AddComponent<RectTransform>();
        var fillRect = AddAndGetRectComp(fillObject);
        sliderComponent.fillRect = fillRect;
        fillRect.anchoredPosition = Vector2.zero;
        fillRect.offsetMin = Vector2.zero;
        fillRect.offsetMax = Vector2.zero;
        fillRect.pivot = new Vector2(0.5f, 0f);


        var fillMaskedGaugeObject = new GameObject();
        fillMaskedGaugeObject.transform.parent = fillObject.transform;
        fillMaskedGaugeObject.name = "FillMaskedGauge";

        var fillMaskedRawImageComp = fillMaskedGaugeObject.AddComponent<RawImage>();
        fillMaskedRawImageComp.texture = fillMaskedGaugeSprite;
        // fillMaskedRawImageComp.texture = fillMaskedGaugeSpriteForTwoDimention;

        var fillMaskedrect = AddAndGetRectComp(fillMaskedGaugeObject);
        fillMaskedrect.anchoredPosition = Vector2.zero;
        fillMaskedrect.sizeDelta = new Vector2(10f,200f);
        fillMaskedrect.anchorMin = new Vector2(0.5f, 0f);
        fillMaskedrect.anchorMax = new Vector2(0.5f, 0f);
        fillMaskedrect.pivot = new Vector2(0.5f, 0f);


        // test
        sliderComponent.value = 0.4f;

    }

    public void ProvideParamAndInit()
    {
        var textureFilePath = "D:\\Lab\\UnityModManaging\\ResourcesForUICreation\\Frame2.png";
        // "D:\Lab\UnityModManaging\ResourcesForUICreation\Frame1.png"
        Texture2D outTexture = null;
        HelperFunctions.CreateTextureFromFile(textureFilePath, out outTexture);

        var spriteFilePath = "D:\\Lab\\UnityModManaging\\ResourcesForUICreation\\Frame1.png";
        Texture2D outTexture_2 = null;

        Vector4 border = new Vector4(10f, 10f, 10f, 10f);

        var outSprite = HelperFunctions.CreateSpriteFromFile(spriteFilePath, ref outTexture_2, border);

        InitSliderUIFromOutside(parentCanvas, outSprite, outTexture);

    }

    public void InitSliderUIFromOutside(
            // RectTransform sampleBackgroundImage,
            Canvas parentCanvas,
            Sprite backGroundSprite,
            Texture2D fillMaskedGaugeSprite
            )
    {
        // write down UI for fill bar gauge


        // var currentSample = sampleBackgroundImage;

        // Vector2 parentRect = new Vector2(255f, 0.5f);
        // Vector2 sliderRectPos = new Vector2(255f, 0.5f);

        var currentObject = new GameObject();
        currentObject.name = "createdSliderParent";
        currentObject.transform.parent = parentCanvas.transform;
        var parentRectTrans = currentObject.AddComponent<RectTransform>();
        if (parentRectTrans != null)
        {
            parentRectTrans.anchoredPosition = new Vector2(-12f, 0f);
        }
        // parentRectTrans.anchoredPosition = parentRect;

        parentRectTrans.anchorMin = new Vector2(1f, 0.5f);
        parentRectTrans.anchorMax = new Vector2(1f, 0.5f);



        var sliderObject = new GameObject();
        sliderObject.transform.parent = currentObject.transform;
        var sliderComponent = sliderObject.AddComponent<Slider>();
        sliderComponent.transition = Selectable.Transition.None;
        sliderComponent.direction = Slider.Direction.BottomToTop;
        sliderComponent.minValue = 0f;
        sliderComponent.maxValue = 1f;


        var currentSliderRect = sliderObject.GetComponent<RectTransform>();
        // currentSliderRect.anchoredPosition = sliderRectPos;
        currentSliderRect.anchoredPosition = Vector2.zero;
        currentSliderRect.sizeDelta = new Vector2(20f, 160f);
        // currentSliderRect. = new Rect(0f, 0f, 20f, 180f);
        sliderObject.name = "Slider";

        // background
        var backgroundObject = new GameObject();
        backgroundObject.transform.parent = sliderObject.transform;
        // backgroundObject.name = "Background";
        backgroundObject.name = "Fill Area";

        // var backgroundRect = backgroundObject.GetComponent<RectTransform>();
        var backgroundRect = backgroundObject.AddComponent<RectTransform>();
        backgroundRect.anchorMin = new Vector2(0.25f, 0f);
        backgroundRect.anchorMax = new Vector2(0.75f, 1f);
        backgroundRect.pivot = new Vector2(0.5f, 0.5f);
        // WaitAndCall(0.1f, () => backgroundRect.sizeDelta = new Vector2(10f, 160f));
        backgroundRect.anchoredPosition = Vector2.zero;
        // backgroundRect.sizeDelta = new Vector2(10f, 160f);
        backgroundRect.offsetMin = Vector2.zero;
        backgroundRect.offsetMax = Vector2.zero;


        var backgroundImage = backgroundObject.AddComponent<Image>();
        backgroundImage.sprite = backGroundSprite;
        backgroundImage.type = Image.Type.Sliced;

        var backgroundMask = backgroundObject.AddComponent<Mask>();


        // need to change background -> fill area

        var fillObject = new GameObject();
        fillObject.transform.parent = backgroundObject.transform;
        fillObject.name = "Fill";
        var fillImage = fillObject.AddComponent<Image>();
        fillImage.sprite = null;
        // fillImage.type = Image.Type.Sliced;

        var fillObjectMaskComp = fillObject.AddComponent<Mask>();
        // var fillRect = fillObject.AddComponent<RectTransform>();
        var fillRect = AddAndGetRectComp(fillObject);
        sliderComponent.fillRect = fillRect;
        fillRect.anchoredPosition = Vector2.zero;
        fillRect.offsetMin = Vector2.zero;
        fillRect.offsetMax = Vector2.zero;
        fillRect.pivot = new Vector2(0.5f, 0f);


        var fillMaskedGaugeObject = new GameObject();
        fillMaskedGaugeObject.transform.parent = fillObject.transform;
        fillMaskedGaugeObject.name = "FillMaskedGauge";

        var fillMaskedRawImageComp = fillMaskedGaugeObject.AddComponent<RawImage>();
        fillMaskedRawImageComp.texture = fillMaskedGaugeSprite;

        var fillMaskedrect = AddAndGetRectComp(fillMaskedGaugeObject);
        fillMaskedrect.anchoredPosition = Vector2.zero;
        fillMaskedrect.sizeDelta = new Vector2(10f, 200f);
        fillMaskedrect.anchorMin = new Vector2(0.5f, 0f);
        fillMaskedrect.anchorMax = new Vector2(0.5f, 0f);
        fillMaskedrect.pivot = new Vector2(0.5f, 0f);


        // test
        sliderComponent.value = 0.4f;

    }

    #endregion
    public RectTransform AddAndGetRectComp(GameObject paramObject)
    {
        var result = paramObject.GetComponent<RectTransform>();
        if(result == null)
        {
            result = paramObject.AddComponent<RectTransform>();

        }

        return result;
    }

    
    public void WaitAndCall(float waitSeconds, Action callBack)
    {
        StartCoroutine(WaitAndCallRoutine(waitSeconds, callBack));
    }
    public IEnumerator WaitAndCallRoutine(float waitSeconds, Action callBack)
    {
        yield return new WaitForSeconds(waitSeconds);

        callBack?.Invoke();

    }

    public (GameObject,Canvas,CanvasScaler,GraphicRaycaster) CreateDefaultCanvas(string objectName, int sortingOrder, bool bIsBlockingCavnas)
    {
        // var result
        // (GameObject, Canvas, CanvasScaler, GraphicRaycaster) result = 
        GameObject canvasObj = new GameObject();
        canvasObj.gameObject.name = objectName;
        var createdCanvas = canvasObj.AddComponent<Canvas>();
        createdCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        createdCanvas.sortingOrder = sortingOrder;
        var currentScaler = canvasObj.AddComponent<CanvasScaler>();
        var currentRayCaster = canvasObj.AddComponent<GraphicRaycaster>();
        currentRayCaster.blockingObjects = GraphicRaycaster.BlockingObjects.All;
        (GameObject, Canvas, CanvasScaler, GraphicRaycaster) result = (canvasObj, createdCanvas, currentScaler, currentRayCaster);
        return result;
    }

    public GameObject CreateDefaultGameObject(string name, Transform parent = null)
    {
        GameObject result = new GameObject(name);
        if(parent != null)
        {
            result.transform.SetParent(parent);
        }
        return result;
    }

    public List<GameObject> CreateButtonsWithParam(string name, int iterationCount, Transform parent = null)
    {
        List<GameObject> buttonList = new List<GameObject>(iterationCount);

        for (int i = 0; i < iterationCount; i++)
        {
            // child_5 tab 1 breeding
            GameObject tabButtonObj = CreateDefaultGameObject("Tab_" + (i + 1) + "", parent);
            var tabButtonRect = AddAndGetRectComp(tabButtonObj);
            tabButtonRect.sizeDelta = new Vector2(235f, 80f);
            tabButtonRect.localScale = Vector2.one;

            var tabImage = tabButtonObj.AddComponent<Image>();
            tabImage.sprite = tabButtonSprite;
            tabImage.type = Image.Type.Sliced;
            var tabButton = tabButtonObj.AddComponent<Button>();
            tabButton.targetGraphic = tabImage;
            buttonList.Add(tabButtonObj);
        }
        
        return buttonList;
    }

    public List<GameObject> CreateContentsWithParam(string name, int iterationCount, Transform parent = null)
    {
        List<GameObject> contentList = new List<GameObject>(iterationCount);

        for (int i = 0; i < iterationCount; i++)
        {
            // child_4, contents_1, first content
            GameObject currentContentObj = CreateDefaultGameObject(name + (i + 1) + "", parent);
            // GameObject tabButtonObj = CreateDefaultGameObject("Tab_" + (i + 1) + "", parent);
            var currentContentRect = AddAndGetRectComp(currentContentObj);
            currentContentRect.anchorMax = new Vector2(1f, 1f);
            currentContentRect.anchorMin = new Vector2(0f, 0f);
            currentContentRect.offsetMax = new Vector2(0f, 0f);
            currentContentRect.offsetMin = new Vector2(0f, 0f);
            currentContentRect.pivot = new Vector2(0.5f, 0.5f);
            currentContentRect.sizeDelta = new Vector2(0f, 0f);
            currentContentRect.anchoredPosition = new Vector2(0f, 0f); // always adjust anchoredPosition at last step
            currentContentRect.localScale = Vector2.one;

            var currentLayoutGroup = currentContentObj.AddComponent<VerticalLayoutGroup>();
            currentLayoutGroup.childAlignment = TextAnchor.UpperCenter;
            currentLayoutGroup.padding.top = 10;
            currentLayoutGroup.spacing = 10f;

            currentLayoutGroup.childControlWidth = false;
            currentLayoutGroup.childControlHeight = false;
            currentLayoutGroup.childForceExpandWidth = true;
            currentLayoutGroup.childForceExpandHeight = false;

            contentList.Add(currentContentObj);
        }

        return contentList;
    }

    public List<GameObject> CreateContentsUpListWithParam(string name, int iterationCount, Transform parent = null)
    {
        List<GameObject> contentUpList = new List<GameObject>(iterationCount);

        for (int i = 0; i < iterationCount; i++)
        {
            // child_5, contents_1, upgrade lists
            
            GameObject currentUpgradeObj = CreateDefaultGameObject(name + "_" + (i + 1) + "", parent);
            // GameObject tabButtonObj = CreateDefaultGameObject("Tab_" + (i + 1) + "", parent);
            var currentUpgradeRect = AddAndGetRectComp(currentUpgradeObj);
            currentUpgradeRect.anchorMax = new Vector2(0f, 0f);
            currentUpgradeRect.anchorMin = new Vector2(0f, 0f);
            currentUpgradeRect.offsetMax = new Vector2(542f, 100f);
            currentUpgradeRect.offsetMin = new Vector2(-542f, -100f);
            currentUpgradeRect.pivot = new Vector2(0.5f, 0.5f);
            // currentUpgradeRect.sizeDelta = new Vector2(0f, 0f);
            // currentUpgradeRect.anchoredPosition = new Vector2(0f, 0f); // always adjust anchoredPosition at last step
            currentUpgradeRect.localScale = Vector2.one;

            var currentUpgradeRectLayoutGroup = currentUpgradeObj.AddComponent<VerticalLayoutGroup>();
            currentUpgradeRectLayoutGroup.childAlignment = TextAnchor.UpperCenter;
            // currentUpgradeRectLayoutGroup.padding.top = 10;
            currentUpgradeRectLayoutGroup.spacing = 10f;
            currentUpgradeRectLayoutGroup.childControlWidth = false;
            currentUpgradeRectLayoutGroup.childControlHeight = false;
            currentUpgradeRectLayoutGroup.childForceExpandWidth = true;
            currentUpgradeRectLayoutGroup.childForceExpandHeight = false;

            contentUpList.Add(currentUpgradeObj);
        }

        return contentUpList;
    }

    public List<GameObject> CreateContentsUpItemListWithParam(string name, int iterationCount, Transform parent = null)
    {
        List<GameObject> contentUpItemList = new List<GameObject>(iterationCount);

        for (int i = 0; i < iterationCount; i++)
        {
            // child_5, contents_1, upgrade lists

            // var currentUpItem = currentUpgradeList[0];
            // var currentParrentRect = AddAndGetRectComp(currentUpgradeList[0]);
            string itemName = name;
            if(i==0)
            {
                itemName += "_Title";
            }
            else
            {
                itemName += "_UpItem" + i;
            }
            GameObject currentUpItemObj = CreateDefaultGameObject(itemName, parent);
            // GameObject tabButtonObj = CreateDefaultGameObject("Tab_" + (i + 1) + "", parent);
            var currentUpItemRect = AddAndGetRectComp(currentUpItemObj);
            currentUpItemRect.anchorMax = new Vector2(0f, 0f);
            currentUpItemRect.anchorMin = new Vector2(0f, 0f);
            currentUpItemRect.offsetMax = new Vector2(542f, 25f);
            currentUpItemRect.offsetMin = new Vector2(-542f, -25f);
            currentUpItemRect.pivot = new Vector2(0.5f, 0.5f);
            // currentUpgradeRect.sizeDelta = new Vector2(0f, 0f);
            // currentUpgradeRect.anchoredPosition = new Vector2(0f, 0f); // always adjust anchoredPosition at last step
            currentUpItemRect.localScale = Vector2.one;

            var currentUpItemImage = currentUpItemObj.AddComponent<Image>();
            currentUpItemImage.raycastTarget = false;
            currentUpItemImage.type = Image.Type.Sliced;
            currentUpItemImage.sprite = upItemBackSprite;

            contentUpItemList.Add(currentUpItemObj);
        }

        return contentUpItemList;
    }

    public PermaUpgradeUIItemInfo CreateUpItemsWithParam(Transform parent = null)
    {
        PermaUpgradeUIItemInfo result = new PermaUpgradeUIItemInfo();
        // upgrade description
        GameObject currentUpgradeDescObj = CreateDefaultGameObject("UpgradeDesc", parent);
        var currentUpgradeDescRect = AddAndGetRectComp(currentUpgradeDescObj);
        currentUpgradeDescRect.anchorMax = new Vector2(0f, 0.5f);
        currentUpgradeDescRect.anchorMin = new Vector2(0f, 0.5f);
        currentUpgradeDescRect.offsetMax = new Vector2(320f, 25f);
        currentUpgradeDescRect.offsetMin = new Vector2(20f, -25f);
        currentUpgradeDescRect.pivot = new Vector2(0f, 0.5f);
        currentUpgradeDescRect.anchoredPosition = new Vector2(20f, 0f); // always adjust anchoredPosition at last step
        currentUpgradeDescRect.localScale = Vector2.one;

        // var currentUpgradeDescText = currentUpgradeDescObj.AddComponent<TextMeshProUGUI>();
        var currentUpgradeDescText = AddTextMeshProUGUIToUpItems(ref currentUpgradeDescObj);

        // effect desc
        GameObject currentEffectDescObj = CreateDefaultGameObject("UpgradeEffect", parent);
        var currentEffectDescRect = AddAndGetRectComp(currentEffectDescObj);
        currentEffectDescRect.anchorMax = new Vector2(0f, 0.5f);
        currentEffectDescRect.anchorMin = new Vector2(0f, 0.5f);
        currentEffectDescRect.offsetMax = new Vector2(630f, 25f);
        currentEffectDescRect.offsetMin = new Vector2(380f, -25f);
        currentEffectDescRect.pivot = new Vector2(0f, 0.5f);
        currentEffectDescRect.anchoredPosition = new Vector2(380f, 0f); // always adjust anchoredPosition at last step
        currentEffectDescRect.localScale = Vector2.one;

        var currentEffectDescText = AddTextMeshProUGUIToUpItems(ref currentEffectDescObj);

        // current point 0 / 0
        GameObject currentPointDescObj = CreateDefaultGameObject("UpgradePoint", parent);
        var currentPointDescRect = AddAndGetRectComp(currentPointDescObj);
        currentPointDescRect.anchorMax = new Vector2(0f, 0.5f);
        currentPointDescRect.anchorMin = new Vector2(0f, 0.5f);
        currentPointDescRect.offsetMax = new Vector2(870f, 25f);
        currentPointDescRect.offsetMin = new Vector2(720f, -25f);
        currentPointDescRect.pivot = new Vector2(0f, 0.5f);
        currentPointDescRect.anchoredPosition = new Vector2(720f, 0f); // always adjust anchoredPosition at last step
        currentPointDescRect.localScale = Vector2.one;

        var currentPointDescText = AddTextMeshProUGUIToUpItems(ref currentPointDescObj);
        // button dec
        GameObject currentDecButtonObj = CreateDefaultGameObject("DecreaseButton", parent);
        var currentDecButtonRect = AddAndGetRectComp(currentDecButtonObj);
        currentDecButtonRect.anchorMax = new Vector2(0f, 0.5f);
        currentDecButtonRect.anchorMin = new Vector2(0f, 0.5f);
        currentDecButtonRect.offsetMax = new Vector2(960f, 25f);
        currentDecButtonRect.offsetMin = new Vector2(910f, -25f);
        currentDecButtonRect.pivot = new Vector2(0f, 0.5f);
        currentDecButtonRect.anchoredPosition = new Vector2(910f, 0f); // always adjust anchoredPosition at last step
        currentDecButtonRect.localScale = Vector2.one;

        Image currentDecButtonImage = currentDecButtonObj.AddComponent<Image>();
        currentDecButtonImage.sprite = IncDecButtonSprite;
        
        Button currentDecButtonComp = currentDecButtonObj.AddComponent<Button>();
        // currentDecButtonComp.colors.disabledColor = new Color(0.5f, 0.5f, 0.5f, 1f);
        currentDecButtonComp.colors = GetOnlyDisabledColor(currentDecButtonComp.colors, new Color(0.5f, 0.5f, 0.5f, 1f));


        var currentDecButtonTextObj = CreateDefaultGameObject("Text", currentDecButtonRect);
        var currentDecButtonText = currentDecButtonTextObj.AddComponent<TextMeshProUGUI>();
        currentDecButtonText.alignment = TextAlignmentOptions.Center;
        currentDecButtonText.fontSize = 55;
        currentDecButtonText.color = new Color(0.2f, 0.2f, 0.2f);
        currentDecButtonText.text = "-";

        var currentDecButtonTextRect = AddAndGetRectComp(currentDecButtonTextObj);
        currentDecButtonTextRect.anchorMax = new Vector2(1f, 1f);
        currentDecButtonTextRect.anchorMin = new Vector2(0f, 0f);
        currentDecButtonTextRect.offsetMax = new Vector2(0f, 0f);
        currentDecButtonTextRect.offsetMin = new Vector2(0f, 0f);
        currentDecButtonTextRect.pivot = new Vector2(0.5f, 0.5f);
        // currentDecButtonTextRect.anchoredPosition = new Vector2(970f, 0f); // always adjust anchoredPosition at last step
        currentDecButtonTextRect.localScale = Vector2.one;

        // button inc
        GameObject currentIncButtonObj = CreateDefaultGameObject("IncreaseButton", parent);
        var currentIncButtonRect = AddAndGetRectComp(currentIncButtonObj);
        currentIncButtonRect.anchorMax = new Vector2(0f, 0.5f);
        currentIncButtonRect.anchorMin = new Vector2(0f, 0.5f);
        currentIncButtonRect.offsetMax = new Vector2(1020f, 25f);
        currentIncButtonRect.offsetMin = new Vector2(970f, -25f);
        currentIncButtonRect.pivot = new Vector2(0f, 0.5f);
        currentIncButtonRect.anchoredPosition = new Vector2(970f, 0f); // always adjust anchoredPosition at last step
        currentIncButtonRect.localScale = Vector2.one;

        Image currentIncButtonImage = currentIncButtonObj.AddComponent<Image>();
        currentIncButtonImage.sprite = IncDecButtonSprite;

        Button currentIncButtonComp = currentIncButtonObj.AddComponent<Button>();
        // currentDecButtonComp.colors.disabledColor = new Color(0.5f, 0.5f, 0.5f, 1f);
        currentIncButtonComp.colors = GetOnlyDisabledColor(currentIncButtonComp.colors, new Color(0.5f, 0.5f, 0.5f, 1f));


        var currentIncButtonTextObj = CreateDefaultGameObject("Text", currentIncButtonRect);
        var currentIncButtonText = currentIncButtonTextObj.AddComponent<TextMeshProUGUI>();
        currentIncButtonText.alignment = TextAlignmentOptions.Center;
        currentIncButtonText.fontSize = 55;
        currentIncButtonText.color = new Color(0.2f, 0.2f, 0.2f);
        currentIncButtonText.text = "+";

        var currentIncButtonTextRect = AddAndGetRectComp(currentIncButtonTextObj);
        currentIncButtonTextRect.anchorMax = new Vector2(1f, 1f);
        currentIncButtonTextRect.anchorMin = new Vector2(0f, 0f);
        currentIncButtonTextRect.offsetMax = new Vector2(0f, 0f);
        currentIncButtonTextRect.offsetMin = new Vector2(0f, 0f);
        currentIncButtonTextRect.pivot = new Vector2(0.5f, 0.5f);
        // currentDecButtonTextRect.anchoredPosition = new Vector2(970f, 0f); // always adjust anchoredPosition at last step
        currentIncButtonTextRect.localScale = Vector2.one;


        result.upgradeDesc = currentUpgradeDescText;
        result.upgradeEffect = currentEffectDescText;
        result.upgradePoint = currentPointDescText;
        result.decButton = currentDecButtonComp;
        result.incButton = currentIncButtonComp;


        return result;
    }

    public void SetRectWithParam(
        Vector2 anchMax,
        Vector2 anchMin,
        Vector2 offsetMax,
        Vector2 offsetMin,
        Vector2 pivot,
        Vector2 anchPos,
        ref RectTransform paramRect
        )
    {
        paramRect.anchorMax = anchMax;
        paramRect.anchorMin = anchMin;
        paramRect.offsetMax = offsetMax;
        paramRect.offsetMin = offsetMin;
        paramRect.pivot = pivot;
        paramRect.anchoredPosition = anchPos;
    }

    public TextMeshProUGUI AddTextMeshProUGUIToUpItems(ref GameObject paramObj)
    {
        var resultText = paramObj.AddComponent<TextMeshProUGUI>();
        resultText.fontSize = 30f;
        resultText.color = Color.black;
        resultText.alignment = TextAlignmentOptions.Center;
        resultText.enableAutoSizing = true;
        resultText.fontSizeMin = 20;
        resultText.fontSizeMax = 26;
        // resultText.enable = false;
        return resultText;
    }

    public ColorBlock GetOnlyDisabledColor(ColorBlock originBlock, Color paramDisabledColor)
    {
        ColorBlock result = originBlock;
        result.disabledColor = paramDisabledColor;
        return result;
    }

    #endregion



    [TestMethod(false)]
    public void GetTempCorruptPoint()
    {
        var tempGet = EventChannelContainer.Instance.Get(typeof(CorruptionPointEventChannel));
        Debug.Log("");
    }

    [TestMethod(false)]
    public void GetTestForTypeConversion()
    {
        EventChannelBase newType = new EventChannelBase();
        TestTypeConvert<EventChannelBase>(newType);
    }

    public void TestTypeConvert<TypeT>(TypeT paramType)
    {
        var currentObj = paramType;
        Debug.Log("[TestTypeConvert]");
    }

    public void OnTestButtonClicked()
    {
        Debug.Log("[OnTestButtonClicked]");
    }

    public void OnTestArrowButtonClicked()
    {
        Debug.Log("[OnTestArrowButtonClicked]");
    }
    public IEnumerator Wait(float waitSec, Action executeAfterDelay)
    {
        yield return new WaitForSeconds(waitSec);
        executeAfterDelay.Invoke();
    }

    

}

public class PermaUpgradeUIItemInfo
{
    public TextMeshProUGUI upgradeDesc;
    public TextMeshProUGUI upgradeEffect;
    public TextMeshProUGUI upgradePoint;
    public Button decButton;
    public Button incButton;
        
}