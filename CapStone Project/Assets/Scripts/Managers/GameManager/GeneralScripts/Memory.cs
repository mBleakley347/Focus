﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : SCR_Interactable
{
    public GameObject memoryManager;
    public GameObject dadsgroup;
    public override bool Click(Vector3 a)
    {
        memoryManager.SetActive(true);
        dadsgroup.SetActive(true);
        return base.Click(a);
    }
}
