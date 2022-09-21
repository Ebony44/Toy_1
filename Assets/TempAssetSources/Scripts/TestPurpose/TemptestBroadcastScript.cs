using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemptestBroadcastScript : MonoBehaviour
{

    [SerializeField] VoidEventChannelSO _onTestEventCalled;
    [SerializeField] VoidEventChannelSO _onSceneLoad;

    [TestMethod(false)]
    public void OnSceneLoad()
    {
        _onSceneLoad.RaiseEvent();
    }

    [TestMethod(false)]
    public void OnTestEventCalled()
    {
        _onTestEventCalled.RaiseEvent();
    }


}
