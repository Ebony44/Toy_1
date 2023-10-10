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

        var currentLayoutGroup = currentTabLayoutObj.AddComponent<HorizontalLayoutGroup>();
        currentLayoutGroup.childAlignment = TextAnchor.MiddleLeft;
        currentLayoutGroup.childForceExpandWidth = true;
        currentLayoutGroup.childControlWidth = true;


        // currentTabRect
        // currentTabs
        // child_3, contents






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
