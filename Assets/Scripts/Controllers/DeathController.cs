using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : Controller
{

    public Action onRestartEarly;

    private void restartEarly()
    {
        if (onRestartEarly != null)
            onRestartEarly();
    }

    private void OnEnable()
    {
        InputManager.instance.onPrimaryReleased += restartEarly;
    }

    private void OnDisable() {
        InputManager.instance.onPrimaryReleased -= restartEarly;
    }
}
