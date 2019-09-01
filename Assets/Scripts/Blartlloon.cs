using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blartlloon : Parryable
{
    public float lift = -1;
    public bool randomColor = true;
    public Color color;
    private Vector2 originalSize;


    private void Awake()
    {
        if (randomColor) { color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)); }
        gameObject.GetComponent<SpriteRenderer>().color = color;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = -lift;

        originalSize = transform.localScale;
    }

    public override bool onParry(Transform initiator)
    {

        bool success = base.onParry(initiator);

        propelBalloon(initiator);

        StopAllCoroutines();
        StartCoroutine(boingAnimation(20));

        return success;
    }

    private void propelBalloon(Transform initiator)
    {
        Vector2 f = 25 * (transform.position - initiator.position);
        GetComponent<Rigidbody2D>().AddForce(f);
    }


    /* private void OnCollisionEnter2D(Collision2D collision)
    {
        StopAllCoroutines();
        StartCoroutine(boingAnimation(collision.relativeVelocity.magnitude / 2));
    } */

    private IEnumerator boingAnimation(float strength)
    {
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            float n = t * strength + .5f;
            transform.localScale = Vector2.one * (-Mathf.Cos(n)/(n) + 1);
            yield return null;
        }
        transform.localScale = originalSize;
    }
}
