using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTestListner : MonoBehaviour
{
    // when assign, an instance is not an consideration...
    // for practice scene is \Assets\Scenes there
    [SerializeField] VoidEventChannelSO _onTestEventCalledSO;
    
    CorruptionPointEventChannel _onCorruptionPointChanged;

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
    public void EvokeOnChangedEvent(float paramValue)
    {
        Debug.Log("Test event called, with float argument, value is " + paramValue);
    }

    //[TestMethod(false)]
    //public void ShowVectorFromAngle(float angle)
    //{
    //    var tempVector = CodeMonkey.Utils.UtilsClass.GetVectorFromAngle((int)angle);
    //    Debug.Log("vector is " + tempVector);
    //}

    [TestMethod(false)]
    public void CreateCorruptPoint()
    {
        
        //if (_onCorruptionPointChanged == null)
        //{
        //    _onCorruptionPointChanged = new CorruptionPointEventChannel();
        //    // _onCorruptionPointChanged = Instantiate(new CorruptionPointEventChannel());
        //}
        Debug.Log("[CreateCorruptPoint]");
        _onCorruptionPointChanged = TempTestChannelContainer.Instance.corruptionPointEventChannel;
    }
    [TestMethod(false)]
    public void AssignCorruptPointChangedEvent()
    {
        
        if (_onCorruptionPointChanged != null)
        {
            _onCorruptionPointChanged.OnEventRaised += EvokeOnChangedEvent;
        }
        Debug.Log("[AssignCorruptPointChangedEvent]");
    }

}
