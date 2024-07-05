using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonMessagePopup : Popup<MSGInfo>
{
    public override void Initialize(MSGInfo info)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public class MSGInfo
{
    public bool isOKPopup = true;
    public string title;
    public string msg;

    public Action OnYes = null;
    public Action OnNo = null;
    public Action OnBack = null;
    //public MSGResult result;
}
