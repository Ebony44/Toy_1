using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChannelContainer : MonoBehaviour
{
    public static EventChannelContainer Instance { get; private set; }

    // public Dictionary<string, System.Object> eventChannelDic = new Dictionary<string, System.Object>();
    //public Dictionary<string, Dictionary<Type, System.Object>> eventChannelDic 
    //    = new Dictionary<string, Dictionary<Type, System.Object>>();

    public Dictionary<Type, System.Object> eventChannelDic = new Dictionary<Type, System.Object>();

    public CorruptionPointEventChannel corruptionPointEventChannel;
    public bool bInitialized = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Init();

    }
    //private void OnEnable()
    //{

    //}
    public void Init()
    {
        corruptionPointEventChannel = new CorruptionPointEventChannel();
        eventChannelDic.Add(corruptionPointEventChannel.GetType(), corruptionPointEventChannel);

        bInitialized = true;

    }

    public System.Object Get(Type paramType)
    {
        System.Object tempResult = null;
        if (eventChannelDic.ContainsKey(paramType))
        {
            tempResult = eventChannelDic[paramType];
        }

        return tempResult;
    }


}
