using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LlamAcademy
{
    [DisallowMultipleComponent]
    public class PlayerGunSelector : MonoBehaviour
    {
        [SerializeField]
        private GunType gun;

        [SerializeField]
        private Transform gunParent;

        [SerializeField]
        private List<GunScriptableObject> guns;

        // [SerializeField]
        private PlayerIK inverseKinematics;
        // playerIK

        [Space]
        [Header("Runtime Filled")]
        public GunScriptableObject ActiveGun;

        private void Start()
        {
            GunScriptableObject currentGun = guns.Find(x => x.Type == this.gun);

            if(currentGun == null)
            {
                Debug.LogError("asdf");
            }
            else
            {
                ActiveGun = currentGun;
                currentGun.Spawn(gunParent, this);

                Transform[] allChildren = gunParent.GetComponentsInChildren<Transform>();
                inverseKinematics.LeftElbowIKTarget = allChildren.FirstOrDefault(child => child.name == "LeftElbow");
                inverseKinematics.RightElbowIKTarget = allChildren.FirstOrDefault(child => child.name == "RighttElbow");
                inverseKinematics.LeftHandIKTarget = allChildren.FirstOrDefault(child => child.name == "LeftHand");
                inverseKinematics.RightHandIKTarget = allChildren.FirstOrDefault(child => child.name == "RightHand");

            }

        }
    }
}

