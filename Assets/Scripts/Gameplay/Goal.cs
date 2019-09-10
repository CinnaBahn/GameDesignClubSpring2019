using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static Goal instance;
    public Action onGoalReached;
    private bool alreadyReached = false;

    private void Awake()
    {
        instance = this;
    }

    // only collides with player because collider should be on "DangerLayer"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // call for any listeners
        if (!alreadyReached)
        {
            if (onGoalReached != null)
                onGoalReached();
            alreadyReached = true;
        }
    }
}
