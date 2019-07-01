using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PuzzleManager : MonoBehaviour
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
    public PuzzleComponent[] elements;
    

    private void Awake()
    {
        foreach (var VARIABLE in elements)
        {
            VARIABLE.doCheck += Check;
        }
    }

    public void Check()
    {
        for (int i = 0; i > elements.Length; i++)
        {
            if (!elements[i].complete)
            {
                complete = false;
                break;
            }

            if (i == elements.Length - 1)
            {
                complete = true;
            }
        }
    }
}
