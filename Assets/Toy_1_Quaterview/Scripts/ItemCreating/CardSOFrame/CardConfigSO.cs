using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class CardConfigSO : SerializableScriptableObject
{
	[Tooltip("The name of the item")]
	[SerializeField] private LocalizedString _name = default;

	[Tooltip("A preview image for the item")]
	[SerializeField]
	private Sprite _previewImage = default;

	[Tooltip("A description of the item")]
	[SerializeField]
	private LocalizedString _description = default;

	[SerializeField]
	private ECardType _eCardType = default;

	// interface and 
	[SerializeField]
	private ICardTypeSO _currentCardInfo = default;


    public LocalizedString Name => _name;
	public Sprite PreviewImage => _previewImage;
	public LocalizedString Description => _description;

	public ECardType ECardType => _eCardType;

	public ICardTypeSO CurrentCardInfo => _currentCardInfo;


    public ICardTypeSO TellCardInfo()
    {
		if(_eCardType == ECardType.Minion)
        {
			return (MinionConfigSO)_currentCardInfo;
        }
		else if(_eCardType == ECardType.Spell)
        {
			
        }
		return null;
    }

}
