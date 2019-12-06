using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_WindowLookToggle : MonoBehaviour
{
    public GameObject Pannel;
    public Text Credits;
    public Image photo1;
    public Image photo2;
    public Image photo3;
    public Image photo4;
    public Image photo5;
    public Image photo6;
    public float timeskis;

    // Start is called before the first frame update
    void Start()
    {
        Pannel.SetActive(true);
        Pannel.GetComponent<Image>().CrossFadeAlpha(0.0f, 0.0f, false);
        Credits.CrossFadeAlpha(0.0f, 0.0f, false);
        photo1.CrossFadeAlpha(0.0f, 0.0f, false);
        photo2.CrossFadeAlpha(0.0f, 0.0f, false);
        photo3.CrossFadeAlpha(0.0f, 0.0f, false);
        photo4.CrossFadeAlpha(0.0f, 0.0f, false);
        photo5.CrossFadeAlpha(0.0f, 0.0f, false);
        photo6.CrossFadeAlpha(0.0f, 0.0f, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.instance.finished) Manager.instance.finished = true;
        timeskis = timeskis + Time.deltaTime * 2;
        if(timeskis > 5)
        {
            Pannel.GetComponent<Image>().CrossFadeAlpha(5.0f, 4.0f, false);
            Credits.CrossFadeAlpha(5.0f, 4.0f, false);
            photo1.CrossFadeAlpha(5.0f, 4.0f, false);
            photo2.CrossFadeAlpha(5.0f, 4.0f, false);
            photo3.CrossFadeAlpha(5.0f, 4.0f, false);
            photo4.CrossFadeAlpha(5.0f, 4.0f, false);
            photo5.CrossFadeAlpha(5.0f, 4.0f, false);
            photo6.CrossFadeAlpha(5.0f, 4.0f, false);
        }
    }
}
