using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    public GameObject hudPrefab;

    private GameObject hud;
    private Text timeHUD;
    private Text livesHUD;
    private Text countdownHUD;

    private bool hudCreated = false;
    private bool countdownFinished = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Countdown.instance.onTick += updateCountdownHUD;
        Countdown.instance.onCountdownReached += hideCountdownHUD;
    }

    private void updateCountdownHUD(float timeLeft)
    {
        countdownHUD.text = timeLeft.ToString();
    }

    private void hideCountdownHUD()
    {
        countdownHUD.text = "GO!!!";
        Destroy(countdownHUD, .5f);
        countdownFinished = true;
    }

    //create a new hud everytime the level reloads
    private void createHUD()
    {
        hud = GameObject.Instantiate(hudPrefab);
        timeHUD = hud.transform.Find("TimeText").GetComponent<Text>();
        livesHUD = hud.transform.Find("LivesText").GetComponent<Text>();
        countdownHUD = hud.transform.Find("CountdownText").GetComponent<Text>();
        hudCreated = true;
    }

    void Update()
    {
        if(!hudCreated){
            createHUD();
            refreshLivesHUD();
        }
        else if (countdownFinished)
            refreshTimeHUD();
    }

    public void refreshTimeHUD() { timeHUD.text = StatsManager.instance.getTime().ToString("#.00"); }
    public void refreshLivesHUD() { livesHUD.text = StatsManager.instance.getLives().ToString(); }
}
