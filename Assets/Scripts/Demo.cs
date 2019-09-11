using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    public float fireAfterSecs = .25f;
    private Rigidbody2D rb;
    private EDirection swingDir = EDirection.CENTER;
    // Start is called before the first frame update
    void Start()
    {
        rb = PlayerManager.instance.getPlayer().GetComponent<Rigidbody2D>();
        StartCoroutine(dropFire(fireAfterSecs));
    }

    

    private void Update()
    {
        swingDir = rb.velocity.x > 0 ? EDirection.RIGHT : EDirection.LEFT;
        if (Mathf.FloorToInt(rb.velocity.magnitude) == 0)
            switch (swingDir)
            {
                case EDirection.LEFT:
                    Controller.gameplayController.onSwingLeft();
                    //print("gonna swing LEFT!");
                    break;
                case EDirection.RIGHT:
                    Controller.gameplayController.onSwingRight();
                    //print("gonna swing RIGHT!");
                    break;
            }
    }

    private IEnumerator dropFire(float s)
    {
        for (float i = 0; i < s; i += Time.deltaTime)
        {
            yield return null;
        }
        Controller.gameplayController.onGrappleFired();
        //StartCoroutine(swing(EDirection.LEFT));
    }

    private IEnumerator swing(EDirection dir)
    {
        switch (dir)
        {
            case EDirection.LEFT:
                Controller.gameplayController.onSwingLeft();
                break;
            case EDirection.RIGHT:
                Controller.gameplayController.onSwingRight();
                break;
        }
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            yield return null;
        }
    }
}
