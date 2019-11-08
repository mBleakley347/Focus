using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SCR_PBManager : MonoBehaviour
{
   
    public bool complete;
    public SCR_PuzzleManager[] puzzles;
    public GameObject lid;
    
    private void Awake()
    {
        foreach (var VARIABLE in puzzles)
        {
            VARIABLE.doCheck += Check;
        }
    }

    public void Check()
    {
        for (int i = 0; i < puzzles.Length; i++)
        {
            if (!puzzles[i].complete)
            {
                complete = false;
                Close();
                break;
            }
            if (i == puzzles.Length - 1)
            {
                complete = true;
                Open();
            }
                
        }
    }

    public void Update()
    {
        if (Manager.instance.paused) return;
        if (complete) Open();
        else Close();
    }

    public void Open()
    {
        lid.transform.localRotation = Quaternion.Lerp(lid.transform.localRotation,Quaternion.Euler(0,0,-90),0.01f );
        //lid.transform.Rotate(0,0,90);
        //lid.transform.localPosition = new Vector3(0.5f,1,0);
    }
    
    public void Close()
    {
        lid.transform.localRotation.Set(0f,0f,0f,0f);
        //lid.transform.localPosition = new Vector3(0,0.5f,0);
    }
}
