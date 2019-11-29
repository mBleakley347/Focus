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
            other.GetComponent<CastPlayer>().numcolliders += 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CastPlayer>().numcolliders -= 1;
        }
    }
}
