using OneLine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
// using OneLine;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 규칙
/// 1. 패널이름은 Panel{이름}으로한다.(구:Panel_{이름})
/// 2. 오픈할때 받은 패킷을 Initialize함수로 SendMessage로 보냄
///     예를들어 PanelShop은 R_00_SHOW_SHOP02를 받아 초기화를 함으로
///     Initialize(R_00_SHOW_SHOP02 info) 함수가 미리 선언되어 있어야함.
/// 
/// 3. 오픈 방식은 PopupManager가 결정.
/// </summary>

[Serializable]
public class PopupInfo {
    public string name;
    public GameObject prefab;
    public bool isUnique;
    public bool isLock;
    public bool openAnimation = true;
    public RectTransform closePos;
}

/// <summary>
/// B->블러 
/// P->팝업
/// F->음영
/// 
/// 블러 -> 처음들어가면 맨뒤에 무조건 하나 깔리고 팝업이 다 없어질때 없어짐
///         BPPPPP요런식으로
/// 
/// 페이드 -> 무조건 최상위 팝업의 바로아래 위치하도록
/// 
/// </summary>

public class PopupManager : MonoBehaviour// MonoSingleton<PopupManager> 
{

    [OneLine][HideLabel]
    public List<PopupInfo> popupList;

    // [OneLine][HideLabel][Header ("너무많아서 길드만 분리")]
    public List<PopupInfo> guildPopup;

    private Dictionary<string, PopupInfo> m_memPopup = new Dictionary<string, PopupInfo> ();
    private List<Tuple<Order, GameObject>> m_popupStack = new List<Tuple<Order, GameObject>> ();
    private Queue<Order> orderQueue = new Queue<Order> ();

    private bool isPlaying = false;
    public bool IsPlaying {
        get { return isPlaying; }
        set {
            isPlaying = value;
        }
    }
    public float openScaleTime = 0.1f;

    /// <summary>
    /// 나중에 팝업 별로 데이터 넣어서 바꿀것.
    /// </summary>
    /// 

    public int ActiveCount {
        get {
            return m_popupStack.Count;
        }
    }
    public string ActiveTopName {
        get {
            if (m_popupStack.Count == 0)
                return "nothing";
            return m_popupStack.Last ().Item1.id;
        }
    }
    public GameObject ActiveTopGameObject {
        get {
            if (m_popupStack.Count == 0)
                return null;
            return m_popupStack.Last ().Item2;
        }
    }
    void Start () {
        foreach (var item in popupList)
            m_memPopup.Add (item.name, item);

        foreach (var item in guildPopup)
            m_memPopup.Add (item.name, item);
    }

    public void Open (string id, object arg, string tag = null, bool block = false) {

        orderQueue.Enqueue (new Order (id, arg, tag, block));
        if (isPlaying)
            return;

        isPlaying = true;
        StartCoroutine (OrderProcess ());
    }

    public void Open (string id) {
        orderQueue.Enqueue (new Order (id, null, null, false));
        if (isPlaying)
            return;

        isPlaying = true;
        StartCoroutine (OrderProcess ());
    }
    public float fadeValue = 0.6f;

    IEnumerator OrderProcess () {
        while (orderQueue.Count > 0) {

            var order = orderQueue.Dequeue ();
            var info = m_memPopup[order.id];
            if (info.isUnique && m_popupStack.FirstOrDefault (item => item.Item1.id == order.id) != null) {
                Debug.LogWarning ("중복된 팝업 요청. id : " + order.id);
                SendToPopup (order.id, "Initialize", order.arg);
                continue;
            }
            MonoFade.Instance.transform.SetSiblingIndex (Mathf.Max (0, m_popupStack.Count));

            var go = Instantiate (info.prefab);
            go.transform.SetParentWithDefaultLocal (transform, Vector3.one);
            if (order.arg != null)
                go.SendMessage ("Initialize", order.arg);
            else
                go.SendMessage ("Initialize");

            if (m_popupStack.Count == 0) {
                MonoBlur.Instance.Blur (1f, 0.3f);
                MonoFade.Instance.Fade (fadeValue, 0.3f);
            }

            m_popupStack.Add ((order, go).ToTuple ());

            MonoFade.Instance.SetInputBlock (!order.backCloseEnable ? 1 : 2);

            yield return StartCoroutine (OpenAnimation (go.transform, info.openAnimation));
        }
        isPlaying = false;
    }

