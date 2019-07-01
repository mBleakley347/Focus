using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PuzzleComponent : Interactable
{
    public delegate void check();
    public event check doCheck;
    private bool currentState;
    public bool complete
    {
        get
        {
            return currentState;
        }
        set
        {
            if (value != currentState)
            {
                currentState = value;
                doCheck();
            }
        }
    }
    
    
   

    private void Start()
    {
        
    }
}
