using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;

public class CSVTestingScript : MonoBehaviour
{
    private void Start()
    {
        Parsing();
        DisplayJoinString();
    }
    public void Parsing()
    {
        var textFile = string.Empty;
        string stringBeforeSplit =
            "ASCENSION 1,4,4,4_3,4_4,4_4_3,4_4_4,4_4_4_3,4_4_4_4,4_4_4_4_3,4_4_4_4_4";
        string[] getStringFromFile = SplitStringWithSeparator(stringBeforeSplit, "\r\n");

        string paramString = stringBeforeSplit;

        var splitStrings = SplitStringWithSeparator(paramString, ",");
        var splitKeyStrings = SplitStringWithUnderBar(splitStrings[0]);

        Debug.Log("asdf");
    }

    public static string[] SplitStringWithUnderBar(string paramString)
    {
        Regex CsvParser = new Regex("_(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        var tempResultString = CsvParser.Split(paramString);

        return tempResultString;
    }
    public static string[] SplitStringWithSeparator(string paramString, string seperator)
    {
        Regex CsvParser = new Regex(seperator + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        var tempResultString = CsvParser.Split(paramString);

        return tempResultString;
    }

    [TestMethod(false)]
    public void DisplayJoinString()
    {
        Dictionary<int, int> tempDic = new Dictionary<int, int>();
        tempDic.Add(3000, 125);
        
        var currentString = string.Join(";", from x in tempDic select string.Format("{0},{1}", x.Key, x.Value));

        Debug.Log(" current string is " + currentString);

    }


}
