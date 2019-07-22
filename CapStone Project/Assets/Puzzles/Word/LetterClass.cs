using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterClass : Interactable
{
    public string name;
    public int correctPos;
    public int currentPos;
    public GameObject currentPlace;

    public override bool Click(Vector3 a)
    {
        if (currentPlace.GetComponent<LetterSpace>())
        {
            currentPlace.GetComponent<LetterSpace>().MoveLetterUp(this);
            print("letter space");
        } else if (currentPlace.GetComponent<LetterManager>())
        {
            currentPlace.GetComponent<LetterManager>().MoveLetterDown(this);
            print("letter manager");
        }
        return true;
    }

    private bool Check()
    {
        if (currentPos == correctPos && currentPlace.GetComponent<LetterManager>()) return true;
        
        return false;
    }
}
