using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineVisualForMathPractice : MonoBehaviour
{
    [SerializeField] LineRenderer firstLine;
    [SerializeField] LineRenderer secondLine;

    [SerializeField] GameObject firstLineStartPointObj;
    [SerializeField] GameObject firstLineMidPointObj;
    [SerializeField] GameObject firstLineEndPointObj;


    [SerializeField] GameObject secondLineStartPointObj;
    [SerializeField] GameObject secondLineMidPointObj;
    [SerializeField] GameObject secondLineEndPointObj;




    private void Start()
    {
        // Setup(5,5);
        SetupWithStartPoint(
            firstX: 0,
            firstY: 0,
            secondX: 5,
            secondY: 10,
            modX: 1,
            modY: 1,
            secondLineLength: 15
            );
    }

    [TestMethod(false)]
    public void Setup(int modX, int modY)
    {
        Vector2 startPoint = new Vector2(1, 5);
        Vector2 endPoint = new Vector2(5, 25);

        var tempVectors = MathHelperFunctions.GetPerpendicularLine(startPoint, endPoint, 15, modX, modY);

        firstLine.positionCount = 2;
        secondLine.positionCount = 2;

        // Vector2 secondStart = -9, 1.8
        // Vector2 secondEnd = 15 , -3

        firstLine.SetPosition(0, startPoint);
        firstLine.SetPosition(1, endPoint);

        secondLine.SetPosition(0, tempVectors.Item1);
        secondLine.SetPosition(1, tempVectors.Item2);

        // lineRenderer.SetPosition(linePosObjects.Count, linePosObjects[0].position);
    }

    [TestMethod(false)]
    public void SetupWithStartPoint(float firstX, float firstY,
        float secondX, float secondY,
        float modX, float modY,
        float secondLineLength
        )
    {
        Vector2 startPoint = new Vector2(firstX, firstY);
        Vector2 endPoint = new Vector2(secondX, secondY);

        SetupPos(firstLineStartPointObj, startPoint);
        var firstMidPointX = (startPoint.x + endPoint.x) / 2f;
        var firstMidPointY = (startPoint.y + endPoint.y) / 2f;
        var firstMidPoint = new Vector2(firstMidPointX, firstMidPointY);

        SetupPos(firstLineMidPointObj, firstMidPoint);

        SetupPos(firstLineEndPointObj, endPoint);


        var tempVectors = MathHelperFunctions.GetPerpendicularLine(startPoint, endPoint, secondLineLength, modX, modY);

        SetupPos(secondLineStartPointObj, tempVectors.Item1);
        var secondMidPointX = (tempVectors.Item1.x + tempVectors.Item2.x) / 2f;
        var secondMidPointY = (tempVectors.Item1.y + tempVectors.Item2.y) / 2f;
        var secondMidPoint = new Vector2(secondMidPointX, secondMidPointY);

        SetupPos(secondLineMidPointObj, secondMidPoint);

        SetupPos(secondLineEndPointObj, tempVectors.Item2);


        firstLine.positionCount = 2;
        secondLine.positionCount = 2;

        // Vector2 secondStart = -9, 1.8
        // Vector2 secondEnd = 15 , -3

        firstLine.SetPosition(0, startPoint);
        firstLine.SetPosition(1, endPoint);

        secondLine.SetPosition(0, tempVectors.Item1);
        secondLine.SetPosition(1, tempVectors.Item2);

        // lineRenderer.SetPosition(linePosObjects.Count, linePosObjects[0].position);
    }


    public void SetupPos(GameObject obj, Vector2 paramPos)
    {
        obj.transform.position = paramPos;
    }

}
