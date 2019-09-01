using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Direction
{
    private static float diagLen = Mathf.Sqrt(2) / 2;
    private static Vector2[] dirVects = { new Vector2(-diagLen, -diagLen),
                                           new Vector2(0, -1),
                                           new Vector2(diagLen, -diagLen),
                                           new Vector2(-1, 0),
                                           new Vector2(0, 0),
                                           new Vector2(1, 0),
                                           new Vector2(-diagLen, diagLen),
                                           new Vector2(0, 1),
                                           new Vector2(diagLen, diagLen)};

    // get the direction the player is currently aiming at
    public static EDirection getAimingDirection() {
        return (EDirection)(
            (Input.GetAxis("Horizontal").CompareTo(0)) + 
            (Input.GetAxis("Vertical").CompareTo(0) * 3) + 
            4);     //offset so all values between 0 and 8
    }

    // get the vector of the direction currently aimed at
    public static Vector2 getAimingVector()
    {
        return getDirectionVector(getAimingDirection());
    }

    // get the vector of an direction
    public static Vector2 getDirectionVector( EDirection dir) { return dirVects[ (int)dir ]; }
}
