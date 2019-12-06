﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpMemory : InteractableObject
{
    public GameObject memoryManager;
    public GameObject dadsgroup;

    public AudioClip remark;
    public float waittime = 1;
    private float whiteouttime = 1.0f;
    public float playerscale = 1f;
    public List<Image> whiteouts;
    private bool start = true;
    private int working = 0;

    private void Awake()
    {
        foreach (Image item in whiteouts)
        {
            item.CrossFadeAlpha(0f, 0f, false);
        }
    }

    public override void Use(CastPlayer player)
    {
        Manager.instance.inMemory = true;

        StartCoroutine(WaitForAudio(player));
    }

    IEnumerator WaitForAudio(CastPlayer player)
    {
        for (; ; )
        {
            if (start)
            {
                if (remark)

                    SCR_AudioManager.instanceAM.voiceSouce.PlayOneShot(remark);
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
                if (working >= whiteouts.Count)
                {
                    player.viewpoint = player.memory;
                    player.viewpoint.enabled = true;
                    player.normal.enabled = false;
                    player.scale = playerscale;
                    foreach (Image item in whiteouts)
                    {
                        item?.CrossFadeAlpha(0.0f, 1.0f, false);
                    }
                    memoryManager.SetActive(true);
                    dadsgroup.SetActive(true);
                    StopCoroutine("WaitForAudio");
                }
            }
            yield return new WaitForSeconds(whiteouttime);
        }
    }




}
