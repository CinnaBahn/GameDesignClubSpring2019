using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionMover : Mover
{
    public bool activeOnStart = true;
    public float duration = 1;

    private Vector2 origin;
    public Vector2 destination;
    public bool offset = true;

    public bool loop = false;
    private int dir = 1; //whether going towards dest or orig

    public bool oneshot = true;
    private bool done = false;

    void Start()
    {
        origin = transform.position;
        destination = offset ? origin + destination : destination; //if destination is offset, apply it; otherwise leave it be
        if (activeOnStart)
            move();
    }

    public override void move()
    {
        StopAllCoroutines();
        StartCoroutine("moveToDest");
    }

    private IEnumerator moveToDest()
    {
        float t = 0;

        while (true)
        {
            if (!done)
            {
                transform.position = Vector2.Lerp(origin, destination, t);
                t += Time.deltaTime / duration * dir;
            }

            if (loop)
            {
                if (t > 1)
                    dir = -1;
                else if (t < 0)
                    dir = 1;
            }
            else if (t > 1)
            {
                StopCoroutine("moveToDest");
                t = 0;
                done = oneshot; //if oneshot, set done to true
            }

            yield return null;
        }
    }
}
