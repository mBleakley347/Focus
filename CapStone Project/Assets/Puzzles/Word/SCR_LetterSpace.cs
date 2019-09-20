using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SCR_LetterSpace : MonoBehaviour
{

    [FormerlySerializedAs("letter")] public SCR_LetterClass scrLetter;
    [SerializeField] private SCR_LetterManager wordManager;
    public int pos;

    public void MoveLetterUp(SCR_LetterClass scrLetterClass)
    {
        int posTemp = pos + wordManager.pos;
        if (CheckLetterSpace(posTemp))
        {
            wordManager.letters[posTemp] = scrLetter;
            scrLetter.gameObject.transform.Translate(0, wordManager.gameObject.transform.position.y,0);
            scrLetter = null;
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
