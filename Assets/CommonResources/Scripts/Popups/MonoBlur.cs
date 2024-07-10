using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonoBlur : MonoBehaviour// MonoSingleton<MonoBlur>
{
    public static MonoBlur Instance;
    public Material material;
    private readonly float sizeMaxValue = 2f;
    public Image image;
    public bool isUse = false;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [TestMethod]
    public Coroutine Blur(float b, float time){
        if(!isUse)
            return null;
        StopAllCoroutines();
        return StartCoroutine(Tween(b,time));
    }

    IEnumerator Tween(float b, float time){
        image.enabled = true;
        var t = 0f;
        var start = material.GetFloat("_Size");
        b = sizeMaxValue * b;
        while(t < 1f){
            t+=Time.deltaTime/time;
            material.SetFloat("_Size", Mathf.Lerp(start,b,t));
            yield return null;
        }
        if(b == 0f){
            image.enabled = false;
        }
    }

    private void OnDisable() {
        ///git에 수정 자꾸 뜨는거 방지
         material.SetFloat("_Size", 0f);
    }
}
