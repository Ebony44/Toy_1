using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelperFunctions : MonoBehaviour
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lineStartPoint"></param>
    /// <param name="lineEndPoint"></param>
    /// <param name="lengthMod"> length of second line</param>
    /// <param name="positionModX"> position of second line</param>
    /// <returns></returns>
    public static (Vector2, Vector2) GetPerpendicularLine(Vector2 lineStartPoint, Vector2 lineEndPoint, int lengthMod, 
        int positionModX,
        int positionModY)
    {
        // line 1 = y = mx+n
        // line 2(this function's return value)

        // 1. get m(slope)
        var modValueY = lineEndPoint.y - lineStartPoint.y;
        var modValueX = lineEndPoint.x - lineStartPoint.x;
        var firstLineSlope = modValueY / modValueX;
        var secondLineSlope = -(1 / firstLineSlope);

        var secondLineFirstPointX = lineStartPoint.x - lengthMod + positionModX;
        var secondLineFirstPointY = (secondLineFirstPointX * secondLineSlope) + positionModY;

        var secondLineSecondPointX = lineEndPoint.x + lengthMod + positionModX;
        var secondLineSecondPointY = (secondLineSecondPointX * secondLineSlope) + positionModY;

        Vector2 secondLineFirstPoint = new Vector2(secondLineFirstPointX, secondLineFirstPointY);
        Vector2 secondLineSecondPoint = new Vector2(secondLineSecondPointX, secondLineSecondPointY);

        // 2. first line's slope * second line's slope  = -1
        // m * m` = -1
        //case 1. m = 5, point 1 (1,5) point 2(3,15)
        // then m` = -1/5
        // y = -0.2*x+n
        // x = 1f, y = -0.2f
        // x = 3f, y = -0.6f
        return (secondLineFirstPoint, secondLineSecondPoint);

    }
}
