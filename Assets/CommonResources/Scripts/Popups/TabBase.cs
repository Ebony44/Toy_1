using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 뭔가 함 OnUpdate()를 호출하면 각 버튼들 상태에 따라 크기랑 위치를 보간해주는 함수.
/// </summary>
/// 
[Serializable]
public class OnTabEvent : UnityEvent<int>{}

public class TabBase : MonoBehaviour {
    public float initialSpace = 0f;
    public Vector2 unActiveSize = new Vector2 (167f, 57f);
    public Vector2 activeSize = new Vector2 (150f, 45f);
    public float space = 5f;

    public float time = 0.2f;

    public List<Button> tabList;

    public OnTabEvent onTabEvent;

    private int m_currentIdx = 0;

    private bool isPlaying = false;

    private void Start () {
        // Setup (0);
    }

    //[TestMethod] //상속에 상속받은 컴포넌트는 안되네
    //public void Setup (int idx) {
    //    m_currentIdx = idx;
    //    var pos = space+initialSpace;
    //    for (int i = 0; i < tabList.Count; i++) {
    //        var isActive = idx != i;
    //        var rect = tabList[i].GetComponent<RectTransform> ();
    //        var tc = tabList[i].GetComponent<TabChild> ();

    //        var acSize = (tc == null?activeSize : tc.activeSize);
    //        var uasSize = tc == null?unActiveSize : tc.unActiveSize;

    //        tabList[i].interactable = isActive;
    //        rect.sizeDelta = isActive ? acSize : uasSize;
    //        rect.anchoredPosition = new Vector2 (pos, 0f);
    //        pos += rect.sizeDelta.x + space;
    //    }
    //}

    public void OnTabClicked (Button button) {
        if (isPlaying)
            return;

        // SoundManager.Instance.PlaySound("Click");

        isPlaying = true;
        if (!tabList.Contains (button))
            throw new System.Exception ("없는 버튼을 이벤트로 넘김. name : " + button.gameObject.name);

        var idx = tabList.IndexOf (button);
        // StartCoroutine (Animation (idx));
    }
    public virtual void OnClickedStartEvent (int idx) { }
    public virtual void OnClickedEndEvent (int idx) { }

    //IEnumerator Animation (int idx) {
    //    if (idx == m_currentIdx) //혹시나 같으면 할필요 없음 ㅇㅇ;
    //        yield break;

    //    OnClickedStartEvent (idx);
    //    m_currentIdx = idx;
    //    var pos = space+initialSpace;
    //    Coroutine co = null;
    //    for (int i = 0; i < tabList.Count; i++) {
    //        var isActive = idx != i;
    //        var rect = tabList[i].GetComponent<RectTransform> ();
    //        var btn = tabList[i];
    //        var tc = tabList[i].GetComponent<TabChild> ();

    //        var acSize = (tc == null?activeSize : tc.activeSize);
    //        var uasSize = tc == null?unActiveSize : tc.unActiveSize;

    //        var endSize = isActive ? acSize : uasSize;
    //        btn.interactable = isActive;
    //        btn.ChangeUISize (endSize, time);
    //        co = btn.MoveUI (new Vector2 (pos, 0f), time);
    //        pos += endSize.x + space;
    //    }
    //    yield return co;
    //    isPlaying = false;
    //    OnClickedEndEvent (m_currentIdx);
    //    onTabEvent.Invoke(m_currentIdx);
    //}

}