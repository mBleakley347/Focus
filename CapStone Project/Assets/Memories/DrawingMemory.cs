using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DrawingMemory : MonoBehaviour
{
    public GameObject[] Dads;
    public int Dadpos;
    public List<Material> dithermat;
    public AudioClip[] VoiceLines;
    public int Voicepos;
    public float Waittime;
    // Start is called before the first frame update
    void Start()
    {
        Dadpos = 0;
        Dads[Dadpos].SetActive(true);
        Voicepos = 0;
        SCR_AudioManager.instanceAM.voiceSouce.clip = VoiceLines[Voicepos];
        var temp = Dads[Dadpos].GetComponentsInChildren<Renderer>();
        foreach (Renderer item in temp)
        {
            foreach (Material mat in item.materials)
            {
                if (mat.shader.name == "Custom/Dither") dithermat.Add(mat);
            }
        }
        SCR_AudioManager.instanceAM.voiceSouce.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Change();
        }
        if (Voicepos < VoiceLines.Length)
        {
            if (SCR_AudioManager.instanceAM.voiceSouce.isPlaying == false)
            {
                Waittime = Waittime + Time.deltaTime * 2;
                if (Waittime > 1)
                {
                    Voicepos++;
                    SCR_AudioManager.instanceAM.voiceSouce.clip = VoiceLines[Voicepos];
                    SCR_AudioManager.instanceAM.voiceSouce.Play();
                    Waittime = 0;
                }
            }
        }
    }

    void Change()
    {


        foreach (Material mat in dithermat)
        {
            while (mat.GetFloat("_Transparency") > 0)
            {
                ditherer(mat, mat.GetFloat("_Transparency") - 0.1f, 0.1f);
            }
        }

        //Dads[Dadpos].SetActive(false);
        //Dadpos++;
        //Dads[Dadpos].SetActive(true);
    }
    private void ditherer(Material a, float b, float c)
    {
        a.SetFloat("_Transparency", Mathf.Lerp(a.GetFloat("_Transparency"), b, c));
    }
}
