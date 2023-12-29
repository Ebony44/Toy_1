using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// from this script, when working is going depper, seperate each parts into cs files
public class CardCreateManager : MonoBehaviour
{

}

public abstract class CardFrame
{
    protected ECardType currentCardType;
    // protected int cardCost;
    protected CharacterStat cardCost;

    protected MinionFrame currentMinionInfo;
    protected SpellFrame currentSpellInfo;
}


public enum ECardType
{
    None = 0,
    Minion,
    Spell,

}

#region 
public abstract class MinionFrame
{
    protected EMinionType currentType;
    protected CharacterStat moveSpeed; // 0 is immovable
    protected CharacterStat damage;
    //protected float moveSpeed; // 0 is immovable
    //protected float damage;

}
public enum EMinionType
{
    None = 0,
    Farharbinger,
}

public abstract class SpellFrame
{
    
}
#endregion
