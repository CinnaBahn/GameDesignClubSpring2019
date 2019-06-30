using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {

    private CameraZone zone; //the parent that contains this trigger and the view Camera

    private void Start()
    {
        zone = transform.parent.gameObject.GetComponent<CameraZone>();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    //When the player enters this trigger, add this CameraZone to the PlayerCamera's list of zones.
    private void OnTriggerEnter2D(Collider2D collision) { if(collision.tag == "Player") PlayerCamera.instance.addZone( zone ); }
    private void OnTriggerExit2D(Collider2D collision) { if (collision.tag == "Player") PlayerCamera.instance.removeZone( zone ); }

}
