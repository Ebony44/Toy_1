using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "CardConfig", menuName = "Toy1/CardFrame/Card")]
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
	// [SerializeField]
	[Tooltip("Info which is minion or spell")]
	private ICardTypeSO _currentCardInfo = default;

	[Header("Modifier which applied after config created by player")]

	private float modifiedValue = default;


	public LocalizedString Name => _name;
	public Sprite PreviewImage => _previewImage;
	public LocalizedString Description => _description;

	public ECardType ECardType => _eCardType;

	public ICardTypeSO CurrentCardInfo => _currentCardInfo;


	public void Initialize(ICardTypeSO paramCardInfo)
    {
		_currentCardInfo = paramCardInfo;
    }

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
