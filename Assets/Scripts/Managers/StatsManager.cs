using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    //time
    private float startTime;
    //lives
    public int defaultLives = 3;
    private int lives;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        resetTime();
        lives = defaultLives;
    }

    //time
    public void resetTime() { startTime = Time.timeSinceLevelLoad; }
    public float getTime() { return Time.timeSinceLevelLoad - startTime; }

    //lives
    public int getLives() { return lives; }
    public void loseLife() {
        print("\nlives before: " + lives);
        --lives;
        print("lives after: " + lives);
        HUDManager.instance.refreshLivesHud();
    }
    public void gainLife() {
        ++lives;
        HUDManager.instance.refreshLivesHud();
    }
    public void resetLives() {
        lives = defaultLives;
        HUDManager.instance.refreshLivesHud();
    }
}
