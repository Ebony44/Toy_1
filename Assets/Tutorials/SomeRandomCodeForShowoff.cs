using System.Collections;

using System.Collections.Generic;

using UnityEngine;


public class SomeRandomCodeForShowoff : MonoBehaviour

{

    [Header("Targets")]

    [SerializeField] private Transform LTarget; //Foot points

    [SerializeField] private Transform RTarget; //Foot points

    [Header("Raycasts")]

    [SerializeField] private Transform LeftRaycast; //Raycasts to figure out where the feet should be placed in the y axis (hight)

    [SerializeField] private Transform RightRaycast; //Raycasts to figure out where the feet should be placed in the y axis (hight)

    [Header("Distance Config")]

    [SerializeField] private float maxDistL = 0.3f; //Max distance that the body can move

    [SerializeField] private float maxDistR = 0.3f; //Max distance that the body can move

    [Header("Step Config")]

    [SerializeField] private float StepRange = 0.2f; //By how much should the foot go infront of the other foot

    [SerializeField] private float Stepheight = 0.18f; //How much the foot will go up when moving to the next point



    private float DistR; //The actual distance between the feet

    private float DistL; //The actual distance between the feet


    RaycastHit hit;


    GameObject indicatorL; //Hight & Distance indicators

    GameObject indicatorR; //Hight & Distance indicators

    void Start()

    {

        indicatorL = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        indicatorL.transform.localScale = Vector3.one * 0f;

        indicatorL.layer = LayerMask.NameToLayer("Ignore Raycast");

        indicatorR = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        indicatorR.transform.localScale = Vector3.one * 0f;

        indicatorR.layer = LayerMask.NameToLayer("Ignore Raycast");


    }


    // Update is called once per frame

    void Update()

    {

        DistL = Vector3.Distance(LTarget.position, indicatorL.transform.position);


        DistR = Vector3.Distance(RTarget.position, indicatorR.transform.position);


        LTarget.rotation = indicatorL.transform.rotation;

        RTarget.rotation = indicatorR.transform.rotation;


        if (DistL > maxDistL)

        {

            LTarget.position = indicatorL.transform.position;

            maxDistL = 0.4f;

        }

        else if (DistR > maxDistR)

        {

            RTarget.position = indicatorR.transform.position;

        }


        float Range = 0.5f;


        if (Physics.Raycast(LeftRaycast.position, -Vector3.up, out hit, Range))

        {

            indicatorL.transform.position = hit.point;

        }

        if (Physics.Raycast(RightRaycast.position, -Vector3.up, out hit, Range))

        {

            indicatorR.transform.position = hit.point;

        }

    }

    /*IEnumerator WalkR()

    {

    RTarget.position = new Vector3(RTarget.position.x, RTarget.position.y + Stepheight, LTarget.position.z + StepRange);

    yield return new WaitForSeconds(0.1f);

    RTarget.position = new Vector3(RTarget.position.x, indicatorR.transform.position.y + 0.02f, LTarget.position.z + StepRange);

    }


    IEnumerator WalkL()

    {

    LTarget.position = new Vector3(LTarget.position.x, LTarget.position.y + Stepheight, RTarget.position.z + StepRange);

    yield return new WaitForSeconds(0.1f);

    LTarget.position = new Vector3(LTarget.position.x, indicatorL.transform.position.y + 0.02f, RTarget.position.z + StepRange);

    }*/

}

//