using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScriptBallPuzzle : MonoBehaviour
{
    public delegate void Reset();
    public static event Reset ResetPuzzle;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("1");
        if (other.gameObject.CompareTag("Ball"))
        {
            ResetPuzzle();
        }
    }
    
}
