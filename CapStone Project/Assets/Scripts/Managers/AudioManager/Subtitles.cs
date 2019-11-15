using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Subtitles : MonoBehaviour
{

    public string text;
    public int currentText;
    private char[] letters = new char[0];
    private int currentChar = 0;
    private int stringLength;

    public bool writing;

    public Text subtitles;

    public float timeInterval;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        stringLength = text.Length;
        letters = text.ToCharArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (writing)
        {
            if (timer >= timeInterval)
            {
                subtitles.text = subtitles.text + letters[currentChar];
                currentChar++;
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
            if (currentChar >= text.Length)
            {
                writing = false;
                currentChar = 0;
            }
        } else if (Input.GetKeyDown(KeyCode.Space))
        {
            subtitles.text = "";
            writing = true;
        }      
        
    }

    public void StartSubs(string newSubtitles)
    {
        text = newSubtitles;
        letters = text.ToCharArray();
    }
}
