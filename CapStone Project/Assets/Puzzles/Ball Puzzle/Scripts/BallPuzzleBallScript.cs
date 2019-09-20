﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPuzzleBallScript : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private Vector3 origin;
    public bool released;
    // Start is called before the first frame update
    void Start()
    {
        ResetScriptBallPuzzle.ResetPuzzle += Reset;
        origin = transform.position;
    }

    public void Reset()
    {
        released = false;
        rb.velocity.Set(0,0,0);
        rb.angularVelocity.Set(0,0,0);
        rb.Sleep();
        transform.position = origin;
    }

    public void End()
    {
        released = false;
        rb.velocity.Set(0,0,0);
        rb.angularVelocity.Set(0,0,0);
        rb.Sleep();
    }
    // Update is called once per frame
    void Update()
    {
        if (released)
        {
            rb.AddForce(-transform.up);
        }
    }
}
