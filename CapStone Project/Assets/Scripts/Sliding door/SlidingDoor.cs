using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : InteractableObject
{
    public Vector3 openPos;
    public Vector3 closePos;
    public Vector3 newPos;
    public bool opened;
    public float speed;

    private void Awake()
    {
        if (opened)
        {
            newPos = transform.position;
            openPos = newPos;
        }
        else
        {
            newPos = transform.position;
            closePos = newPos;
        }
    }

    public void FixedUpdate()
    {
        if (Manager.instance.paused) return;
        transform.position =
            Vector3.Lerp(transform.position, newPos, Time.fixedDeltaTime * speed);
    }

    public override void Use()
    {
        if (opened)
        {
            newPos = closePos;
        }
        else
        {
            newPos = openPos;
        }
        opened = !opened;
    }
}
