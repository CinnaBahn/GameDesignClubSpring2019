using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private GrappleHook grappleHook;
    private PlayerMovement playerMovement;

    void Start()
    {
        grappleHook = gameObject.GetComponent<GrappleHook>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    private int getVerticalInput() { return Input.GetAxis("Vertical").CompareTo(0); }
    private int getHorizontalInput() { return Input.GetAxis("Horizontal").CompareTo(0); }

    void checkSwingingInput()
    {

        if (Input.GetButtonUp("Fire1"))
        {
            grappleHook.release();
            playerMovement.resetSwing();
        }
        else
        {
            int vert = getVerticalInput();
            if (vert == 1)
                grappleHook.contract();
            else if (vert == -1)
                grappleHook.loosen();

            int hor = getHorizontalInput();
            if (hor == 1)
                grappleHook.swingRight();
            else if (hor == -1)
                grappleHook.swingLeft();
            else
                grappleHook.resetSwing();
        }

    }


    void checkFallingInput()
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
            grappleHook.fire( Direction.getDpadDirection() );
        }
        grappleHook.highlightBestHook(Direction.getDpadDirection());

    }


    void Update () {
        if (grappleHook.swinging)
            checkSwingingInput();
        else
            checkFallingInput();
    }
}