    public void Sort () {
        var list = m_popupStack.ToList ();
        for (int i = 0; i < list.Count; i++) {
            if (i == list.Count - 1)
                MonoFade.Instance.SetInputBlock (!list[i].Item1.backCloseEnable?1 : 2);

            list[i].Item2.transform.SetSiblingIndex (i);
        }
        MonoFade.Instance.transform.SetSiblingIndex (Mathf.Max (0, list.Count - 1));
    }

    /// <summary>
    /// 디폴트면 마지막에 넣은걸 닫음
    /// 아이디를 넣었는데 없으면 아무동작을 하지 않음.
    /// </summary>
    /// <param name="targetId"></param>
    /// 
    public void Close(){  
        Close ("");
    }

    [TestMethod]
    public void CloseAll(){
        var n = 0;
        while(ActiveCount > 0){
            if(n++ > 100)
                break;
            
            Close();
        }
    }

    public void Close (string targetId = "", bool closeSimple = false) {
        if (isPlaying)
            return;

        if (m_popupStack.Count == 0)
            return;

        if(targetId != "" && !IsOpen(targetId)){
            return;
        }

        StartCoroutine (CloseRoutine (targetId, closeSimple));
    }

    private IEnumerator CloseRoutine (string targetId = "", bool closeSimple = false) {
        if (m_popupStack.Count == 1) {
            MonoBlur.Instance.Blur (0f, 0.3f);
            MonoFade.Instance.Fade (0f, 0.3f);
        }

        Tuple<Order, GameObject> deleteTarget = null;
        if (targetId != "") {
            deleteTarget = m_popupStack.FirstOrDefault (t => t.Item1.id.Contains (targetId));
        } else {
            deleteTarget = m_popupStack.Last ();
        }

        if (deleteTarget == null)
            yield break;

        var info = m_memPopup[deleteTarget.Item1.id];


        if (info.closePos == null || closeSimple) { } else {
            deleteTarget.Item2.GetComponent<MonoBehaviour> ().Move (info.closePos.position, 0.2f);
            yield return deleteTarget.Item2.GetComponent<MonoBehaviour> ().Scale (new Vector3 (0, 0, 0), 0.2f);
        }

        //if (deleteTarget.Item1.id.Contains ("Tutorial")&& !closeSimple)
        //    LobbyMenu.Instance.CloseTutorial ();

        //else if (deleteTarget.Item1.id.Contains ("Support"))
        //    WebViewManager.Instance.CloseWebView ();

        m_popupStack.Remove (deleteTarget);

        Destroy (deleteTarget.Item2);
        Sort ();
    }

    public void Clear () {
        while (ActiveCount > 0)
            Close ();
    }

    public void SendToPopup (string id, string methodName, object arg) {
        var find = m_popupStack.FirstOrDefault (t => t.Item1.id == id);
        
        find?.Item2.SendMessage (methodName, arg); //없으면 무시함.
        
    }
    /// <summary>
    /// PopupManager.Instance.CheckIsOpen("MessagePopup");
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool IsOpen (string id) {

        var find = m_popupStack.FirstOrDefault (t => t.Item1.id == id);
        // foreach(var i in m_popupStack)
        //     Debug.Log(i.Item1.id);
        //Debug.Log("PopupManager.cs CheckIsOpen(string id) Result : "+(find != null?"true":"false"));
        return find != null;
    }

    IEnumerator OpenAnimation (Transform trans, bool openAnimation) {
        yield return trans.GetComponent<MonoBehaviour> ().Scale (Vector3.one * 0.7f, Vector3.one, openAnimation?openScaleTime : 0.001f);
        Sort ();
    }

