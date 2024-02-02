using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIngameManagerBroadCast : MonoBehaviour
{
    public EInGameResources testResourceType;
    public IngameResourceChannelSO broadcastSO;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    [TestMethod(false)]
    public void TestEventInvoke(float testValue)
    {
        broadcastSO.RaiseResourceSpentEvent(testResourceType,testValue);
        Debug.Log("[TestEventInvoke], value is " + testValue);
    }
}
