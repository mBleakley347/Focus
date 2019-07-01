using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBManager : MonoBehaviour
{
    bool complete;
    public PuzzleManager[] puzzles;
    
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
                break;
            }
            if (i == puzzles.Length - 1)
            {
                complete = true;
            }
                
        }
    }
}
