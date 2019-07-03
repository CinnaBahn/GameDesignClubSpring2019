using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public GameObject playerPrefab;
    private GameObject player;
    private GrappleHook grapplehook;

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;
        player = GameObject.Instantiate(playerPrefab);
        grapplehook = player.GetComponent<GrappleHook>();
    }

    public void resetPosition() { player.transform.position = FindObjectOfType<Spawn>().transform.position; }

    private void Start()
    {
        resetPosition();
    }

    public GameObject getPlayer() { return player; }

    //force the player to release the grapple hook
    public void forceRelease()
    {
        grapplehook.release();
        //print("let go!");
    }

    public void die(ECauseOfDeath cause)
    {
        StatsManager.instance.loseLife();
        if (StatsManager.instance.getLives() == 0)
        {
            GameplayManager.instance.gameOver();
            StatsManager.instance.resetLives();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            StatsManager.instance.resetTime();
        }
    }
}
