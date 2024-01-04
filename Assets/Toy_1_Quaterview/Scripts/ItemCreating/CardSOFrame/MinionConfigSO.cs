using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinionConfig", menuName = "CardFrame/Minion")]
public class MinionConfigSO : ScriptableObject,ICardTypeSO
{
    [Tooltip("General variables")]
    [SerializeField] private float  _initialCardCost;
    [SerializeField] private float _initialCardPower;

    [Tooltip("Minion specific variables")]

    [SerializeField] private EMinionRaceType _initialMinionType;

    [SerializeField] private List<EMinionTagType> initialMinionTags;

    [SerializeField] private int _initialSummonCount;

    [SerializeField] private float _initialMoveSpeed;

    [SerializeField] private float _initialAttackRange;


    public EMinionRaceType InitialMinionType=> _initialMinionType;

    public float InitialCardCost => _initialCardCost;

    public float InitialCardPower => _initialCardPower;
    
    public int InitialSummonCount => _initialSummonCount;

    public float InitialMoveSpeed => _initialMoveSpeed;

    public float InitialAttackRange => _initialAttackRange;

}
