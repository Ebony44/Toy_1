using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionConfigSO : ScriptableObject
{
    [SerializeField] private float  _initialCardCost;
    [SerializeField] private float _initialCardPower;
    // [SerializeField] private EMinionType initialMinionType;

    [SerializeField] private int _initialSummonCount;

    [SerializeField] private float _initialMoveSpeed;

    [SerializeField] private float _initialAttackRange;

    public float InitialCardCost => _initialCardCost;

    public float InitialCardPower => _initialCardPower;
    
    public int InitialSummonCount => _initialSummonCount;

    public float InitialMoveSpeed => _initialMoveSpeed;

    public float InitialAttackRange => _initialAttackRange;

}
