using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//This component causes objects it is put on to intiate the death sequence if they collide with a player object.
public class Hurtbox : MonoBehaviour
{
    private GrappleHook grappleHook;
    public GameManager gameManager;

    void Start()
    {
    }


        //Hitting this object initiates the death sequence. For now it just restarts the scene.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameManager.PlayerDeath();
        }
    }
}
