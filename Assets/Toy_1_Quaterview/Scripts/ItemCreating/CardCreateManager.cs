using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// from this script, when working is going depper, seperate each parts into cs files
public class CardCreateManager : MonoBehaviour
{
    public void InstantiateCard(CardFrame targetPrefab)
    {

    }
    
}

public abstract class CardFrame
{
    protected ECardType currentCardType;
    // protected int cardCost;
    protected CharacterStat cardCost;
    protected CharacterStat cardPower; // related to minion/spell's damage, healing scale, or etc...

    protected MinionFrame currentMinionInfo;
    protected SpellFrame currentSpellInfo;
}


public enum ECardType
{
    None = 0,
    Minion,
    Spell,

}

#region Abstracts
public abstract class MinionFrame
{
    protected EMinionRaceType currentType;
    protected CharacterStat moveSpeed; // 0 is immovable
    // protected CharacterStat damage;

    protected CharacterStat summonCount;

    protected CharacterStat attackRange;

    protected CharacterStat damageReduction;


    //protected float moveSpeed; // 0 is immovable
    //protected float damage;

}
public enum EMinionRaceType
{
    None = 0,
    Harbinger,
    Demon,
    Native
}
public enum EMinionTagType
{
    Normal,
    Tower, // maybe move speed is zero, or tanky
    Duration,
    Stealth,
    Packs, // equal to or more than 10

    OnsummonSkill,
    OnDeathSkill,






}
public enum EAttackRangeType
{
    Melee,
    Ranged,

}

public abstract class SpellFrame
{
    
}
#endregion
