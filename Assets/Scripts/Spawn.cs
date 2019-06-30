using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is searched for by the gameManager
public class Spawn : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject.GetComponent<SpriteRenderer>());
    }
}
