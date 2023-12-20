using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreateManager : MonoBehaviour
{

    Dictionary<EItemType, ItemGeneralTypeInfo> itemInfos = new Dictionary<EItemType, ItemGeneralTypeInfo>();

    public void RollItem()
    {

    }

    public void InitializeWeights()
    {
        // TODO: 
        // psuedo code
        // if weight 100 options are 2,
        // 0~99 -> first option
        // 100~199 ->  second option
        var tempRandom = Random.Range(0, 100);

        // test

        ItemWeightInfo lifeTier3 = new ItemWeightInfo();
        lifeTier3.currentType = EItemOptionType.MaxLife;
        lifeTier3.weight = 200;

        ItemWeightInfo lifeTier2 = new ItemWeightInfo();
        lifeTier2.currentType = EItemOptionType.MaxLife;
        lifeTier2.weight = 100;

        List<ItemWeightInfo> testList = new List<ItemWeightInfo>(16);
        testList.Add(lifeTier3);
        testList.Add(lifeTier2);


    }

    public void SortOutWeight(List<ItemWeightInfo> paramList)
    {
        var currentWeight = 0;
        foreach (var currentItem in paramList)
        {
            currentItem.currentWeightRangeMin = currentWeight;
            currentItem.currentWeightRangeMax = currentWeight + currentItem.weight;
            currentWeight += currentItem.weight;
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

public enum EItemType
{
    None = 0,
    BionicJewel,
}
public enum EItemOptionType
{
    None = 0,
    MaxLife,
}
public class ItemGeneralTypeInfo
{
    public EItemType currentType;
    public int totalWeight;
    public List<ItemWeightInfo> weightInfos = new List<ItemWeightInfo>(64);

    // public List<>
}

public class ItemWeightInfo
{
    public EItemOptionType currentType;
    public int weight;

    // modify AFTER instantiate
    public int currentWeightRangeMin;
    public int currentWeightRangeMax;

}
