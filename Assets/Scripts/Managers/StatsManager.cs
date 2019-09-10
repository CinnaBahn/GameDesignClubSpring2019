using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    private float startTime;
    private float finalTime;
    private bool timeStopped;
    public int defaultLives = 10;
    private int lives;

    private void Awake()
    {
        if (instance)
            Destroy(this);
        else
            instance = this;
    }

    void Start()
    {
        resetLives();
        Countdown.instance.onCountdownReached += setStartTime;
        Goal.instance.onGoalReached += stopTime;
    }

    private void setStartTime()
    {
        startTime = Time.time;
    }

    //time
    public float getTime()
    {
        if (!timeStopped)
            return Time.time - startTime;
        else
            return finalTime;
    }
    public void stopTime()
    {
        finalTime = getTime();
        timeStopped = true;
    }

    //lives
    public int getLives() { return lives; }
    public void loseLife() { --lives; }
    public void gainLife() { ++lives; }
    public void resetLives() { lives = defaultLives; }
}
