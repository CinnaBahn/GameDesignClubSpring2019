using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this class is for windows that can be popped up during the game
public class Window : MonoBehaviour
{
    public GameObject windowPrefab;
    protected GameObject window;

    protected virtual void createWindow() { window = Instantiate<GameObject>(windowPrefab, transform); }
    protected virtual void destroyWindow() { Destroy(window); }

    protected Text getTextComponent(string name)
    {
        foreach (Text t in window.transform.GetComponentsInChildren<Text>())
            if (t.gameObject.name == name)
                return t;

        return null;
    }
}
