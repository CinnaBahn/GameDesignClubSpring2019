using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapplable : MonoBehaviour
{
    enum GrapplableFSM
    {
        GRAPPLABLE,
        UNGRAPPLABLE,
        GRAPPLED
    }

    private GrapplableFSM state = GrapplableFSM.GRAPPLABLE;
    private static Color highlightColor = Color.cyan;

    public void forceStateChange(bool grapplable)
    {
        if (!grapplable)
        {
            if (state == GrapplableFSM.GRAPPLED)
                PlayerManager.instance.forceRelease();
            state = GrapplableFSM.UNGRAPPLABLE;
        }
        else
            state = GrapplableFSM.GRAPPLABLE;
    }

    public bool isGrapplable()
    {
        return state == GrapplableFSM.GRAPPLABLE;
    }

    public bool grapple()
    {
        if (state == GrapplableFSM.GRAPPLABLE)
        {
            state = GrapplableFSM.GRAPPLED;
            return true;
        }
        else
            return false;
    }

    public void release()
    {
        if (state == GrapplableFSM.GRAPPLED)
            state = GrapplableFSM.GRAPPLABLE;
    }

    public void highlight()
    {
        transform.GetComponent<SpriteRenderer>().color = highlightColor;
        //print("highlighted!");
    }

    public void dehighlight()
    {
        transform.GetComponent<SpriteRenderer>().color = Color.white;
        //print("dehighlighted!");
    }
}
