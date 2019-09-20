using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlatformButton : SCR_PuzzleComponent
{
    public GameObject[] effectedObjects;
    [SerializeField] private bool pressed = false;
    [SerializeField] private Vector3 moveDistance;
    public override bool Click(Vector3 a)
    {
        Debug.Log("1");
        if (pressed)
        {
            Debug.Log("2");
            return base.Click(a);
        }
        
        Debug.Log("3");
        pressed = true;
        MoveObjects();
        return base.Click(a);
    }

    private void MoveObjects()
    {
        foreach (var objects in effectedObjects)
        {
            var position = objects.transform.localPosition;
            position += moveDistance;
            //position = Vector3.Lerp(position, new Vector3(position.x + moveDistance.x, position.y + moveDistance.y, position.z + moveDistance.z), Time.deltaTime);
            objects.transform.localPosition = position;
        }
    }
    private void Awake()
    {
        SCR_ResetArea.ResetPuzzle += Reset;
    }
    public void Reset()
    {
        if (pressed)
        {
            foreach (var objects in effectedObjects)
            {
                var position = objects.transform.localPosition;
                position -= moveDistance;
                //position = Vector3.Lerp(position, new Vector3(position.x + moveDistance.x, position.y + moveDistance.y, position.z + moveDistance.z), Time.deltaTime);
                objects.transform.localPosition = position;
            }
        }
        pressed = false;
    }
}
