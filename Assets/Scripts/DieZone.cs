using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieZone : MonoBehaviour
{
    public ECauseOfDeath cause;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager.instance.die(cause);
    }
}
