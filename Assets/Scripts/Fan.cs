using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Fan : MonoBehaviour
{
    [Range(1, 50)]
    public int blowStrength = 3;
    [Range(1, 100)]
    public int blowRange = 10;
    [Range(1, 10)]
    public int fanWidth = 1;

    void Update()
    {
        //done after value update in editor inspector, doesn't run during game
        if (!Application.isPlaying)
        {
            Transform current = transform.GetChild(0);
            ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
            Transform fanCasing = transform.GetChild(1);

            //adjust scaling of vars
            int strengthScale = blowStrength * 10;
            int rangeScale = blowRange * 5;
            float widthScale = fanWidth * 10;

            //current
            current.GetComponent<AreaEffector2D>().forceMagnitude = strengthScale; //strength
            current.localScale = new Vector3(rangeScale, widthScale, 1); //scale
            current.localPosition = new Vector3(rangeScale / 2, 0, 0); //pos

            //particles
            particles.startSpeed = strengthScale; //velocity
            particles.startLifetime = rangeScale / strengthScale; //lifetime
            particles.transform.localScale = new Vector3(1, 1, 1); //scale (locked)
            particles.transform.position = gameObject.transform.position; //pos (locked)

            //casing
            fanCasing.localScale = new Vector3(3, widthScale, 1); //scale
            fanCasing.localPosition = new Vector3(0, 0, 0); //pos (locked)
        }
    }
}
