using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface assigned to any Object that, at some point, will listen to InputManager's Actions.
/// </summary>
public interface IControllable
{
    /// <summary>
    /// Responsible for assigning Actions and enabling relevant components.
    /// </summary>
    void EnableControls();

    /// <summary>
    /// Responsible for deassigning Actions and disabling relevant components.
    /// </summary>
    void DisableControls();
}
