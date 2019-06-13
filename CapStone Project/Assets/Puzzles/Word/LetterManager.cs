using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterManager : MonoBehaviour
{
    
    public LetterClass[] letters;
    public LetterSpace[] space;
    public int pos;
    
    public bool CheckLetterSpace(int posTemp)
    {
        if (space[posTemp] == null)
        {
            return true;
        }
        return false;
    }
}
