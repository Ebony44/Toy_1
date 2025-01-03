using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LlamAcademy
{
    [CreateAssetMenu(fileName = "Shoot Config", menuName = "Test/Guns/Shoot Configuration", order = 2)]
    public class ShootConfigurationScriptableObject : ScriptableObject
    {
        public LayerMask HitMask;
        public Vector3 Spread = new Vector3(0.1f, 0.1f, 0.1f);
        public float FireRate = 0.25f;


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
