using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Popup<TPacket> : MonoBehaviour
{
    public abstract void Initialize(TPacket info);

    public virtual void PopupClose(){
        PopupManager.Instance.Close();
    }

    public void OpenPopup(string id){
        PopupManager.Instance.Open(id, null);
    }
}
