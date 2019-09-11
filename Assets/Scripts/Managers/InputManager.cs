using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class calls events when different buttons are pressed.
public class InputManager : MonoBehaviour
{

    public Action onPrimaryPressed, onSecondaryPressed;
    public Action onPrimaryReleased, onSecondaryReleased;

    public Action onDirectionChanged;
    public Action onUpPressed, onDownPressed, onLeftPressed, onRightPressed;
    public Action onAllDirectionsReleased, onUpReleased, onDownReleased, onLeftReleased, onRightReleased;

    public static InputManager instance;

    private void Awake()
    {
        instance = this;
    }

    private EDirection lastDir = EDirection.CENTER;
    private void Update()
    {
        // primary button (grapple hook and OKing menues)
        if (Input.GetButtonDown("Fire1"))
        {
            if (onPrimaryPressed != null)
                onPrimaryPressed();
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            if (onPrimaryReleased != null)
                onPrimaryReleased();
        }

        // secondary button (parry and cancel)
        if (Input.GetButtonDown("Fire2"))
        {
            if (onSecondaryPressed != null)
                onSecondaryPressed();
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            if (onSecondaryReleased != null)
                onSecondaryReleased();
        }

        // dpad
        EDirection currentDir = Direction.getAimingDirection();
        if (currentDir != lastDir)
            if (onDirectionChanged != null)
                onDirectionChanged();
        switch (currentDir)
        {
            case EDirection.CENTER:
                if (onAllDirectionsReleased != null)
                    onAllDirectionsReleased();
                break;
            case EDirection.UP:
                if(onUpPressed != null)
                    onUpPressed();
                break;
            case EDirection.RIGHT:
                if (onRightPressed != null)
                    onRightPressed();
                break;
            case EDirection.DOWN:
                if (onDownPressed != null)
                    onDownPressed();
                break;
            case EDirection.LEFT:
                if (onLeftPressed != null)
                    onLeftPressed();
                break;
            case EDirection.UPRIGHT:
                if (onUpPressed != null)
                    onUpPressed();
                if (onRightPressed != null)
                    onRightPressed();
                break;
            case EDirection.DOWNRIGHT:
                if (onDownPressed != null)
                    onDownPressed();
                if (onRightPressed != null)
                    onRightPressed();
                break;
            case EDirection.DOWNLEFT:
                if (onDownPressed != null)
                    onDownPressed();
                if (onLeftPressed != null)
                    onLeftPressed();
                break;
            case EDirection.UPLEFT:
                if (onUpPressed != null)
                    onUpPressed();
                if (onLeftPressed != null)
                    onLeftPressed();
                break;
        }
        lastDir = currentDir;
    }
}
