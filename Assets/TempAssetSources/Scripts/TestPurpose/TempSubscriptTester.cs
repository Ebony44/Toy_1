using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSubscriptTester : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO _onSceneLoadSO;
    [SerializeField] FieldOfView fow;

    [TestMethod(false)]
    public void TestFunction()
    {

        // pos.x = cardDistance * Mathf.Sin(pos.z * Mathf.Deg2Rad);
        // pos.y = cardDistance * Mathf.Cos(pos.z * Mathf.Deg2Rad);

        //transform.rotation.SetLookRotation(transform.position + Vector3.right);
        // Mathf.Atan2()

        var temp = Mathf.Atan(45f);
        Debug.Log("temp is " + temp
            + " angle " + temp * Mathf.Rad2Deg);

        var temp2 = Mathf.Atan2(1, 1);

        Debug.Log("temp2 is " + temp2
            + " angle " + temp2 * Mathf.Rad2Deg);
    }

    [TestMethod(false)]
    public void TestLookRot(float angle)
    {
        // Quaternion.LookRotation

        Debug.Log("angle is " + angle
            + " that angle of sin  " + Mathf.Sin(angle * Mathf.Deg2Rad)
            + " that angle of cos  " + Mathf.Cos(angle * Mathf.Deg2Rad));

        // GUI.Box()

    }

    public Texture tempImage;
    private void OnGUI()
    {
        //for (int i = 0; i < 28; i++)
        //{
        //    var tempRad = 30f + (5f * i);
        //    var tempX = Mathf.Sin(tempRad) * 60f + 125f;
        //    var tempY = Mathf.Cos(tempRad) * 60f + 125f;
        //    // var tempY = 10f;

        //    var tempRect = new Rect(new Vector2(tempX, tempY), Vector2.one * 25f);
        //    if (tempImage != null)
        //    {
        //        GUI.Box(tempRect, tempImage);
        //    }
        //}

        // var tempRect = new Rect(new Vector2(10, 10), Vector2.one * 50f);

    }


    [TestMethod(false)]
    public void TestAssign()
    {
        Debug.Log("scene load Event assign");
        _onSceneLoadSO.OnEventRaised += EvokeEvent;
    }
    [TestMethod(false)]
    public void TestDismiss()
    {
        Debug.Log("scene load Event dismiss");
        _onSceneLoadSO.OnEventRaised -= EvokeEvent;
    }
    public void EvokeEvent()
    {
        Debug.Log("scene load Event called");
    }
    [TestMethod(false)]
    public void ShowAimDirectionTest()
    {
        Debug.Log("angle is " + fow.targetDirection);
    }


}
