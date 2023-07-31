using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineObjectSetup : MonoBehaviour
{
    [SerializeField] List<Transform> linePosObjects;
    // Transform[] linePosObjects;
    [SerializeField] LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {

        int index = 0;
        lineRenderer.positionCount = linePosObjects.Count + 1;
        foreach (var item in linePosObjects)
        {
            lineRenderer.SetPosition(index, item.position);
            index++;
        }
        lineRenderer.SetPosition(linePosObjects.Count, linePosObjects[0].position);

    }

}
