using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EInGameResources
{
    Blood = 0,
    Bone,
    Flesh,
}

public class IngameManager : MonoBehaviour
{
    
    // currently blood, flesh and bone
    public Dictionary<EInGameResources, float> currentResources = new Dictionary<EInGameResources, float>(3);

    public Dictionary<EInGameResources, float> currentMaxLimitResources = new Dictionary<EInGameResources, float>(3);
    public readonly Dictionary<EInGameResources, float> FixedMaxLimitResources = new Dictionary<EInGameResources, float>(3);
    // public currentListenEvent 

    // use it as listener
    // so no need to reference other components
    public List<IngameResourceChannelSO> resourceEventChannels = new List<IngameResourceChannelSO>();


    private void Start()
    {
        Initialize();
    }
    private void OnEnable()
    {
        foreach (var item in resourceEventChannels)
        {
            item.OnEventRaised += OnResourceChanged;
            
        }
    }
    private void OnDisable()
    {
        foreach (var item in resourceEventChannels)
        {
            item.OnEventRaised -= OnResourceChanged;
        }
    }
    [TestMethod(false)]
    public void TestBreakPoint()
    {
        var tempCurrent = currentResources[EInGameResources.Blood];

    }

    public void OnResourceChanged(EInGameResources resourceType,float settingValue)
    {
        Debug.Log(" current resource type " + resourceType
            + " before value " + currentResources[resourceType]
            + " after value " + (currentResources[resourceType] + settingValue) );
        currentResources[resourceType] += settingValue;
        
    }

    public void Initialize()
    {
        var currentItems = System.Enum.GetValues(typeof(EInGameResources));
        foreach (EInGameResources item in currentItems)
        {
            currentResources.Add(item, 0);
        }
        
    }

    public void UpdateResources(EInGameResources targetResource, float settingValue)
    {
        
    }

    public void GameClear()
    {
        // reset max and current
        foreach (var item in currentMaxLimitResources)
        {
            var currentFixedLimitValue = FixedMaxLimitResources[item.Key];
            // item.Value = currentFixedLimitValue;
            currentMaxLimitResources[item.Key] = currentFixedLimitValue;

            currentResources[item.Key] = 0;

        }
        // clear other features

    }

    

}


