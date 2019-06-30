using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;

    public bool spawnAtStart = false;
    public float lifetime = 0.0f;
    public int maxActive = 0;

    public bool loop = false;
    public float loopInterval = 1.0f;

    private Queue<GameObject> instances;

    private void Start()
    {
        instances = new Queue<GameObject>();

        if (spawnAtStart)
            spawnSingle();
        if (loop)
            startLoop(loopInterval);
    }

    //spawn a single instance and despawn old ones if maxCount is exceeded
    public void spawnSingle() {
        GameObject temp = GameObject.Instantiate(prefab);
        temp.AddComponent<Destroyer>(); //add a Destroyer so that its functions can be called to destroy this instance eventually
        temp.transform.position = transform.position;
        instances.Enqueue(temp);

        if (maxActiveExceeded())
            despawnExcess();

        //if has lifetime, start counting until it's destroyed
        if (lifetime > 0)
            StartCoroutine(countdownAndDestroy(temp));
    }

    //check if more instances than desired
    private bool maxActiveExceeded() { return maxActive > 0 && instances.Count > maxActive; }

    //get rid of instances if queue is full
    private void despawnExcess()
    {
        for (int i = instances.Count - maxActive; i > 0; i--)
            instances.Dequeue().GetComponent<Destroyer>().shrinkAndDestroy(1);
    }

    //start spawning instances on a constance interval
    public void startLoop(float li)
    {
        stopLoop();
        loop = true;
        loopInterval = li;
        StartCoroutine(waitAndSpawn());
    }

    //stop spawning instances on constant interval
    public void stopLoop()
    {
        loop = false;
        StopCoroutine(waitAndSpawn());
    }

    private IEnumerator waitAndSpawn()
    {
        while(true)
        {
            spawnSingle();
            yield return new WaitForSeconds(loopInterval);
        }
    }

    private IEnumerator countdownAndDestroy(GameObject toDestroy)
    {
        yield return new WaitForSeconds(lifetime);
        toDestroy.GetComponent<Destroyer>().shrinkAndDestroy(1);
    }
}
