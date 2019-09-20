using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Gear : MonoBehaviour
{
    
    /*
     * needs a active bool
     * list of effected gears
     * gear that effected it
     * current rotation?
     * needed rotation
     *
     * funtion to rotate effected gears and check against the parent gear
     */

    public bool active;
    private SCR_Gear[] adjacent;
    public Vector3 rotationNeeded;
    private Vector3 rotationChange;
    public bool correct;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotate(SCR_Gear parent)
    {
        transform.Rotate(rotationChange);
        if (transform.eulerAngles == rotationNeeded) correct = true;
        else correct = false;

        foreach (var VARIABLE in adjacent)
        {
            if (VARIABLE != parent) VARIABLE.Rotate(this);
        }
    }
}
