using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompMath_SecondContent : MonoBehaviour
{
    public Transform youTrans;
    public Transform waypointFromTrans;
    public Transform waypointToTrans;
    public Transform debugTrans;

    // TODO: tell which passed or not

    /*
     * 
     * 
     *  It might first seem like it's easy to determine if you have passed a waypoint you are heading towards. 
     *  Can't you just use the distance between you and the waypoint 
     *  and if that distance is small, then change waypoint. 
     *  This will work if your character has a small turning radius. 
     *  But if that turning radius is large, 
     *  I promise you that you will see the character turning around the waypoint and never reach it - 
     *  it will just spin around in a circle. But there may be a better way.

This is the basic scene: 
     * 
     * 
     */

    private void Update()
    {
        Vector3 currentWayPoint = Vector3.one;

    }

    private void CheckPath()
    {
        var currentPos = youTrans.position;
    }

}
