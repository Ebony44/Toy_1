using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionModifierFrame : MonoBehaviour
{
    [Tooltip("General variables")]
    [SerializeField] private CharacterStat cardCost;
    [SerializeField] private CharacterStat cardPower;

    [Tooltip("Minion specific variables")]

    [SerializeField] private EMinionRaceType minionType;

    [SerializeField] private List<EMinionTagType> modifiedMinionTags;

    [SerializeField] private CharacterStat summonCount;

    [SerializeField] private CharacterStat moveSpeed;

    [SerializeField] private CharacterStat attackRange;

    // [SerializeField] private HealthConfigSO _initialHealthConfig;
}
