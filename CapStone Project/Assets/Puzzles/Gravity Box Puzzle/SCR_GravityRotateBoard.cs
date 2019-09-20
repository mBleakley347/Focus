using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_GravityRotateBoard : SCR_Interactable
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private int rotation = 90;
    [SerializeField] private GameObject board;
    private Vector3 newRotation;

    public void Awake()
    {
        newRotation = transform.rotation.eulerAngles;
    }

    public override bool Click(Vector3 a)
    {
        //board.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,rotation),rotationSpeed);
        newRotation = new Vector3(newRotation.x,newRotation.y, newRotation.z +rotation);
        //board.transform.Rotate(Vector3.Lerp(transform.rotation.eulerAngles, newRotation,rotationSpeed));
        return base.Click(a);
    }

    private void Update()
    {
        board.transform.Rotate(Vector3.Lerp(transform.rotation.eulerAngles, newRotation,rotationSpeed));
    }
}
