using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    public GameObject hudPrefab;

    private GameObject hud;
    private Text timeHud;
    private Text livesHud;

    void Start()
    {
        instance = this;
        hud = GameObject.Instantiate(hudPrefab);
        DontDestroyOnLoad(hud);
        timeHud = hud.transform.Find("TimeText").GetComponent<Text>();
        livesHud = hud.transform.Find("LivesText").GetComponent<Text>();

        refreshTimeHud();
        refreshLivesHud();
    }

    void Update()
    {
        refreshTimeHud();
    }

    public void refreshTimeHud() { timeHud.text = StatsManager.instance.getTime().ToString(); }
    public void refreshLivesHud() { livesHud.text = StatsManager.instance.getLives().ToString(); }
}
