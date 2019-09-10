using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public static Countdown instance;
    public int timeUntilControlEnabled = 3;
    public Action onCountdownReached;
    public Action<float> onTick;

    private void Awake()
    {
        if (instance)
            Destroy(this);
        else
            instance = this;

        StartCoroutine(startCountdown());
    }

    private IEnumerator startCountdown()
    {
        for (timeUntilControlEnabled = 3; timeUntilControlEnabled > 0; timeUntilControlEnabled--)
        {
            // notify those listening to onTick
            if (onTick != null)
                onTick(timeUntilControlEnabled);
            yield return new WaitForSeconds(.5f);
        }

        // notify those waiting for countdown to finish
        if (onCountdownReached != null)
            onCountdownReached();
    }

}
