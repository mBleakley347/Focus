using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable:MonoBehaviour
{
    public delegate void managerref(bool a);
    public event managerref check;

    public virtual bool Click()
    {
       
        return true;
    }
    public virtual bool Hold(Vector3 a)
    {
        
        return true;
    }

    public virtual bool Release()
    {
        
        return true;
    }
}
