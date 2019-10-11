﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Interactable:MonoBehaviour
{
    public delegate void managerref(bool a);
    public event managerref check;

    public virtual bool Click(Vector3 a)
    {
        if (Manager.instance.paused) return true;
        return true;
    }
    public virtual bool Hold(Vector3 a)
    {
        if (Manager.instance.paused) return true;
        return true;
    }

    public virtual bool Release()
    {
        if (Manager.instance.paused) return true;
        return true;
    }
}
