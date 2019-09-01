using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// this class duplicates a prefab in a row, useful for rows of hooks that need to be evenly spaced
[ExecuteAlways]
[System.Serializable]
public class RowMaker : MonoBehaviour
{
    public GameObject prefab;
    public int copies = 2;
    public float spacing = 5;
    public float angle = 0;
    public float twist = 0;

    [SerializeField]
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
        if (copyList == null)
            clean();
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
    private void wipeStrayCopies()
    {
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject copy = transform.GetChild(i).gameObject;
            if (!copyList.Contains(copy))
            {
                print("wipe");
                DestroyImmediate(copy);
            }
        }
    }

    // if there are copies leftover from last time that copyList lost reference to, add them to copyList instead of deleting them and making more
    private void recoverStrayCopies()
    {
        for (int i = 0; i < copies && i < transform.childCount; i++)
        {
            GameObject copy = transform.GetChild(i).gameObject;
            if (!copyList.Contains(copy))
            {
                print("recover!");
                addCopy(copy);
            }
        }

    }

    // destroy the preview created during the last edit session and add fresh copies in
    public void clean()
    {
        print("clean!");
        copyList = new List<GameObject>();

        recoverStrayCopies();
        wipeStrayCopies();

        for (int i = transform.childCount; i < copies; i++)
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
        string path = AssetDatabase.GetAssetPath(prefab);
        //remove "Assets/Resources/" and ".prefab" from the path
        path = path.Replace("Assets/Resources/", "");
        path = path.Replace(".prefab", "");
        print(path);

        GameObject copy = Instantiate(Resources.Load(path, typeof(GameObject)), transform) as GameObject;
        //GameObject copy = GameObject.Instantiate(Resources.Load("Hook", typeof(GameObject)), transform) as GameObject;
        //GameObject copy = GameObject.Instantiate(prefab, transform);
        addCopy(copy);
    }

    private void addCopy(GameObject copy)
    {
        copyList.Add(copy);
    }
}
