using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScalerUpdater : MonoBehaviour
{
	private int m_Width;

	// Token: 0x04000C10 RID: 3088
	private int m_Height;

	// Token: 0x04000C11 RID: 3089
	[UnityEngine.Serialization.FormerlySerializedAs("referenceResolution")]
	[SerializeField]
	private Vector2 m_ReferenceResolution;

	// Token: 0x04000C12 RID: 3090
	private CanvasScaler m_CanvasScaler;

    private void Awake()
    {
		this.m_CanvasScaler = base.GetComponent<CanvasScaler>();
		// base.enabled = !Application.isPlaying;
    }

    private void LateUpdate()
    {
		if (this.m_Width == Screen.width && this.m_Height == Screen.height)
		{
			return;
		}
		this.m_Width = Screen.width;
		this.m_Height = Screen.height;
		bool flag = (float)this.m_Width < this.m_ReferenceResolution.x;
		bool flag2 = (float)this.m_Height < this.m_ReferenceResolution.y;
		if (flag && flag2)
		{
			this.m_CanvasScaler.referenceResolution = new Vector2(this.m_ReferenceResolution.x, this.m_ReferenceResolution.y);
			return;
		}
		if (flag)
		{
			this.m_CanvasScaler.referenceResolution = new Vector2(this.m_ReferenceResolution.x, (float)this.m_Height);
			return;
		}
		if (flag2)
		{
			this.m_CanvasScaler.referenceResolution = new Vector2((float)this.m_Width, this.m_ReferenceResolution.y);
			return;
		}
		this.m_CanvasScaler.referenceResolution = new Vector2((float)this.m_Width, (float)this.m_Height);
	}
}
