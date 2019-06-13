using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpace : MonoBehaviour
{

    public LetterClass letter;
    [SerializeField] private LetterManager wordPlacement;
    public int pos;

    public void MoveLetterUp()
    {
        int posTemp = pos - wordPlacement.pos;
        if (CheckLetterSpace(posTemp))
        {
            wordPlacement.letters[posTemp] = letter;
            letter.gameObject.transform.Translate(0, wordPlacement.gameObject.transform.position.y,0);
            letter = null;
        }
    }

    public bool CheckLetterSpace(int posTemp)
    {
        if (wordPlacement.letters[posTemp] == null)
        {
            return true;
        }
        return false;
    }
}
