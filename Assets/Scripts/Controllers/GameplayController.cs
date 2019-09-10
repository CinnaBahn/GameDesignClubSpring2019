using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : Controller {

    enum GameplayFSM
    {
        HOOKED,
        FREE
    }

    private GameplayFSM state = GameplayFSM.FREE;
    
    public Action onGrappleFired, onGrappleReleased;
    public Action onGrappleContracted, onGrappleLoosened;
    public Action onSwingLeft, onSwingRight, onSwingRelax;
    public Action onParrySpin;

    // primary
    private void primaryPressed()
    {
        if (state == GameplayFSM.FREE)
            if (onGrappleFired != null)
                onGrappleFired();
    }
    private void primaryReleased()
    {
        if (state == GameplayFSM.HOOKED)
            if (onGrappleReleased != null)
                onGrappleReleased();
    }

    //secondary
    private void secondaryPressed()
    {
        //if (state == GameplayFSM.FREE)
            if (onParrySpin != null)
                onParrySpin();
    }

    // dpad
    private void allDirectionsReleased()
    {
        if (state == GameplayFSM.HOOKED)
            if (onSwingRelax != null)
                onSwingRelax();
    }
    private void upPressed()
    {
        if (state == GameplayFSM.HOOKED)
            if (onGrappleContracted != null)
                onGrappleContracted();
    }
    private void downPressed()
    {
        if (state == GameplayFSM.HOOKED)
            if (onGrappleLoosened != null)
                onGrappleLoosened();
    }
    private void leftPressed()
    {
        if (state == GameplayFSM.HOOKED)
            if (onSwingLeft != null)
                onSwingLeft();
    }
    private void rightPressed()
    {
        if (state == GameplayFSM.HOOKED)
            if (onSwingRight != null)
                onSwingRight();
    }

    private void OnEnable()
    {
        // grapple
        InputManager.instance.onPrimaryPressed += primaryPressed;
        InputManager.instance.onPrimaryReleased += primaryReleased;

        // parry
        InputManager.instance.onSecondaryPressed += secondaryPressed;

        // dpad
        InputManager.instance.onAllDirectionsReleased += allDirectionsReleased;
        InputManager.instance.onUpPressed += upPressed;
        InputManager.instance.onDownPressed += downPressed;
        InputManager.instance.onLeftPressed += leftPressed;
        InputManager.instance.onRightPressed += rightPressed;
    }

    private void OnDisable()
    {
        // grapple
        InputManager.instance.onPrimaryPressed -= primaryPressed;
        InputManager.instance.onPrimaryReleased -= primaryReleased;

        // parry
        InputManager.instance.onSecondaryPressed -= secondaryPressed;

        // dpad
        InputManager.instance.onAllDirectionsReleased -= allDirectionsReleased;
        InputManager.instance.onUpPressed -= upPressed;
        InputManager.instance.onDownPressed -= downPressed;
        InputManager.instance.onLeftPressed -= leftPressed;
        InputManager.instance.onRightPressed -= rightPressed;
    }

    void Start()
    {
        GrappleHook g = PlayerManager.instance.getPlayer().GetComponent<GrappleHook>();
        g.onSuccessfulGrapple += toHooked;
        g.onRelease += toFree;
    }

    private void toHooked(Grapplable hookedOn) { state = GameplayFSM.HOOKED; }
    private void toFree(Grapplable releasedFrom) { state = GameplayFSM.FREE; }

    void doFreeInput()
    {
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
}
