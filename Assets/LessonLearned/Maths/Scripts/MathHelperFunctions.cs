using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelperFunctions : MonoBehaviour
{
    #region line equation related
    /// <summary>
    /// 
    /// </summary>
    /// <param name="lineStartPoint"></param>
    /// <param name="lineEndPoint"></param>
    /// <param name="lengthMod"> length of second line</param>
    /// <param name="positionModX"> position of second line</param>
    /// <returns></returns>
    public static (Vector2, Vector2) GetPerpendicularLine(Vector2 lineStartPoint, Vector2 lineEndPoint, 
        float lengthMod, 
        float positionModX,
        float positionModY)
    {
        // line 1 = y = mx+n
        // line 2(this function's return value)

        // 1. get m(slope)
        //var modValueY = lineEndPoint.y - lineStartPoint.y;
        //var modValueX = lineEndPoint.x - lineStartPoint.x;
        //var firstLineSlope = modValueY / modValueX;
        var firstLineSlope = GetSlopeOfLine(lineStartPoint, lineEndPoint);
        if (firstLineSlope == 0)
        {
            Debug.LogError("y / x is 0");
            // in this case, x = 10, y = 0 // x = 20 , y 0... 
            // m == 0
            // second line should be x 0, y = 10 // x = 0, y = 20
            var secondLineFirstPointX = lineStartPoint.y + positionModX;
            var secondLineFirstPointY = lineStartPoint.x + positionModY;

            var secondLineSecondPointX = lineEndPoint.y + positionModX;
            var secondLineSecondPointY = lineEndPoint.x + positionModY;

            Vector2 secondLineFirstPoint = new Vector2(secondLineFirstPointX, secondLineFirstPointY);
            Vector2 secondLineSecondPoint = new Vector2(secondLineSecondPointX, secondLineSecondPointY);
            return (secondLineFirstPoint, secondLineSecondPoint);
        }
        else
        {
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

    public static (Vector2, Vector2) GetPerpendicularLineAtMidPoint(Vector2 lineStartPoint, Vector2 lineEndPoint,
        Vector2 midPoint,
        float lengthMod
        )

    {
        Debug.Log("[GetPerpendicularLineAtMidPoint], first line first point " + lineStartPoint
            + " firstl ine second point " + lineEndPoint);
        // line 1 = y = mx+n
        // line 2(this function's return value)

        // 1. get m(slope)
        //var modValueY = lineEndPoint.y - lineStartPoint.y;
        //var modValueX = lineEndPoint.x - lineStartPoint.x;
        //var firstLineSlope = modValueY / modValueX;

        if(lineStartPoint.x == lineEndPoint.x
            && lineStartPoint.y == lineEndPoint.y)
        {
            // 2 postions are identical
            Vector2 secondLineFirstPoint = new Vector2(lineStartPoint.x, lineStartPoint.y);
            Vector2 secondLineSecondPoint = new Vector2(lineEndPoint.x, lineEndPoint.y + 1);

        }

        var firstLineSlope = GetSlopeOfLine(lineStartPoint, lineEndPoint);
        if (firstLineSlope == 0)
        {
            Debug.LogError("y / x is 0");
            // in this case, x = 10, y = 0 // x = 20 , y 0... 
            // m == 0
            // second line should be x 0, y = 10 // x = 0, y = 20

            // find which is 0
            

            var secondLineFirstPointX = lineStartPoint.y;
            var secondLineFirstPointY = lineStartPoint.x;

            var secondLineSecondPointX = lineEndPoint.y;
            var secondLineSecondPointY = lineEndPoint.x;

            Vector2 secondLineFirstPoint = new Vector2(secondLineFirstPointX, secondLineFirstPointY);
            Vector2 secondLineSecondPoint = new Vector2(secondLineSecondPointX, secondLineSecondPointY);


            float absSLFirstX = Mathf.Abs(secondLineFirstPointX);
            float absSLFirstY = Mathf.Abs(secondLineFirstPointY);

            float absSLSecondX = Mathf.Abs(secondLineSecondPointX);
            float absSLSecondY = Mathf.Abs(secondLineSecondPointY);

            if ((absSLFirstX - absSLSecondX) == 0)
            {
                // x is 0,
                Debug.LogError("x is 0");

                // secondLineFirstPoint = new Vector2(midPoint.x, secondLineFirstPointY);
                secondLineFirstPoint = new Vector2(midPoint.x, -secondLineSecondPointY);
                secondLineSecondPoint = new Vector2(midPoint.x, secondLineSecondPointY);
            }
            else if( (absSLFirstY - absSLSecondY) == 0)
            {
                // examples doesn't come here...
                // y is 0
                Debug.LogError("y is 0");
                secondLineFirstPoint = new Vector2(secondLineFirstPointX, midPoint.y);
                secondLineSecondPoint = new Vector2(secondLineSecondPointX, midPoint.y);
            }

            
            return (secondLineFirstPoint, secondLineSecondPoint);
        }
        else
        {
            var secondLineSlope = -(1 / firstLineSlope);
            var currentConstant = GetConstantOfLineEquation(midPoint, secondLineSlope);

            float positionModX = 0f; // it's x of line.. in future, maybe in use
            float positionModY = currentConstant;


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


    public static float GetSlopeOfLine(Vector2 lineStartPoint, Vector2 lineEndPoint)
    {
        float resultSlope = 0f;
        // m = (y2 - y1) / (x2 - x1)
        var modValueY = lineEndPoint.y - lineStartPoint.y;
        var modValueX = lineEndPoint.x - lineStartPoint.x;
        resultSlope = modValueY / modValueX;
        return resultSlope;
    }

    public static float GetConstantOfLineEquation(Vector2 onePoint, float slope)
    {
        // ex 1)
        // mid point is 1,2, slope is -1/2
        // y = mx + n
        // 2 = (-1/2 * 1) + n
        // n = 5/2
        float resultConstant = 0f;
        // y - mx = n
        var modX = onePoint.x;
        var modY = onePoint.y;
        resultConstant = modY - (slope * modX);
        return resultConstant;

    }

    public static Vector2 GetMidPointOfLine(Vector2 lineStartPoint, Vector2 lineEndPoint)
    {
        Vector2 resultMidPoint = Vector2.zero;
        var midPointX = (lineStartPoint.x + lineEndPoint.x) / 2f;
        var midPointY = (lineStartPoint.y + lineEndPoint.y) / 2f;
        resultMidPoint = new Vector2(midPointX, midPointY);
        return resultMidPoint;
    }

    #endregion
    public static Vector2 GetEasedMidPoint(Vector2 lineStartPoint, Vector2 lineEndPoint, Vector2 midPoint, 
        float heightOfEasePoint,
        bool bSelectItem1
        )
    {
        Vector2 result = Vector2.zero;
        var perpendicularLine = GetPerpendicularLineAtMidPoint(lineStartPoint, lineEndPoint, midPoint, heightOfEasePoint);
        if(bSelectItem1)
        {
            result = perpendicularLine.Item1;
        }
        else
        {
            result = perpendicularLine.Item2; // must check it is always item2? 
        }

        Debug.Log("[GetEasedMidPoint], item 1 is " + perpendicularLine.Item1
            + " item 2 is " + perpendicularLine.Item2);
        // would depend on quadrant
        return result;
    }

}

