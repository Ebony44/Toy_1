using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTag : MonoBehaviour
{
    public int row;
    public int column;
    public int cellType;
    public int modifier;

    public CellTag upCell;
    public CellTag downCell;
    public CellTag leftCell;
    public CellTag rightCell;

    //public LinkedList<CellTag> rowLink;
    //public LinkedList<CellTag> columnLink;

}
