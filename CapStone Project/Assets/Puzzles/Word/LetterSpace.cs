using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpace : MonoBehaviour
{

    public LetterClass letter;
    [SerializeField] private LetterManager wordManager;
    public int pos;

    public void MoveLetterUp(LetterClass letterClass)
    {
        int posTemp = pos + wordManager.pos;
        if (CheckLetterSpace(posTemp))
        {
            wordManager.letters[posTemp] = letter;
            letter.gameObject.transform.Translate(0, wordManager.gameObject.transform.position.y,0);
            letter = null;
        }
    }

    public bool CheckLetterSpace(int posTemp)
    {
        if (wordManager.letters[posTemp] == null)
        {
            return true;
        }
        return false;
    }
}
