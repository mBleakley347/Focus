using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSoundTriggerSwitch : MonoBehaviour
{
    public AudioSource floorsound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<CastPlayer>().currentfootstep.Contains(floorsound))
            {
                other.GetComponent<CastPlayer>().currentfootstep.Add(floorsound);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<CastPlayer>().currentfootstep.Contains(floorsound))
            {
                other.GetComponent<CastPlayer>().currentfootstep.Remove(floorsound);
            }
        }
    }
}
