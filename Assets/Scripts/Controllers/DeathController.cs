using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void onRestartEarlyEventHandler();

public class DeathController : Controller
{
    public event onRestartEarlyEventHandler onRestartEarly;

    // Update is called once per frame
    void Update()
    {
        if(active)
            if (Input.GetButtonDown("Fire1"))
            {
                if (onRestartEarly != null)
                    onRestartEarly();
            }
    }
}
