using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTestListner : MonoBehaviour
{
    // when assign, an instance is not an consideration...
    // for practice scene is \Assets\Scenes there
    [SerializeField] VoidEventChannelSO _onTestEventCalledSO;
    [TestMethod(false)]
    public void TestAssign()
    {
        Debug.Log("Test event assign");
        _onTestEventCalledSO.OnEventRaised += EvokeEvent;
    }
    [TestMethod(false)]
    public void TestDismiss()
    {
        Debug.Log("Test event dismiss");
        _onTestEventCalledSO.OnEventRaised -= EvokeEvent;
    }
    public void EvokeEvent()
    {
        Debug.Log("Test event called");
    }
}
