﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullPatrol : MonoBehaviour
{

    public float speed; //speed of the boi
    public float distance; //distance of the ray detecting collision
    public string movedir; //initial direction of movement so far can either be "up" or "right"
    public string coldir; //direction of the ray to detect for collision

    private bool movingInitialDir = true;

    public Transform groundDetection;

    Vector2 downlef;
    Vector2 uplef;
    Vector2 downrig;

    void Start()
    {
        downlef = new Vector2(-1,-1);
        uplef = new Vector2(-1,1);
        downrig = new Vector2(1,-1);
    }

    void Update()
    {
        if (movedir.Equals("right"))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (coldir.Equals("down"))
            {
                RaycastHit2D groundBoi = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
                if (groundBoi.collider == false)
                {
                    if (movingInitialDir == true)
                    {
                        transform.eulerAngles = new Vector3(0, -180, 0);
                        movingInitialDir = false;
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        movingInitialDir = true;
                    }
                }
            }
            else if (coldir.Equals("up"))
            {
                RaycastHit2D groundBoi = Physics2D.Raycast(groundDetection.position, Vector2.up, distance);
                if (groundBoi.collider == false)
                {
                    if (movingInitialDir == true)
                    {
                        transform.eulerAngles = new Vector3(0, -180, 0);
                        movingInitialDir = false;
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        movingInitialDir = true;
                    }
                }
            }
        }
        else if (movedir.Equals("up"))
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);

            if (coldir.Equals("right"))
            {
                RaycastHit2D groundBoi = Physics2D.Raycast(groundDetection.position, Vector2.right, distance);
                if (groundBoi.collider == false)
                {
                    if (movingInitialDir == true)
                    {
                        transform.eulerAngles = new Vector3(-180, 0, 0);
                        movingInitialDir = false;
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        movingInitialDir = true;
                    }
                }
            }
            else if (coldir.Equals("left"))
            {
                RaycastHit2D groundBoi = Physics2D.Raycast(groundDetection.position, Vector2.left, distance);
                if (groundBoi.collider == false)
                {
                    if (movingInitialDir == true)
                    {
                        transform.eulerAngles = new Vector3(-180, 0, 0);
                        movingInitialDir = false;
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        movingInitialDir = true;
                    }
                }
            }
        }
        else if (movedir.Equals("rotate"))
        {
                RaycastHit2D groundBoi = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
                RaycastHit2D sideBoi = Physics2D.Raycast(groundDetection.position, Vector2.right, distance);
                RaycastHit2D upBoi = Physics2D.Raycast(groundDetection.position, Vector2.up, distance);
                RaycastHit2D leftBoi = Physics2D.Raycast(groundDetection.position, Vector2.left, distance);
                RaycastHit2D diagonalley = Physics2D.Raycast(groundDetection.position, Vector2.one, distance);
                RaycastHit2D downleft = Physics2D.Raycast(groundDetection.position, downlef, distance);
                RaycastHit2D upleft = Physics2D.Raycast(groundDetection.position, uplef, distance);
                RaycastHit2D downright = Physics2D.Raycast(groundDetection.position, downrig, distance);

                if(groundBoi.collider)
                {
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                }
                else if (!groundBoi.collider)
                {
                    if (sideBoi.collider || downright)
                    {
                        transform.Translate(Vector2.down * speed * Time.deltaTime);
                    }
                    else if(!sideBoi.collider && diagonalley || upBoi.collider)
                    {
                      transform.Translate(Vector2.right * speed * Time.deltaTime);
                    }
                    else if(!upBoi.collider && upleft && !diagonalley)
                    {
                        transform.Translate(Vector2.up * speed * Time.deltaTime);
                    }
                    else if(!upleft && downleft)
                    {
                      transform.Translate(Vector2.left * speed * Time.deltaTime);
                    }
                }

        }

    }
  }