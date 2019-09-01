using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void onGrappleFiredEventHandler();
public delegate void onGrappleReleasedEventHandler();
public delegate void onGrappleContractedEventHandler();
public delegate void onGrappleLoosenedEventHandler();

public delegate void onSwingLeftEventHandler();
public delegate void onSwingRightEventHandler();
public delegate void onSwingRelaxEventHandler();

public delegate void onParrySpinEventHandler();

public class GameplayController : Controller {

    //private GrappleHook grappleHook;
    enum GameplayFSM
    {
        HOOKED,
        FREE
    }

    private GameplayFSM state = GameplayFSM.FREE;

    public event onGrappleFiredEventHandler onGrappleFired;
    public event onGrappleReleasedEventHandler onGrappleReleased;

    public event onGrappleContractedEventHandler onGrappleContracted;
    public event onGrappleLoosenedEventHandler onGrappleLoosened;

    public event onSwingLeftEventHandler onSwingLeft;
    public event onSwingRightEventHandler onSwingRight;
    public event onSwingRelaxEventHandler onSwingRelax;

    public event onParrySpinEventHandler onParrySpin;

    void Start()
    {
        GrappleHook g = PlayerManager.instance.getPlayer().GetComponent<GrappleHook>();
        g.onSuccessfulGrapple += new onSuccessfulGrappleEventHandler(toHooked);
        g.onRelease += new onReleaseEventHandler(toFree);
    }

    private void toHooked(Grapplable hookedOn) { state = GameplayFSM.HOOKED; }
    private void toFree(Grapplable releasedFrom) { state = GameplayFSM.FREE; }

    void doHookedInput()
    {

        if (Input.GetButtonUp("Fire1"))
        {
            if (onGrappleReleased != null)
                onGrappleReleased();
        }
        else
        {
            int vert = getVerticalInput();
            if (vert == 1)
                onGrappleContracted();
            else if (vert == -1)
                onGrappleLoosened();

            int hor = getHorizontalInput();
            if (hor == 1)
                onSwingRight();
            else if (hor == -1)
                onSwingLeft();
            else
                onSwingRelax();
        }

    }


    void doFreeInput()
    {

        /*
        switch (getDpadDirection())
        {
            case (1): //right pressed
                break;
            case (4): //up-right pressed
                break;
            case (3): //up pressed
                break;
            case (2): //up-left pressed
                break;
            case (-1): //left pressed
                break;
            case (-4): //down-left pressed
                break;
            case (-3): //down pressed
                break;
            case (-2): //down-right pressed
                break;
        }*/
        if (Input.GetButtonDown("Fire1"))
        {
            if (onGrappleFired != null)
                onGrappleFired();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (onParrySpin != null)
                onParrySpin();
        }

        // DEBUG!!!!!!!!!!!!!!!!
        if (Input.GetButtonDown("Fire3"))
        {
            PlayerManager.instance.resetPosition();
        }

    }


    void Update () {
        if(active)
            if (state == GameplayFSM.HOOKED)
                doHookedInput();
            else if(state == GameplayFSM.FREE)
                doFreeInput();
    }
}
