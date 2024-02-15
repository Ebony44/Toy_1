using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotProductForMathPractice : MonoBehaviour
{

    public Transform srcTransform;
    public Transform dstTransform;

    public GameObject srcVisualObject;
    public GameObject dstVisualObject;


    [TestMethod(false)]
    public void GetCurrentDegree()
    {
        FindOutDotProduct(srcTransform.position, dstTransform.position);
    }

    public void FindOutDotProduct(Vector3 src, Vector3 dst)
    {
        srcVisualObject.transform.position = src;
        dstVisualObject.transform.position = dst;
        var modSrc = Vector3.Normalize(src);
        var modDst = Vector3.Normalize(dst);
        var magSrc = Vector3.Magnitude(src);
        var magDst = Vector3.Magnitude(dst);

        var modMagSrc = Vector3.Magnitude(modSrc);
        var modMagDst = Vector3.Magnitude(modDst);


        Debug.Log("mod src and mod dst are " + modSrc + ", " + modDst
            + " mag src and mag dst are " + magSrc + ", " + magDst
            + " mod mag src and mag dst are " + modMagSrc+ ", " + modMagDst);

        // -1 to opposite direction, 0 to perpendicular, 1 to same direction

        // var dotProduct = (src.x * dst.x) + (src.y * dst.y);
        var dotProduct = (modSrc.x * modDst.x) + (modSrc.y * modDst.y) + (modSrc.z * modDst.z);

        // two vector's between degree
        var angle = Mathf.Acos(dotProduct) * 180 / Mathf.PI;

        // get cos from dotproduct / times of magnitude of two vectors
        // var cosAngle = dotProduct / magSrc * magDst;
        var cosResult = dotProduct / modMagSrc * modMagDst;
        

        Debug.Log("current angle is " + angle
            + " dot product is " + dotProduct
            + " cos angle is " + cosResult);
        
    }
}
