using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GearClass : MonoBehaviour
{
    public Vector3 onPosition;
    public Vector3 offPosition;
    public bool active;
    public bool Constant;
    public GearClass[] parents;
    public Quaternion correctRotation;

    public void Change()
    {
        if (Constant) return;
        active = !active;
        if (!active) transform.transform.position = offPosition;
        if (active) transform.transform.position = onPosition;
    }

    private void Start()
    {
        if (!active) transform.transform.position = offPosition;
        if (active) transform.transform.position = onPosition;
    }
}
