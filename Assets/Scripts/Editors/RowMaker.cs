using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// this class duplicates a prefab in a row, useful for rows of hooks that need to be evenly spaced
[ExecuteAlways]
public class RowMaker : MonoBehaviour
{
    public GameObject prefab;
    public int copies = 2;
    public float spacing = 5;
    public float angle = 0;
    public float twist = 10;

    private List<GameObject> copyList;

    // effectively locks copies in place, restricting the level designer to only modify their positions by adjusting the RowMaker script
    private void Update()
    {
        if (!Application.isPlaying)
            preview();
    }

    // add/delete a single copy per frame (approaching copies specified in RowMaker) and update all copies' positions
    public void preview()
    {
        updateCopies();
        updatePositions();
    }

    // delete or add copies 1 per frame until there we have have the right amount of copies
    public void updateCopies()
    {
        int oldCopies = copyList.Count;
        if (copies > oldCopies)
            addCopy();
        else if (copies < oldCopies)
            removeCopy();
    }

    public void updatePositions()
    {
        for (int i = 0; i < copyList.Count; i++)
        {
            float deg = angle * Mathf.Deg2Rad;
            float x = (spacing * i) * Mathf.Cos(deg);
            float y = (spacing * i) * Mathf.Sin(deg);

            //twist
            //x *= Mathf.Cos(twist * i);
            //y *= Mathf.Sin(twist * i);

            copyList[i].transform.position = transform.position + new Vector3(x, y);
        }
    }

    // destroy the copies created during the last edit session (copyList's ref to them was lost, so copyList doesn't know these copies already exist and will create more than desired)
    private void wipeStrayChildren()
    {
        for (int i = transform.childCount; i > 0; i--)
            DestroyImmediate(transform.GetChild(0).gameObject);
    }

    // destroy the preview created during the last edit session and add fresh copies in
    public void clean()
    {
        wipeStrayChildren();

        copyList = new List<GameObject>();
        for (int i = 0; i < copies; i++)
            addCopy();
    }

    // remove known copy from copyList and delete it
    private void removeCopy()
    {
        GameObject toDestroy = copyList[copyList.Count - 1];
        copyList.Remove(toDestroy);
        DestroyImmediate(toDestroy);
    }

    // create copy and add to copyList
    private void addCopy()
    {
        GameObject toAdd = GameObject.Instantiate(prefab, transform);
        copyList.Add(toAdd);
        DestroyImmediate(toAdd.GetComponent<RowMaker>());
    }
}
