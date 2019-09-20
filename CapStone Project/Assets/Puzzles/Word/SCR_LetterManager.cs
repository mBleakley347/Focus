using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_LetterManager : MonoBehaviour
{
    
    public SCR_LetterClass[] letters;
    public SCR_LetterSpace[] space;
    public int pos;

    public void MoveLetterDown(SCR_LetterClass scrLetter)
    {
        if (CheckLetterSpace(scrLetter.currentPos - pos))
        {
            for (int i = 0; i < letters.Length; i++)
            {
                if (letters[i] == null) continue;
                if (letters[i] == scrLetter)
                {
                    letters[i] = null;
                }
            }
        }
    }

    public bool CheckLetterSpace(int posTemp)
    {
        if (space[posTemp] == null)
        {
            if (space[posTemp].scrLetter == null) return true;
        }
        return false;
    }
}
