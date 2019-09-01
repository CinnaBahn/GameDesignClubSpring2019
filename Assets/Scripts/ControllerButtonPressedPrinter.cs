using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// credit to ayrrik
// https://answers.unity.com/questions/411950/find-out-if-any-button-on-any-gamepad-has-been-pre.html
public class ControllerButtonPressedPrinter : MonoBehaviour
{
    void Update()
    {
        for (int controller = 1; controller < 3; controller++)
            for (int button = 0; button < 20; button++)
                if (Input.GetKeyDown("joystick " + controller + " button " + button))
                    print("joystick " + controller + " button " + button);
    }
}
