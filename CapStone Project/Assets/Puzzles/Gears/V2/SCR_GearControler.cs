using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_GearControler : SCR_Interactable
{
    public SCR_GearClass[] gears;

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

    private void RotateGear(SCR_GearClass scrGear)
    {
        if (scrGear.flipRotation)
        {
            scrGear.Completed = scrGear.gameObject.transform.localRotation * Quaternion.Euler(0, 0, -45);
        }
        else
        {
            scrGear.Completed = scrGear.gameObject.transform.localRotation * Quaternion.Euler(0, 0, 45);
        }
    }

    public override bool Click(Vector3 a)
    {
        GearRotateCheck();
        return true;
    }
}
