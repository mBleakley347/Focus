using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Goal : SCR_PuzzleComponent
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.gameObject.GetComponent<SCR_Ball>().End();
            complete = true;
        }
    }
}
