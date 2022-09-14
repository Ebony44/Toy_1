using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public static class HelperFunctions
{

    #region isometric control and view
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0,45,0));

    public static Vector3 ToIso(this Vector3 input)
    {
        return _isoMatrix.MultiplyPoint3x4(input);
    }
    #endregion

    #region file IO
    public static void CreateDirectory(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    public static string[] GetFilesfullPath(string folderPath)
    {
        string[] fileNames;
        fileNames = Directory.GetFiles(folderPath);

        
        return fileNames;
    }

    public static string[] FindMatchAfterCertainString(string rawString, string certainStringPat)
    {
        
        // var tempRegex = new Regex(@"(?<=[^{folderPath}\/])(\w+)");
        //var str = "=IIF(IsNothing(folderPath!Metadata1.Value),\"N / A\"," + rawString + "!Metadata4.Value)" + "TestMatFolder" + "!Metadata6.Value"
        //    + "er!Metadata77.Value";
        // var tempRegex = new Regex($@"(?<=[{folderPath}]!)(\w+)");
        // var tempPattern = @"(?<=" + rawString + "!)(\\w+)";
        var tempPattern = @"(?<=" + certainStringPat + ")([0-9]*)";
        
        

        var tempRegex = new Regex(tempPattern);
        var matches = tempRegex.Matches(rawString);
        Debug.Log("raw string is " + rawString);
        foreach (var item in matches)
        {
            Debug.Log("matches are " + item);
        }
        // MatchCollection
        #region
        //var testString = "Mat_1234" + ", Mat_3456";
        //var tempPattern2 = @"(?<=" + "Mat_" + ")([0-9]*)";
        //var tempRegex2 = new Regex(tempPattern2);
        //var matches2 = tempRegex2.Matches(testString);
        //foreach (var item in matches2)
        //{
        //    Debug.Log("matches are " + item.ToString());
        //}
        #endregion

        string[] result = new string[matches.Count];
        for (int i = 0; i < result.Length; i++)
        {
            var temp = matches[i];
            result[i] = matches[i].Value;
        }


        return result;

    }

    //public static string[] FindMatchBeforeCertainString(string rawString, string certainStringPat)
    //{
    //    //var tempPattern = @"(?<=" + certainStringPat + ")([0-9]*)";

    //    //var tempRegex = new Regex(tempPattern);
    //    //var matches = tempRegex.Matches(rawString);

    //    //string[] result = new string[matches.Count];
    //    //return result;
        
    //}


    public static string[] FindMatchAfterCertainString(string[] rawStrings, string certainStringPat)
    {
        string result = string.Empty;
        StringBuilder sb = new StringBuilder();
        foreach (var item in rawStrings)
        {
            if(string.IsNullOrEmpty(item))
            {
                // Debug.Log("[FindMatchAfterCertainString], item is null or empty");
                continue;
            }
            sb.Append(item);
            sb.Append(", ");
        }
        result = sb.ToString();
        return FindMatchAfterCertainString(result, certainStringPat);
    }

    public static int ParseAndReturnNumber(bool bHighestReturn, string[] numberStrings)
    {
        var result = 0;
        var tempValue = 0;
        foreach (var item in numberStrings)
        {
            int.TryParse(item, out tempValue);
            if(bHighestReturn == false)
            {
                if(result >= tempValue)
                {
                    result = tempValue;
                }
            }
            else
            {
                if (result <= tempValue)
                {
                    result = tempValue;
                }
            }
        }
        return result;
    }

    #endregion



#if UNITY_EDITOR
    // Gets value from SerializedProperty - even if value is nested
    public static object GetValue(this UnityEditor.SerializedProperty property)
    {
        object obj = property.serializedObject.targetObject;

        FieldInfo field = null;
        foreach (var path in property.propertyPath.Split('.'))
        {
            var type = obj.GetType();
            field = type.GetField(path);
            obj = field.GetValue(obj);
        }
        return obj;
    }

    // Sets value from SerializedProperty - even if value is nested
    public static void SetValue(this UnityEditor.SerializedProperty property, object val)
    {
        object obj = property.serializedObject.targetObject;

        List<KeyValuePair<FieldInfo, object>> list = new List<KeyValuePair<FieldInfo, object>>();

        FieldInfo field = null;
        foreach (var path in property.propertyPath.Split('.'))
        {
            var type = obj.GetType();
            field = type.GetField(path);
            list.Add(new KeyValuePair<FieldInfo, object>(field, obj));
            obj = field.GetValue(obj);
        }

        // Now set values of all objects, from child to parent
        for (int i = list.Count - 1; i >= 0; --i)
        {
            list[i].Key.SetValue(list[i].Value, val);
            // New 'val' object will be parent of current 'val' object
            val = list[i].Value;
        }
    }
#endif // UNITY_EDITOR

    #region string
    public static string ReplaceString(string originString, string[] cutOffOption)
    {
        var sb = new StringBuilder(originString);
        for (int i = 0; i < cutOffOption.Length; i++)
        {
            sb = sb.Replace(cutOffOption[i], string.Empty);
        }
        return sb.ToString();
    }
    #endregion



}
