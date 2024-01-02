using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionConfigSO : ScriptableObject
{
    [SerializeField] private float  _initialCardCost;
    [SerializeField] private float _initialCardPower;
    // [SerializeField] private EMinionType initial;

    public float InitialCardCost => _initialCardCost;

    public float InitialCardPower => _initialCardPower;

}
