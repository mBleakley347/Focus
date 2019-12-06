using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Memory : SCR_Interactable
{
    public GameObject memoryManager;
    public GameObject dadsgroup;
    
    public AudioClip remark;
    public float waittime = 1;
    private float whiteouttime = 1.0f;
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

    public override bool Click(Vector3 a)
    {
        Manager.instance.inMemory = true;
        StartCoroutine(WaitForAudio());
        return base.Click(a);
    }

    IEnumerator WaitForAudio()
    {
        for(; ; )
        {

            if (start)
            {
                if (remark)
                    SCR_AudioManager.instanceAM.voiceSouce.PlayOneShot(remark);
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

                player.drop();
                player.viewpoint = player.memory;
                player.viewpoint.enabled = true;
                player.normal.enabled = false;
                if (working >= whiteouts.Count)
                {
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
