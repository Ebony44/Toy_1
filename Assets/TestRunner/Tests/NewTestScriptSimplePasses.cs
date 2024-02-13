using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScriptSimplePasses
{
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePassesSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptSimplePassesWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [Test]
    public void StringWriterTest()
    {
        // Arrange
        var stringWriterUnderTest = new StringWriter();
        stringWriterUnderTest.NewLine = "\\n";
        var testStringA = "I am testing";
        var testStringB = "with new line";

        // Act
        stringWriterUnderTest.WriteLine(testStringA);
        stringWriterUnderTest.WriteLine(testStringB);

        // Assert
        Assert.AreEqual("I am testing\\nwith new line\\n", stringWriterUnderTest.ToString());
    }

    [Test]
    public void ListRemoveFromList()
    {
        List<int> tempList = new List<int> { 1, 2, 3, 4 };
        List<int> listToDelete = new List<int> { 2, 3 };

        RemoveListFromList<int>(listToDelete, tempList);

        Assert.IsFalse(tempList.Contains(2));
        Debug.Log("temp list first and second " + tempList[0] + ", " + tempList[1]);

    }
    public static void RemoveListFromList<T>(List<T> listToDelete, List<T> srcList)
    {
        for (int i = 0; i < listToDelete.Count; i++)
        {
            srcList.Remove(listToDelete[i]);
        }
    }

}
