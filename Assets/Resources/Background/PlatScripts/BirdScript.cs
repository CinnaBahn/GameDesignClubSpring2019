using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public float lift = 1;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = -lift;
    }
	
	void OnBecameVisible()
    {
        enabled = true;
    }
	
}
