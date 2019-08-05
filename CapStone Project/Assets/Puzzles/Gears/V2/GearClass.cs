using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GearClass : PuzzleComponent
{
    public Vector3 onPosition;
    public Vector3 offPosition;
    public bool active;
    public bool Constant;
    public bool flipRotation;
    public GearClass[] parents;
    public Vector3 correctRotation;
    
    

    public Quaternion Completed
    {
        get { return transform.localRotation; }
        set
        {
            if (value != transform.localRotation)
            {
                //angles must be in positive values
                if (value.eulerAngles.z >= correctRotation.z - 1 && value.eulerAngles.z <= correctRotation.z + 1)
                {
                    complete = true;
                }
                else
                {
                    complete = false;
                }
            }
            
            transform.localRotation = value;
        }
    }

    public override bool Hold(Vector3 cursorPosition)
    {
        if (Constant) return true;
        Vector3 temp = Vector3.Project(transform.parent.InverseTransformPoint(cursorPosition), (offPosition - onPosition));
        float dist = Vector3.Dot((offPosition - onPosition).normalized,(offPosition-temp).normalized)>0? Vector3.Distance(temp, offPosition):0;
        Vector3 temp2 = Vector3.Lerp(offPosition, onPosition, dist/Vector3.Distance(offPosition,onPosition));
        transform.localPosition = temp2;
        return true; 
    }

    public override bool Release()
    {
        if (Constant) return true;
        if (Vector3.Distance(offPosition, gameObject.transform.localPosition) >
            Vector3.Distance(onPosition, gameObject.transform.localPosition))
        {
            transform.localPosition = onPosition;
            active = true;
        }
        else
        {
            transform.localPosition = offPosition;
            active = false;
        }
        return true;
    }

    private void Start()
    {
        //onPosition = transform.InverseTransformPoint(onPosition);
        //offPosition = transform.InverseTransformPoint(offPosition);
        if (parents.Length != 0)
        {
            flipRotation = !parents[0].flipRotation;
        }
        if (Constant) return;
        if (!active) transform.transform.localPosition = offPosition;
        if (active) transform.transform.localPosition = onPosition;
    }
}
