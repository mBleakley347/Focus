using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public bool locked;

    // Start is called before the first frame update
    void Start()
    {
        if (!locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Manager.instance.cursorMode = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Manager.instance.cursorMode = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
