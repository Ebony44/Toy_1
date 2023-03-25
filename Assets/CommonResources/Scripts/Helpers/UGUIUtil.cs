using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UGUIUtil
{
    public static void SetAlpha (this Graphic target, float alpha) {
        if(target == null)
            return;

        var color = target.color;
        color.a = alpha;
        target.color = color;
    }

    public static void SetAlpha (this Graphic target, float alpha, bool containChild) {
        if(target == null)
            return;

        var color = target.color;
        color.a = alpha;
        target.color = color;

        if(containChild){
            foreach(var ig in target.GetComponentsInChildren<Graphic>())
            {
                if(ig == null)
                    continue;
                var c = ig.color;
                c.a = alpha;
                ig.color = c;
            }
        }


    }
    



}
