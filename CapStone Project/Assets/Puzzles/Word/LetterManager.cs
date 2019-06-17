using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterManager : MonoBehaviour
{
    
    public LetterClass[] letters;
    public LetterSpace[] space;
    public int pos;

    public void MoveLetterDown(LetterClass letter)
    {
        if (CheckLetterSpace(letter.currentPos - pos))
        {
            for (int i = 0; i < letters.Length; i++)
            {
                if (letters[i] == null) continue;
                if (letters[i] == letter)
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
            if (space[posTemp].letter == null) return true;
        }
        return false;
    }
}
