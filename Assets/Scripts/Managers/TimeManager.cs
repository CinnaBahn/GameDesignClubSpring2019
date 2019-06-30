using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    private float startTime;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        resetTime();
    }

    public void resetTime()
    {
        startTime = Time.realtimeSinceStartup;
    }

    public float getTime()
    {
        return Time.realtimeSinceStartup - startTime;
    }
}
