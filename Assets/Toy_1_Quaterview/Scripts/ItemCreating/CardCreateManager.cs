using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// from this script, when working is going depper, seperate each parts into cs files
public class CardCreateManager : MonoBehaviour
{

}

public abstract class CardFrame
{
    public ECardType currentCardType;
    public int cardCost;
}


public enum ECardType
{
    None = 0,
    Minion,
    Spell,

}
