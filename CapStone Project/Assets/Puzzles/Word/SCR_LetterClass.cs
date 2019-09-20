using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_LetterClass : SCR_Interactable
{
    public string name;
    public int correctPos;
    public int currentPos;
    public GameObject currentPlace;

    public override bool Click(Vector3 a)
    {
        if (currentPlace.GetComponent<SCR_LetterSpace>())
        {
            currentPlace.GetComponent<SCR_LetterSpace>().MoveLetterUp(this);
            print("letter space");
        } else if (currentPlace.GetComponent<SCR_LetterManager>())
        {
            currentPlace.GetComponent<SCR_LetterManager>().MoveLetterDown(this);
            print("letter manager");
        }
        return true;
    }

    private bool Check()
    {
        if (currentPos == correctPos && currentPlace.GetComponent<SCR_LetterManager>()) return true;
        
        return false;
    }
}
