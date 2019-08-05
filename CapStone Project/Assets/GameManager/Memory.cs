using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory : Interactable
{
    [SerializeField] private string scene;
    public override bool Click(Vector3 a)
    {
        Manager.instance.LoadNextScene(scene);
        return base.Click(a);
    }
}
