using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Fan : MonoBehaviour
{
    public float strength = 25;
    public float range = 30;
    // Start is called before the first frame update
    void Update()
    {
        if (!Application.isPlaying)
        {
            Transform current = transform.GetChild(0);
            updateZone(current);
            updateStrength(current);

            Transform particles = transform.GetChild(2);
            updateParticles(particles);
        }
    }

    private void updateZone(Transform c)
    {
        Vector3 originalS = c.localScale;
        c.localScale = new Vector3(range, originalS.y, originalS.z);
        c.localPosition = new Vector3(range / 2, 0, 0);
    }

    private void updateStrength(Transform c)
    {
        c.GetComponent<AreaEffector2D>().forceMagnitude = strength;
    }

    private void updateParticles(Transform p)
    {
        ParticleSystem ps = p.GetComponent<ParticleSystem>();
        ps.startSpeed = strength;
        ps.startLifetime = range / strength;
    }


}
