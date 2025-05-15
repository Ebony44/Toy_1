using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CorruptionPointEventChannel : EventChannelBase
{
	public UnityAction<float> OnEventRaised;

	public void RaiseEvent(float value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}

}
