using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class is for windows that can be popped up during the game
public class Window : MonoBehaviour
{
    public GameObject windowPrefab;
    protected GameObject window;

    protected virtual void createWindow() { window = Instantiate<GameObject>(windowPrefab, transform); }
    protected virtual void destroyWindow() { Destroy(window); }
}
