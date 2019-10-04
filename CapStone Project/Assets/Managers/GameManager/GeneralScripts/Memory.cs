using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : SCR_Interactable
{
    [SerializeField] private string scene;
    public override bool Click(Vector3 a)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Manager.instance.LoadNextScene(scene);
        return base.Click(a);
    }
}
