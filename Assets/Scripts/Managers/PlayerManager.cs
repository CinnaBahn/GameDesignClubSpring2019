using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public GameObject playerPrefab;
    private GameObject player;
    private GrappleHook grapplehook;

    public Action onDeathAnimationFinish;

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
        // listen to GameplayManager for when the player is ordered to die
        GameplayManager.instance.onDeath += die;

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
        //print("oh no i died. i need to animate");
        StartCoroutine(dieAnimation());
    }

    private IEnumerator dieAnimation()
    {
        for (float i = 0; i < 2; i += Time.deltaTime)
        {
            yield return null;
        }
        if (onDeathAnimationFinish != null)
            onDeathAnimationFinish();
    }
}
