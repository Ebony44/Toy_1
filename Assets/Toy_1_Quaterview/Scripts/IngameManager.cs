using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Toy_1
{
    public enum EInGameResources
    {
        Blood = 0,
        Bone,
        Flesh,
    }

    public class IngameResourceInfo
    {
        public EInGameResources currentType;
        // CharacterStat currentResourceIncreaseSpeed;

        public float currentResource;
        public float currentMaxLimit;
        // public readonly float fixedMaxLimit;

        public float startResourceValue;
        public float increaseSpeed;

        public IngameResourceInfo(EInGameResources currentType, float currentResource, float currentMaxLimit, float startResourceValue, float increaseSpeed)
        {
            this.currentType = currentType;
            this.currentResource = currentResource;
            this.currentMaxLimit = currentMaxLimit;
            this.startResourceValue = startResourceValue;
            this.increaseSpeed = increaseSpeed;
        }


        // CharacterStat currentResourceIncreaseSpeed;

    }

    public class IngameManager : MonoBehaviour
    {

        #region resources of ingame
        // currently blood, flesh and bone
        public Dictionary<EInGameResources, IngameResourceInfo> currentResourceInfos = new Dictionary<EInGameResources, IngameResourceInfo>(3);
        // public Dictionary<EInGameResources, float> currentResources = new Dictionary<EInGameResources, float>(3);
        // public Dictionary<EInGameResources, float> baseResourceIncreaseSpeed = new Dictionary<EInGameResources, float>(3);
        // public Dictionary<EInGameResources, float> currentMaxLimitResources = new Dictionary<EInGameResources, float>(3);

        public readonly Dictionary<EInGameResources, float> baseStarts = new Dictionary<EInGameResources, float>(3);
        public readonly Dictionary<EInGameResources, float> baseMaxLimits = new Dictionary<EInGameResources, float>(3);
        public readonly Dictionary<EInGameResources, float> baseIncreaseSpeeds = new Dictionary<EInGameResources, float>(3);


        // use it as listener
        // so no need to reference other components
        public List<IngameResourceChannelSO> resourceEventChannels = new List<IngameResourceChannelSO>();

        // event which broadcast or listen
        public VoidEventChannelSO OnEnemyDeath;

        #endregion


        private void Start()
        {
            // Initialize();
        }
        private void Update()
        {

        }
        private void OnEnable()
        {
            foreach (var item in resourceEventChannels)
            {
                item.OnSpentEventRaised += OnResourceChanged;

            }
        }
        private void OnDisable()
        {
            foreach (var item in resourceEventChannels)
            {
                item.OnSpentEventRaised -= OnResourceChanged;
            }
        }
        [TestMethod(false)]
        public void TestBreakPoint()
        {
            // var tempCurrent = currentResources[EInGameResources.Blood];
            var tempCurrent = currentResourceInfos[EInGameResources.Blood];

        }

        public void OnResourceChanged(EInGameResources resourceType, float settingValue)
        {
            Debug.Log(" current resource type " + resourceType
                + " before value " + currentResourceInfos[resourceType].currentResource
                + " after value " + (currentResourceInfos[resourceType].currentResource + settingValue));
            currentResourceInfos[resourceType].currentResource += settingValue;

        }

        public void Initialize()
        {
            var currentItems = System.Enum.GetValues(typeof(EInGameResources));
            foreach (EInGameResources item in currentItems)
            {
                // currentResources.Add(item, 0);
                var currentMaxLimit = baseMaxLimits[item];
                var currentStartResource = baseStarts[item];
                var currentIncreaseSpeed = baseIncreaseSpeeds[item];
                IngameResourceInfo currentInfo = new IngameResourceInfo(item, 0, currentMaxLimit, currentStartResource, currentIncreaseSpeed);

                currentResourceInfos.Add(item, currentInfo);
            }
        }

        //public void Initialize()
        //{

        //}

        public void UpdateResources(EInGameResources targetResource, float settingValue)
        {

        }

        public void GameClear()
        {
            // reset max and current
            //foreach (var item in currentMaxLimitResources)
            //{
            //    var currentFixedLimitValue = baseMaxLimits[item.Key];
            //    // item.Value = currentFixedLimitValue;
            //    currentMaxLimitResources[item.Key] = currentFixedLimitValue;

            //    currentResources[item.Key] = 0;

            //}
            currentResourceInfos.Clear();

            // clear other features

        }



    }


}



