using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMemory : InteractableObject
{
    public GameObject memoryManager;
    public GameObject dadsgroup;

    public override void Use(CastPlayer player)
    {
        memoryManager.SetActive(true);
        dadsgroup.SetActive(true);
    }
}
