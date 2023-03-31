using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellChecker : MonoBehaviour
{
    public enum eCheckType
    {
        LEFT = 0,
        RIGHT,
        UP,
        DOWN,
    }

    #region check as linked list
    //public int CheckCellsMatched(CellTag checkingCell, int checkCount, eCheckType checkType)
    //{
    //    var result = 0;
    //    // row first
    //    int leftCount = checkCount;
    //    int rightCount = checkCount;
    //    if (checkingCell.leftCell != null
    //        && checkType == eCheckType.LEFT)
    //    {
    //        leftCount += CheckCellsMatched(checkingCell.leftCell, leftCount, checkType);
    //        // leftCount = checkingCell.leftCell.cellType == checkingCell.cellType ? leftCount + 1 : leftCount;
    //    }



    //    // then column

    //    return result;

    //}
    #endregion

    public bool CheckCellIsMatched(CellTag checkingCell, int checkCount, List<CellTag> cellBoard)
    {
        var result = false;
        var checkerRow = checkingCell.row;
        var checkerColumn = checkingCell.column;

        var leftCount = 0;
        var rightCount = 0;
        var upCount = 0;
        var downCount = 0;

        for (int i = 0; i < cellBoard.Count; i++)
        {
            
        }
        while(leftCount > 2 && rightCount > 2)
        {
            var leftItem = cellBoard.Find(x => x.row == checkerRow - 1);
        }
        

        return result;
    }


}
