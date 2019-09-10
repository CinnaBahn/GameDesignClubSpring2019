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

    void setupComponents() { force = GetComponent<ConstantForce2D>(); }

    private void Awake()
    {
        setupComponents();
    }

    private void Start()
    {
        //GameplayController pc = GetComponent<GameplayController>();
        GameplayController pc = Controller.gameplayController;
        pc.onGrappleReleased += resetSwing;
        pc.onSwingRelax += resetSwing;
        pc.onSwingLeft += swingLeft;
        pc.onSwingRight += swingRight;
    }
}
