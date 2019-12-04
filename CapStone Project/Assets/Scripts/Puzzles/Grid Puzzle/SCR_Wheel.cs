using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        newRotation = graphic.transform.localEulerAngles;
        player = FindObjectOfType<CastPlayer>().GetComponent<SCR_PlayerController>();
    }

    public override bool Click(Vector3 a)
    {
        pos = a;
        return base.Click(a);
    }

    public override bool Hold(Vector3 a)
    {
        a = Input.mousePosition;
        //Vector3 FacePos = Vector3.ProjectOnPlane(transform.InverseTransformPoint(a), transform.forward);
        Vector3 FacePos = Camera.main.WorldToScreenPoint(transform.position);
        //FacePos.Normalize();
        
        if (a.x < FacePos.x - 30 && !directionY || directionY && a.y > FacePos.y + 30)
        {
            MoveNode(true);
            player.ForceMouseUp(resetTime);
            newRotation = new Vector3(graphic.transform.localEulerAngles.x,graphic.transform.localEulerAngles.y, newRotation.z +90);
            newQuaternion = Quaternion.Euler(newRotation);
            newRotation = newQuaternion.eulerAngles;
        } else if (a.x > FacePos.x + 30 && !directionY || directionY && a.y < FacePos.y - 30)
        {
            MoveNode(false);
            player.ForceMouseUp(resetTime);
            newRotation = new Vector3(graphic.transform.localEulerAngles.x,graphic.transform.localEulerAngles.y, newRotation.z -90);
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
        if (Manager.instance.paused) return;
        if (graphic.transform.localRotation.z < newQuaternion.z || graphic.transform.localRotation.z > newQuaternion.z)
        {
            graphic.transform.localRotation = Quaternion.Lerp(graphic.transform.localRotation, newQuaternion, rotationSpeed);
        }
    }
}
