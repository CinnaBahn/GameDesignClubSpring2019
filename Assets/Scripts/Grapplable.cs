using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapplable : MonoBehaviour
{
    private bool currentlyGrappleable = true;
    private bool grappled = false;

    //public bool isCurrentlyGrapplable() { return currentlyGrappleable; }
    public void setCurrentlyGrapplable(bool g)
    {
        currentlyGrappleable = g;
        if (grappled && !currentlyGrappleable)
            PlayerManager.instance.forceRelease();
    }
    public bool grapple()
    {
        return grappled = currentlyGrappleable;
    }
    public void release()
    {
        grappled = false;
    }
}
