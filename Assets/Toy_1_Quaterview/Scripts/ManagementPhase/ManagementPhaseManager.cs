using UnityEngine;

public class ManagementPhaseManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetGridFromPos(Vector2 currentPos)
    {
        // floor or ceil
        Vector2 gridPos = new Vector2(Mathf.Floor(currentPos.x), Mathf.Floor(currentPos.y));
        return gridPos;
    }

}
