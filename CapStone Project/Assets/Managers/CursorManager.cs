using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public bool locked;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(locked);
        Manager.instance.ChangeCursorMode(locked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
