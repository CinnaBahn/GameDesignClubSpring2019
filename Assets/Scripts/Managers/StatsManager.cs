using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    //time
    private float startTime;
    //lives
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
        resetTime();
        resetLives();
    }

    //time
    public void resetTime() { startTime = GameplayManager.instance.timeOfControlEnabled; }
    public float getTime() { return Time.time - GameplayManager.instance.timeOfControlEnabled; }

    //lives
    public int getLives() { return lives; }
    public void loseLife() { --lives; }
    public void gainLife() { ++lives; }
    public void resetLives() { lives = defaultLives; }
}
