using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach this to a GameObject you want to destroy and call an appropriate method
public class Destroyer : MonoBehaviour
{
    public void shrinkAndDestroy(float duration)
    {
        Grapplable g = GetComponentInChildren<Grapplable>();
        if (g)
            g.setCurrentlyGrapplable(false);

        StartCoroutine(shrink(duration));
    }

    private IEnumerator shrink(float duration)
    {
        Vector3 originalSize = transform.localScale;
        for(float t = duration; t > 0; t -= Time.deltaTime)
        {
            transform.localScale = originalSize * t / duration;
            yield return null;
        }
        Destroy(gameObject);
    }
}
