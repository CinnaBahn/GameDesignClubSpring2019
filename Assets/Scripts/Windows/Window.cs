using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is for windows that can be popped up during the game.
/// </summary>
public abstract class Window : MonoBehaviour, IControllable
{
    /// <summary>
    /// The prefab to instantiate upon CreateWindow().
    /// </summary>
    public GameObject WindowPrefab;

    /// <summary>
    /// The Window Prefab-instantiated GameObject.
    /// </summary>
    protected GameObject window;

    public Action OnWindowCreated;
    public Action OnWindowDestroy;

    /// <inheritdoc/>
    public abstract void EnableControls();

    /// <inheritdoc/>
    public abstract void DisableControls();

    /// <summary>
    /// Instantiates WindowPrefab and stores in Window.
    /// </summary>
    protected virtual void CreateWindow()
    {
        this.window = Instantiate<GameObject>(this.WindowPrefab, this.transform);
        if (this.OnWindowCreated != null)
            this.OnWindowCreated();
    }

    /// <summary>
    /// Destroys the Window GameObject.
    /// </summary>
    protected virtual void DestroyWindow()
    {
        if (this.OnWindowDestroy != null)
            this.OnWindowDestroy();
        Destroy(this.window);
    }

    /// <summary>
    /// Retrieves the Text Object from a child within the window GameObject.
    /// </summary>
    /// <param name="name">The name of the GameObject whose Text we want to retrieve.</param>
    /// <returns>The Text Object if found; null if not.</returns>
    protected Text GetTextComponent(string name)
    {
        foreach (Text t in window.transform.GetComponentsInChildren<Text>())
            if (t.gameObject.name == name)
                return t;

        return null;
    }
}
