using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut_Logic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int currentFirstValue = 0;
    public int currentSecondValue = 0;

    public (int,int) SRLatch(int firstInput, int secondInput)
    {
        // with NAND gates, create a SR latch

        // NAND Gate: A digital logic gate that outputs false only when all its inputs are true.
        // It is the negation of the AND gate. The truth table for a NAND gate is as follows:
        // Inputs | Output
        // 0, 0   | 1
        // 0, 1   | 1
        // 1, 0   | 1
        // 1, 1   | 0

        if(firstInput == 0 && secondInput == 0)
        {
            // previous result
            return (currentFirstValue, currentSecondValue);
        }
        else if(firstInput == 0 && secondInput == 1)
        {

        }

        switch (firstInput)
        {
            case 0:
                switch (secondInput)
                {
                    case 0:
                        return (0, 0);
                    case 1:
                        return (1, 0);
                }
                break;
            case 1:
                switch (secondInput)
                {
                    case 0:
                        return (0, 1);
                    case 1:
                        Debug.LogError("Invalid input");
                        return (0, 0);
                }
                break;
            default:
                Debug.LogError("Invalid input");
                return (0, 0);
        }
        Debug.LogError("Invalid input");
        return (0, 0);
        
        
    }
}
