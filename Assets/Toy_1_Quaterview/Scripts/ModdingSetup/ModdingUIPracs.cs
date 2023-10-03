using System;
using System.Collections;
using System.Collections.Generic;
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
    public Sprite loadingImageSprite;


    #region create UIs

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

    private void Start()
    {
        // InitSliderUI();
        InitBlockingCanvas();
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
