using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private ConstantForce2D force;

    private void swing(EDirection dir) { force.force = Direction.getDirectionVector(dir) * 10; }
    public void swingRight() { swing(EDirection.RIGHT); }
    public void swingLeft() { swing(EDirection.LEFT); }
    public void resetSwing() { force.force = Vector2.zero; }

    private void Awake()
    {
        force = GetComponent<ConstantForce2D>();
    }

    private void OnEnable()
    {
        InputManager i = InputManager.instance;
        i.onLeftPressed += swingLeft;
        i.onRightPressed += swingRight;
        i.onDirectionChanged += resetSwing;
    }

    private void OnDisable()
    {
        InputManager i = InputManager.instance;
        i.onLeftPressed -= swingLeft;
        i.onRightPressed -= swingRight;
        i.onDirectionChanged -= resetSwing;
    }
}
