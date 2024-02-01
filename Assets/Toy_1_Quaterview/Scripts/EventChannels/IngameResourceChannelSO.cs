using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Toy1/IngameResourceSO")]
public class IngameResourceChannelSO : ScriptableObject
{
	// public readonly EInGameResources resourceType;
    public UnityAction<EInGameResources, float> OnEventRaised;

	public void RaiseEvent(EInGameResources resourceType, float value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(resourceType,value);
	}

}
