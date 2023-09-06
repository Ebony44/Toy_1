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
        corruptionPointEventChannel = new CorruptionPointEventChannel();
    }

    [TestMethod(false)]
    public void TestShow()
    {
        Debug.Log("asdf");
    }
}
