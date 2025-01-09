using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Toy_1
{
	[CreateAssetMenu(menuName = "Toy1/IngameResourceSO")]
	public class IngameResourceChannelSO : ScriptableObject
	{
		// public readonly EInGameResources resourceType;
		public UnityAction<EInGameResources, float> OnSpentEventRaised;

		public UnityAction<EInGameResources, IngameResourceModInfo> OnInfoChangeEventRaised;


		public void RaiseResourceSpentEvent(EInGameResources resourceType, float spentValue)
		{
			if (OnSpentEventRaised != null)
				OnSpentEventRaised.Invoke(resourceType, spentValue);
		}
		public void RaiseResourceInfoChangedEvent(EInGameResources resourceType, IngameResourceModInfo paramInfo)
		{
			OnInfoChangeEventRaised?.Invoke(resourceType, paramInfo);
		}

	}

	public class IngameResourceModInfo
	{
		EInGameResources currentType;
		// CharacterStat currentResourceIncreaseSpeed;

		float modStartResource; // use it with operator +=
		float modMaxLimit; // use it with operator +=
		float modIncreaseSpeed; // use it with operator +=

		// CharacterStat currentResourceIncreaseSpeed;

	}
}
