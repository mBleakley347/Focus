using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class SCR_Wheel : SCR_PuzzleComponent
{
    [SerializeField] private SCR_GridBase grid;
    [SerializeField] private Vector3 pos;
    [SerializeField] private SCR_PlayerController player;
    [SerializeField] private bool directionY;
    [SerializeField] private float resetTime;
    [SerializeField] private float distance;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 newPos;
    [SerializeField] private GameObject graphic;
    private Vector3 nodePosition;
    
    public Vector3 newRotation;
    public Quaternion newQuaternion;

    private void Awake()
    {
        newRotation = graphic.transform.eulerAngles;
    }

    public override bool Click(Vector3 a)
    {
        pos = a;
        return base.Click(a);
    }

    public override bool Hold(Vector3 a)
    {
        a -= pos;
        Vector3 FacePos = Vector3.ProjectOnPlane(transform.InverseTransformPoint(a), transform.forward);
        FacePos.Normalize();
        if (a.x < FacePos.x - 1 && !directionY || directionY && a.y > FacePos.y + 2)
        {
            MoveNode(true);
            player.ForceMouseUp(resetTime);
            newRotation = new Vector3(graphic.transform.eulerAngles.x,graphic.transform.eulerAngles.y, newRotation.z +90);
            newQuaternion = Quaternion.Euler(newRotation);
            newRotation = newQuaternion.eulerAngles;
        } else if (a.x > FacePos.x + 2 && !directionY || directionY && a.y < FacePos.y - 1)
        {
            MoveNode(false);
            player.ForceMouseUp(resetTime);
            newRotation = new Vector3(graphic.transform.eulerAngles.x,graphic.transform.eulerAngles.y, newRotation.z -90);
            newQuaternion = Quaternion.Euler(newRotation);
            newRotation = newQuaternion.eulerAngles;
        }
        

        return base.Hold(a);
    }

    private void MoveNode(bool positive)
    {
        if (!directionY)
        {
            if (positive)
            {
                grid.MoveRight();
            }
            else
            {
                grid.MoveLeft();
            }
        }
        else
        {
            if (positive)
            {
                grid.MoveUp();
            }
            else
            {
                grid.MoveDown();
            }
        }
    }

    private void Update()
    {
        if (graphic.transform.rotation.z < newQuaternion.z || graphic.transform.rotation.z > newQuaternion.z)
        {
            graphic.transform.rotation = Quaternion.Lerp(graphic.transform.rotation, newQuaternion, rotationSpeed);
        }
    }
}
