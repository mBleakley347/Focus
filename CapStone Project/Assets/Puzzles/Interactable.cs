using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable:MonoBehaviour
{
    public delegate void managerref(bool a);
    public managerref check;

    public virtual bool Click()
    {
        return true;
    }
}
