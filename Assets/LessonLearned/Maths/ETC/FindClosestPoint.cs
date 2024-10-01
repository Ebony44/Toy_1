using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [TestMethod(false)]
    public void TestShowClosestPoint()
    {

    }


    // https://stackoverflow.com/questions/51905268/how-to-find-closest-point-on-line
    // For finite lines:
    Vector3 GetClosestPointOnFiniteLine(Vector3 point, Vector3 line_start, Vector3 line_end)
    {
        Vector3 line_direction = line_end - line_start;
        float line_length = line_direction.magnitude;
        line_direction.Normalize();
        float project_length = Mathf.Clamp(Vector3.Dot(point - line_start, line_direction), 0f, line_length);
        return line_start + line_direction * project_length;
    }

    // For infinite lines:
    Vector3 GetClosestPointOnInfiniteLine(Vector3 point, Vector3 line_start, Vector3 line_end)
    {
        return line_start + Vector3.Project(point - line_start, line_end - line_start);
    }
    // 
}
