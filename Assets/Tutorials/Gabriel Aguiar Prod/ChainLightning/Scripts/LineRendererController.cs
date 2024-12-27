using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GabrielAguiar
{
    public class LineRendererController : MonoBehaviour
    {
        [SerializeField] List<LineRenderer> lineRenderers = new List<LineRenderer>();

        public void SetPosition(Transform start, Transform end)
        {
            if(lineRenderers.Count <= 0)
            {
                Debug.LogError("LineRenderers list is empty");
                return;
            }
            for (int i = 0; i < lineRenderers.Count; i++)
            {
                if (lineRenderers[i].positionCount >= 2)
                {
                    lineRenderers[i].SetPosition(0, start.position);
                    lineRenderers[i].SetPosition(1, end.position);
                }
                else
                {
                    Debug.LogError("LineRenderer must have at least 2 positions");
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}

