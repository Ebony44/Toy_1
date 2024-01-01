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
    protected EMinionType currentType;
    protected CharacterStat moveSpeed; // 0 is immovable
    // protected CharacterStat damage;

    protected CharacterStat summonCount;

    //protected float moveSpeed; // 0 is immovable
    //protected float damage;

}
public enum EMinionType
{
    None = 0,
    Harbinger,
    Demon,
    Native
}

public abstract class SpellFrame
{
    
}
#endregion
