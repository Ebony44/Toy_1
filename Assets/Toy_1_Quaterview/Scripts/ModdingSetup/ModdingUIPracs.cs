using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    #region create UIs
    public void Init()
    {
        var currentObject = new GameObject();
        currentObject.name = "createdSliderParent";
        currentObject.transform.parent = parentCanvas.transform;
        currentObject.AddComponent<RectTransform>();
    }

    private void Start()
    {
        Init();
    }


    #endregion


}
