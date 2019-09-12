using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    public float fireAfterSecs = .25f;
    private Rigidbody2D rb;
    private PlayerMovement pm;
    private EDirection swingDir = EDirection.CENTER;
    // Start is called before the first frame update
    void Start()
    {
        pm = PlayerManager.instance.getPlayer().GetComponent<PlayerMovement>();
        rb = PlayerManager.instance.getPlayer().GetComponent<Rigidbody2D>();
        StartCoroutine(dropFire(fireAfterSecs));
    }


    private void Update()
    {
        swingDir = rb.velocity.x > 0 ? EDirection.RIGHT : EDirection.LEFT;
        if (Mathf.FloorToInt(rb.velocity.magnitude) == 0)
            if (swingDir == EDirection.LEFT)
                    pm.swingLeft();
            else if(swingDir == EDirection.RIGHT)
                    pm.swingRight();
    }

    private IEnumerator dropFire(float s)
    {
        for (float i = 0; i < s; i += Time.deltaTime)
        {
            yield return null;
        }
        PlayerManager.instance.getPlayer().GetComponent<GrappleHook>().fire();
    }

}
