using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathPractices : MonoBehaviour
{
    // rotation matrix

    // if horizontal line is there, i can find easily diagonal line from that line like
    // line start from (0,0) to (10,0)
    // some diagonal line which start at (0,-5) to (0,5)


    private void Start()
    {
        TestRotationMatrix();
    }

    public void TestRotationMatrix()
    {
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

        var passingDegree = 90;
        var resultStartVector = GetRotatedMatrix(diagonalStartPoint, passingDegree);
        var resultEndVector = GetRotatedMatrix(diagonalEndPoint, passingDegree);



        Debug.Log($"Rotated Start Point: {rotatedStartX}, {rotatedStartY}");
        Debug.Log($"Rotated End Point: {rotatedEndX}, {rotatedEndY}");
        Debug.Log("start vector is " + resultStartVector
            + " end vector is " + resultEndVector);


        // Debug.Log($"Rotated Start Point: {rotatedStartX}, {rotatedStartY}");

    }

    public Vector3 GetRotatedMatrix(Vector3 paramVector, float paramDegree)
    {
        var result = Vector3.zero;
        var radianTheta = paramDegree * Mathf.Deg2Rad;
        var currentX = paramVector.x;
        var currentY = paramVector.y;

        var rotateX = currentX * Mathf.Cos(radianTheta) - (currentY * Mathf.Sin(radianTheta));
        var rotateY = currentX * Mathf.Sin(radianTheta) + currentY * Mathf.Cos(radianTheta);
        result = new Vector3(rotateX, rotateY, 0);

        return result;
    }


}