    /// <summary>
    /// 결재중 에러 발생시 실행
    /// </summary>
    public void InappPopupsClose () {
        Close ("Shop");
        Close ("VIP");
        Close ("SaveGold");
        Close ("SpecialBuy");
        Close ("Inapp");
        Close ("MessagePopup");
    }

    public struct Order {
        public string id;
        public object arg;

        public string tag;

        public bool backCloseEnable;

        public Order (string id, object arg, string tag, bool backCloseEnable) {
            this.id = id;
            this.arg = arg;
            this.tag = tag;
            this.backCloseEnable = backCloseEnable;
        }
    }

    [TestMethod]
    //테스트용
    public void TestOpen(string id)
    {
        if (id == "Reward")
        {
            //RewardInfo rewardInfo = new RewardInfo
            //{
            //    gold = 2000,
            //    after = 3000,
            //    title = "title",
            //    content = "content"
            //};
            // ForwardCanvasManager.Instance.rewardPanel.Initialize(rewardInfo);
        }
        else
            Open(id, null);
    }

    //[TestMethod]
    ////테스트용
    //public void InappOpen (string msg) {
    //    PanelInappProgress.Set (msg, false);
    //}

    [TestMethod]
    public void TestClose (string id) {
        Close (id);
    }

    [TestMethod]
    public void GetPopupStack () {
        Debug.Log ("PopupStack : " + m_popupStack.Count);
    }

#if UNITY_ANDROID

    [TestMethod]
    public void TestErrorMsg (int errorCode) {
        //WooriIAB.SetErrorMsg ("Error " + errorCode);
    }
#endif

    //[TestMethod]
    //public void TestPopupCallback () {
    //    PanelMessagePopup.Show (STM.Get ("T_USERINFOTITLE"), "Test", true, () => PopupManager.Instance.InappPopupsClose ());
    //}

    //[TestMethod]
    //public void TestMessagePopup () {
    //    PanelMessagePopup.Show (STM.Get ("T_USERINFOTITLE"), "Test", true, null, null, () => PopupManager.Instance.InappPopupsClose ());
    //}
    //[TestMethod(false)]
    //public void TestPopup()
    //{
    //    PanelMessagePopup.Show(title: "",
    //                msg: STM.Get("IAP_PENDING") + "\n" + STM.Get("IAP_CHECK_PAYERROR"),
    //                isOkPopup: false,
    //                onOk: () => {
    //                    Application.OpenURL("https://pay.google.com/");
    //                    PopupManager.Instance.InappPopupsClose();
    //                },
    //                onNo: null,//() => PopupManager.Instance.InappPopupsClose(),
    //                onBack: null,//() => PopupManager.Instance.InappPopupsClose(),
    //                tag: null,
    //                isLock: true
    //                );
    //}

    //[TestMethod (false)]
    //public void TestPopup2 () {
    //    Loading.On ();
    //    // PanelInappProgress.Set(STM.Get("IAP_PENDING_WAIT"));
    //    //WooriIAB.SetErrorMsg("error 6");
    //    PanelMessagePopup.Show (STM.Get ("T_USERINFOTITLE"), STM.Get ("IAP_CANCELLED_PURCHASE"), true, () => {
    //            PopupManager.Instance.Close ("Inapp");
    //            PopupManager.Instance.Close ("MessagePopup");
    //        }, null,
    //        () => {
    //            PopupManager.Instance.Close ("Inapp");
    //            PopupManager.Instance.Close ("MessagePopup");
    //        }, null, true);
    //}

    //[TestMethod (false)]
    //public void TestPopup3 () {
    //    Loading.Off ();
    //    PopupManager.Instance.Close ("MessagePopup");

    //    //Close("Inapp");
    //    // PanelInappProgress.Set(STM.Get("IAP_PENDING_WAIT"));

    //}

    [TestMethod (false)]
    public void GetActiveCountAsLog () {
        Debug.Log ("current popup count " + ActiveCount);
    }
}