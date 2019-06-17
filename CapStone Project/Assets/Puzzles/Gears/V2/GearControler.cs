using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearControler : Interactable
{
    public GearClass[] gears;

    public void GearRotateCheck()
    {
        foreach (var gear in gears)
        {
            if (gear.active)
            {
                if (gear.parents.Length != 0)
                {
                    foreach (var parent in gear.parents)
                    {
                        if (parent.active)
                        {
                            RotateGear(gear);
                            return;
                        }
                    }
                } else RotateGear(gear);
            }
        }
    }

    private void RotateGear(GearClass gear)
    {
        gear.gameObject.transform.Rotate(0,0,45f);
        gear.Completed = gear.gameObject.transform.rotation;
    }

    public override bool Click()
    {
        GearRotateCheck();
        return true;
    }
}
