using System;
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

    #region sprite and texture creation from outside resources
    public static void CreateTextureFromFile(string filePath, out Texture2D outTexture)
    {
        // Texture2D tex;
        outTexture = null;
        // tex = new Texture2D(4, 4, TextureFormat.BGRA32, false);
        // var tempBool = System.IO.File.Exists(filePath);
        if (File.Exists(filePath))
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            outTexture = new Texture2D(64, 64, TextureFormat.ARGB32, false);

            outTexture.LoadImage(fileData);

        }
        else
        {
            // log error?
        }


    }
    public static Sprite CreateSpriteFromFile(string filePath, ref Texture2D texture)
    {
        CreateTextureFromFile(filePath, out texture);
        // texture = ConvertToPremultipliedAlpha(texture);
        texture = FillColorAlpha(texture);

        // texture = FillColorAlpha(texture);
        // texture.Apply();

        // Texture2D newTex = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        // newTex = FillColorAlpha(newTex);
        // newTex.SetPixels(newTex.GetPixels());
        // newTex.Apply();

        GameObject merchantObj = new GameObject();
        // var rendererComponent = merchantObj.AddComponent<SpriteRenderer>();
        // rendererComponent.sortingOrder = 35;
        // rendererComponent.sprite = 

        var tempSprite =
            Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            // new Vector2(texture.width / 2, texture.height / 2),
            new Vector2(0.5f, 0.5f),
            100f,
            0,
            SpriteMeshType.FullRect
            // Vector2.zero
            );

        // rendererComponent.transform.localPosition = new Vector3(44.5f, 1.74f, -8.5f);
        // merchantObj.name = "Goblin Merchant";

        //var tempSprite =
        //    Sprite.Create(
        //    newTex,
        //    new Rect(0, 0, newTex.width, newTex.height),
        //    new Vector2(newTex.width / 2, newTex.height / 2),
        //    100f,
        //    0,
        //    SpriteMeshType.FullRect
        //    // Vector2.zero
        //    );


        return tempSprite;
    }

    public static Sprite CreateSpriteFromFile(string filePath, ref Texture2D texture, Vector4 border)
    {
        CreateTextureFromFile(filePath, out texture);
        // texture = ConvertToPremultipliedAlpha(texture);
        texture = FillColorAlpha(texture);

        // texture = FillColorAlpha(texture);
        // texture.Apply();

        // Texture2D newTex = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        // newTex = FillColorAlpha(newTex);
        // newTex.SetPixels(newTex.GetPixels());
        // newTex.Apply();

        GameObject merchantObj = new GameObject();
        // var rendererComponent = merchantObj.AddComponent<SpriteRenderer>();
        // rendererComponent.sortingOrder = 35;
        // rendererComponent.sprite = 

        var tempSprite =
            Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            // new Vector2(texture.width / 2, texture.height / 2),
            new Vector2(0.5f, 0.5f),
            100f,
            0,
            SpriteMeshType.FullRect,
            border
            // Vector2.zero
            );

        // tempSprite.border = border;

        // rendererComponent.transform.localPosition = new Vector3(44.5f, 1.74f, -8.5f);
        // merchantObj.name = "Goblin Merchant";

        //var tempSprite =
        //    Sprite.Create(
        //    newTex,
        //    new Rect(0, 0, newTex.width, newTex.height),
        //    new Vector2(newTex.width / 2, newTex.height / 2),
        //    100f,
        //    0,
        //    SpriteMeshType.FullRect
        //    // Vector2.zero
        //    );


        return tempSprite;
    }


    public static Texture2D FillColorAlpha(Texture2D tex2D, Color32? fillColor = null)
    {
        if (fillColor == null)
        {
            fillColor = Color.clear;
        }
        Color32[] fillPixels = new Color32[tex2D.width * tex2D.height];
        for (int i = 0; i < fillPixels.Length; i++)
        {
            fillPixels[i] = (Color32)fillColor;
        }
        tex2D.SetPixels32(fillPixels);
        return tex2D;
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
    public static string ReplaceStringToEmpty(string originString, string[] cutOffOption)
    {
        var sb = new StringBuilder(originString);
        for (int i = 0; i < cutOffOption.Length; i++)
        {
            sb = sb.Replace(cutOffOption[i], string.Empty);
        }
        return sb.ToString();
    }
    #endregion

    public static T GetNextEnum<T>(this T src) where T : struct
    {
        if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

        T[] Arr = (T[])Enum.GetValues(src.GetType());
        int j = Array.IndexOf<T>(Arr, src) + 1;
        return (Arr.Length == j) ? Arr[0] : Arr[j];
    }


    public static T GetRandomValueWithExcluding<T>(this T src)
    {
        return src;
    }
    public static void RemoveListFromList<T>(List<T> listToDelete, List<T> srcList)
    {
        for (int i = 0; i < listToDelete.Count; i++)
        {
            srcList.Remove(listToDelete[i]);
        }
    }


}
