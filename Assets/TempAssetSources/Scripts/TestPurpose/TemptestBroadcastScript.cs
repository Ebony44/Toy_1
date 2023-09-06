using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemptestBroadcastScript : MonoBehaviour
{

    [SerializeField] VoidEventChannelSO _onTestEventCalled;
    [SerializeField] VoidEventChannelSO _onSceneLoad;

    CorruptionPointEventChannel _onCorruptionPointChanged;

    public GameObject containerObject;

    private void Awake()
    {
        _onCorruptionPointChanged = new CorruptionPointEventChannel();
        var temp = containerObject.AddComponent<TempTestChannelContainer>();
    }
    public CorruptionPointEventChannel GetBroadCasterCorruptionPointChannel()
    {
        return _onCorruptionPointChanged;
    }

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

    [TestMethod(false)]
    public void OnCorruptionPointChanged(float changeValue)
    {
        if(_onCorruptionPointChanged == null)
        {
            // _onCorruptionPointChanged = TempTestChannelContainer.Instance.CorruptionPointEventChannel;
            // _onCorruptionPointChanged = new CorruptionPointEventChannel();
        }
        _onCorruptionPointChanged = TempTestChannelContainer.Instance.corruptionPointEventChannel;
        _onCorruptionPointChanged.RaiseEvent(changeValue);
    }


}
