using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [Header("transition back")]
    public AudioClip postMemoryRemark;
    public AudioClip postMemoryRemark2;
    public float waittime = 1;
    private float whiteouttime = 1.0f;
    public List<Image> whiteouts;
    private bool start = true;
    private int working = 0;


    private bool changing = false;
    private bool changingback = false;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (changing)
        {
            Change();
        }
        if (changingback&&!SCR_AudioManager.instanceAM.voiceSouce.isPlaying&&SCR_AudioManager.instanceAM.voiceSouce.clip == VoiceLines[VoiceLines.Length-1]&&Dadpos==Dads.Length-1)
        {
            changeout();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(Dadpos + " "+SCR_AudioManager.instanceAM.voiceSouce.isPlaying+" "+changingback);
        }
        if (Voicepos < VoiceLines.Length)
        {
            if (SCR_AudioManager.instanceAM.voiceSouce.isPlaying == false)
            {
                Waittime = Waittime + Time.deltaTime * 2;
                if (Waittime > 1)
                {
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
                            if (voicefadesynchro.Count == 1)
                            {
                                changingback = true;
                            }
                            voicefadesynchro.RemoveAt(0);
                            changing = true;
                        }
                    }
                    Debug.Log("voicepos " + Voicepos);
                    SCR_AudioManager.instanceAM.voiceSouce.clip = VoiceLines[Voicepos];
                    SCR_AudioManager.instanceAM.voiceSouce.Play();
                    Waittime = 0;
                    Voicepos++;
                }
            }
        }

        if (SCR_AudioManager.instanceAM.voiceSouce.isPlaying&&ismusicstingline)
        {
            if (SCR_AudioManager.instanceAM.voiceSouce.time >= musicstingtimes[0].myList[0])
            {
                SCR_AudioManager.instanceAM.musicSouce.PlayOneShot(musicstingclip[0].myList[0]);
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
            if (Dadpos != Dads.Length)
            {
                dithermat = new List<Material>(nextdithermat);
            }
            Dadpos += 1;
            nextdithermat = new List<Material>();
            changing = false;

            //Dads[Dadpos].SetActive(false);
            //Dadpos++;
            //Dads[Dadpos].SetActive(true);
        }
    }

    public void changeout()
    {
        // fade the old dad out and the new dad in
        for( int i = 0; i < dithermat.Count; i++)
        {
            //fade the old dad out
            
            ditherer(dithermat[i], 0, Mathf.InverseLerp(1,0,dithermat[i].GetFloat("_Transparency"))+(ditherrate*Time.deltaTime));
        }
        if (dithermat[0].GetFloat("_Transparency") == 0)
        {
            // cleanup and reset
            Dads[Dadpos].SetActive(false);

            start = true;
            StartCoroutine(WaitForAudio());
            changingback = false;

            //Dads[Dadpos].SetActive(false);
            //Dadpos++;
            //Dads[Dadpos].SetActive(true);
        }
    }
    private void ditherer(Material a, float b, float c)
    {
        a.SetFloat("_Transparency", Mathf.Lerp(a.GetFloat("_Transparency"), b, c));
    }
    
    IEnumerator WaitForAudio()
    {
        for(; ; )
        {
            //Debug.Log("test");
            if (start)
            {
                
                if (postMemoryRemark)
                    SCR_AudioManager.instanceAM.voiceSouce.PlayOneShot(postMemoryRemark);
                StartCoroutine(secondclip(postMemoryRemark.length));
                //Fade out current music
                float startVolume = SCR_AudioManager.instanceAM.musicSouce.volume;

                while (SCR_AudioManager.instanceAM.musicSouce.volume > 0)
                {
                    SCR_AudioManager.instanceAM.musicSouce.volume -= startVolume * Time.deltaTime / 5.0f;

                    yield return null;
                }

                SCR_AudioManager.instanceAM.musicSouce.Stop();
                SCR_AudioManager.instanceAM.musicSouce.volume = startVolume;
                start = false;
                yield return new WaitForSeconds(waittime);
            }
            // handle timed transition to black here
            if (working < whiteouts.Count)
            {
                if (whiteouts.Count != 0)
                    whiteouts[working]?.CrossFadeAlpha(4.0f, whiteouttime, false);
                working++;
            }
            else
            {
                CastPlayer player = GameObject.FindGameObjectWithTag("Player").GetComponent<CastPlayer>();

                player.scale = 1;
                player.viewpoint = player.normal;
                player.viewpoint.enabled = true;
                player.memory.enabled = false;
                Manager.instance.inMemory = false;
                if (working >= whiteouts.Count)
                {
                    foreach (Image item in whiteouts)
                    {
                        item?.CrossFadeAlpha(0.0f, 1.0f, false);
                    }
                    //memoryManager.SetActive(true);
                    //dadsgroup.SetActive(true);
                    yield break;
                }
            }
            yield return new WaitForSeconds(whiteouttime);
        }
    }

    IEnumerator secondclip(float waittime)
    {
        new WaitForSeconds(waittime+1);
        if (postMemoryRemark2)
            SCR_AudioManager.instanceAM.voiceSouce.PlayOneShot(postMemoryRemark2);
        yield break;
    }
}