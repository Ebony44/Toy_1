using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTestChannelContainer : MonoBehaviour
{
    public static TempTestChannelContainer Instance { get; private set; }

    public CorruptionPointEventChannel corruptionPointEventChannel;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        Init();

    }
    public void Init()
    {
        // broadcaster
        // use channel as 
        // ex)         //corruptionPointEventChannel.RaiseEvent(value: 1);

        // listener
        // assing into channel as
        // ex 1) _onCorruptionPointChanged.OnEventRaised += EvokeOnChangedEvent;
        // ex 2) if (_onCorruptionPointChanged != null)
        //{
        //    _onCorruptionPointChanged.OnEventRaised += EvokeOnChangedEvent;
        //}

        corruptionPointEventChannel = new CorruptionPointEventChannel();

    }

    [TestMethod(false)]
    public void TestShow()
    {
        Debug.Log("asdf");
    }
}
