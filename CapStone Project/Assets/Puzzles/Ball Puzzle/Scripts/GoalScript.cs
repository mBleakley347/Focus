using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : PuzzleComponent
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.gameObject.GetComponent<BallPuzzleBallScript>().End();
            complete = true;
        }
    }
}
