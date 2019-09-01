using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookableHighlighter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }
}
