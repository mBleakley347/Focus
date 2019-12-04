using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DrawingMemory : MonoBehaviour
{
    public GameObject[] Dads;
    public int Dadpos;
    public List<Material> dithermat;
    private List<Material> nextdithermat = new List<Material>();
    public AudioClip[] VoiceLines;
    public List<int> voicefadesynchro = new List<int>();
    public int Voicepos;
    public float Waittime;
    public float ditherrate = 0.1f;
    [Header("music sting")]
    public List<int> musicstingtalkindex;

    public bool ismusicstingline = false;
    public List<FloatListWrapper> musicstingtimes;
    public List<AudioListWrapper> musicstingclip;


    private bool changing = false;
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
        if (changing)
        {
            Change();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Change();
        }
        if (Voicepos < VoiceLines.Length)
        {
            if (SCR_AudioManager.instanceAM.voiceSouce.isPlaying == false)
            {
                Waittime = Waittime + Time.deltaTime * 2;
                if (Waittime > 1)
                {
                    Voicepos++;
                    if (musicstingtalkindex.Count > 0)
                    {
                        if (Voicepos == musicstingtalkindex[0] && !ismusicstingline)
                        {
                            ismusicstingline = true;
                        }
                    }

                    if (voicefadesynchro.Count > 0)
                    {
                        if (Voicepos == voicefadesynchro[0]&&!changing)
                        {
                            voicefadesynchro.RemoveAt(0);
                            changing = true;
                        }
                    }
                    SCR_AudioManager.instanceAM.voiceSouce.clip = VoiceLines[Voicepos];
                    SCR_AudioManager.instanceAM.voiceSouce.Play();
                    Waittime = 0;
                }
            }
        }

        if (SCR_AudioManager.instanceAM.voiceSouce.isPlaying&&ismusicstingline)
        {
            if (SCR_AudioManager.instanceAM.voiceSouce.time >= musicstingtimes[0].myList[0])
            {
                SCR_AudioManager.instanceAM.voiceSouce.PlayOneShot(musicstingclip[0].myList[0]);
                musicstingclip[0].myList.RemoveAt(0);
                musicstingtimes[0].myList.RemoveAt(0);
                if (musicstingclip[0].myList.Count == 0)
                {
                    musicstingclip.RemoveAt(0);
                    musicstingtimes.RemoveAt(0);
                    musicstingtalkindex.RemoveAt(0);
                    ismusicstingline = false;
                }
            }
        }
    }

    void Change()
    {
        // get next dad's materials for dithering

        if (nextdithermat.Count == 0)
        {
            var temp = Dads[(Dadpos+1)].GetComponentsInChildren<Renderer>();
            foreach (Renderer item in temp)
            {
                foreach (Material mat in item.materials)
                {
                    if (mat.shader.name == "Custom/Dither")
                    {
                        nextdithermat.Add(mat);
                        // make sure it's invisible
                        mat.SetFloat("_Transparency", 0);
                    }
                }
            }
            Dads[Dadpos+1].SetActive(true);
        }


        // fade the old dad out and the new dad in
        for( int i = 0; i < dithermat.Count; i++)
        {
            //fade the old dad out
            
            ditherer(dithermat[i], 0, Mathf.InverseLerp(1,0,dithermat[i].GetFloat("_Transparency"))+(ditherrate*Time.deltaTime));
            // and swap it out
            ditherer(nextdithermat[i], 1, 1-dithermat[i].GetFloat("_Transparency"));
        }

        if (nextdithermat[0].GetFloat("_Transparency") == 1)
        {
            // cleanup and reset
            Dads[Dadpos].SetActive(false);
            Dadpos += 1;
            dithermat = new List<Material>(nextdithermat);
            nextdithermat = new List<Material>();
            changing = false;

            //Dads[Dadpos].SetActive(false);
            //Dadpos++;
            //Dads[Dadpos].SetActive(true);
        }
    }
    private void ditherer(Material a, float b, float c)
    {
        a.SetFloat("_Transparency", Mathf.Lerp(a.GetFloat("_Transparency"), b, c));
    }
}