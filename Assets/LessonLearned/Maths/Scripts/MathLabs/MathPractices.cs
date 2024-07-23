using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathPractices : MonoBehaviour
{
    
    private void Start()
    {
        // TestRotationMatrix();
        
    }

    public void DrawLineWithTwoPoints(Vector2 paramStartPoint, Vector2 paramEndPoint)
    {
        // draw line with two points
    }
    

    Vector3 cachedStartPoint = Vector3.zero;
    Vector3 cachedEndPoint = Vector3.zero;
    bool bIsDrawn = false;

    [SerializeField] private LineRenderer currentLineDrawer = null;

    [SerializeField] private GameObject firstIndicator = null;
    [SerializeField] private GameObject secondIndicator = null;
    [SerializeField] private GameObject thirdIndicator = null;


    [TestMethod(false)]
    public void TestRotationMatrixAtTwoDimension(float paramDegree)
    {
        bIsDrawn = true;
        // rotation matrix

        // if horizontal line is there, i can find easily diagonal line from that line like
        // line start from (0,0) to (10,0)
        // some diagonal line which start at (0,-5) to (0,5)

        var startPoint = new Vector2(0, 0);
        var endPoint = new Vector2(10, 0);

        var diagonalStartPoint = new Vector2(0, -5);
        var diagonalEndPoint = new Vector2(0, 5);

        // give me diagonal line which start from (0,-5) to (0,5) and rotate 90 degree
        // with rotation matrix
        // give me calculation of rotation matrix, rotate 90 degree

        // in this case, 
        // target x` and y` is
        // x` = x * cos(theta) - (y * sin(theta))
        // y` = x * sin (theta) + y * cos(theta)
        // for example, theta is 60 degree
        // x` = x * cos(60) - (y * sin(60))
        // y` = x * sin (60) + y * cos(60)
        var currentDegree = 60;
        var radianTheta = currentDegree * Mathf.Deg2Rad;
        var currentStartX = diagonalStartPoint.x;
        var currentStartY = diagonalStartPoint.y;

        var rotatedStartX = currentStartX * Mathf.Cos(radianTheta) - (currentStartY * Mathf.Sin(radianTheta));
        var rotatedStartY = currentStartX * Mathf.Sin(radianTheta) + currentStartY * Mathf.Cos(radianTheta);

        var currentEndX = diagonalEndPoint.x;
        var currentEndY = diagonalEndPoint.y;

        var rotatedEndX = currentEndX * Mathf.Cos(radianTheta) - (currentEndY * Mathf.Sin(radianTheta));
        var rotatedEndY = currentEndX * Mathf.Sin(radianTheta) + currentEndY * Mathf.Cos(radianTheta);

        var passingDegree = paramDegree;
        var resultStartVector = GetRotatedMatrixAsTwoDimension(diagonalStartPoint, passingDegree);
        var resultEndVector = GetRotatedMatrixAsTwoDimension(diagonalEndPoint, passingDegree);



        Debug.Log($"Rotated Start Point: {rotatedStartX}, {rotatedStartY}");
        Debug.Log($"Rotated End Point: {rotatedEndX}, {rotatedEndY}");
        Debug.Log("start vector is " + resultStartVector
            + " end vector is " + resultEndVector);
        cachedStartPoint = new Vector3(resultStartVector.x, resultStartVector.y, 0);
        cachedEndPoint = new Vector3(resultEndVector.x, resultEndVector.y, 0);

        currentLineDrawer.SetPosition(0, cachedStartPoint);
        currentLineDrawer.SetPosition(1, cachedEndPoint);


        // Debug.Log($"Rotated Start Point: {rotatedStartX}, {rotatedStartY}");

        

    }
    [TestMethod(false)]
    public void TestRotationMatrixAtThree(float paramDegree)
    {
        // and below is 3-dimension

        // [1, 0, 0]
        // [0, cos(theta), -sin(theta)]
        // [0, sin(theta), cos(theta)]
        // is Rx(theta) matrix

        // [cos(theta), 0, sin(theta)]
        // [0, 1, 0]
        // [-sin(theta), 0, cos(theta)]
        // is Ry(theta) matrix

        // [cos(theta), -sin(theta), 0]
        // [sin(theta), cos(theta), 0]
        // [0, 0, 1]
        // is Rz(theta) matrix

        // example 1) point (1,2,3), and want to rotate it x-axis, 60 degree(counter-clockwise)
        // x` is = 1*1 + 0*2 + 0*3
        // y` is = 0*1 + cos(60)*2 + -sin(60)*3
        // z` is = 0*1 + sin(60)*2 + cos(60)*3
        var secondStartPoint = new Vector3(1, 2, 3);
        // ok.. point of (1,2,3) and x-axis counter-clockwise, is result same as (1,)
        // in this case, it's x is 1
        // y is 0 + 1 - root(3) / 2 * 3
        var radianTheta = 60 * Mathf.Deg2Rad;
        Debug.Log("Second Start Point is " + secondStartPoint);
        var modifiedX = 1 * secondStartPoint.x + 0 * secondStartPoint.y + 0 * secondStartPoint.z;
        var modifiedY = 0 * secondStartPoint.x + Mathf.Cos(radianTheta) * secondStartPoint.y + -Mathf.Sin(radianTheta) * secondStartPoint.z;
        var modifiedZ = 0 * secondStartPoint.x + Mathf.Sin(radianTheta) * secondStartPoint.y + Mathf.Cos(radianTheta) * secondStartPoint.z;
        Debug.Log("Modified X is " + modifiedX + " Modified Y is " + modifiedY + " Modified Z is " + modifiedZ);

        // at 90 degree
        radianTheta = 90 * Mathf.Deg2Rad;
        modifiedX = 1 * secondStartPoint.x + 0 * secondStartPoint.y + 0 * secondStartPoint.z;
        modifiedY = 0 * secondStartPoint.x + Mathf.Cos(radianTheta) * secondStartPoint.y + -Mathf.Sin(radianTheta) * secondStartPoint.z;
        modifiedZ = 0 * secondStartPoint.x + Mathf.Sin(radianTheta) * secondStartPoint.y + Mathf.Cos(radianTheta) * secondStartPoint.z;

        // at 90, sine is 1, cosine is 0
        // x should be 1
        // y should be -3
        // z should be 2

    }

    public Vector2 GetRotatedMatrixAsTwoDimension(Vector2 paramVector, float paramDegree)
    {
        var result = Vector2.zero;
        var radianTheta = paramDegree * Mathf.Deg2Rad;
        var currentX = paramVector.x;
        var currentY = paramVector.y;

        var rotateX = currentX * Mathf.Cos(radianTheta) - (currentY * Mathf.Sin(radianTheta));
        var rotateY = currentX * Mathf.Sin(radianTheta) + currentY * Mathf.Cos(radianTheta);
        result = new Vector2(rotateX, rotateY);

        return result;
    }

    public Vector3 GetRotatedMatrixAsThreeDimension(Vector3 paramVector, float paramDegree, eAxis eAxis)
    {
        var result = Vector3.zero;

        return result;
    }
}


public enum eAxis
{
    X,
    Y,
    Z
}