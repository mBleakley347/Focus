using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionManager : MonoBehaviour
{
    private int AllCorrectPositions = 0;
    public Interactable[] elements;

    private void Awake()
    {
        foreach (var VARIABLE in elements)
        {
            VARIABLE.check += UpdateCorrectPosition;
        }
    }

    public void UpdateCorrectPosition(bool correct)
    {
        AllCorrectPositions += correct ? 1 : -1;
        if (AllCorrectPositions == elements.Length) print("complete");
    }
}
