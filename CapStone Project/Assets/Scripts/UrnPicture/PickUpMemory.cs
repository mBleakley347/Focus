using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMemory : InteractableObject
{
    public GameObject memoryManager;
    public GameObject dadsgroup;
    
    public AudioClip remark;
    public float waittime = 1;
    public float whiteouttime = 0.2f;
    public List<GameObject> whiteouts;
    private bool start = true;
    private bool reverse = false;
    private int working = 0;

    public override void Use(CastPlayer player)
    {
        StartCoroutine(WaitForAudio());
    }

    IEnumerator WaitForAudio()
    {
        if (start)
        {
            if(remark)
                SCR_AudioManager.instanceAM.voiceSouce.PlayOneShot(remark);
            start = false;
            yield return new WaitForSeconds(waittime);
        }
        // handle timed transition to black here
        if (working < whiteouts.Count)
        {
            if(whiteouts.Count!=0)
                whiteouts[working]?.SetActive(true);
            working++;
        }
        else
        {
            if (working >= whiteouts.Count)
            {
                reverse = true;
                memoryManager.SetActive(true);
                dadsgroup.SetActive(true);
            }

            if (reverse&&working>-1)
            {
                if(whiteouts.Count!=0)
                    whiteouts[working]?.SetActive(false);
                working--;
            }
            else if(working==-1)
            {
                StopCoroutine("WaitForAudio");
            }
        }
        yield return new WaitForSeconds(whiteouttime);
    }
}
