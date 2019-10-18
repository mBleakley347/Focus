using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeInterface : MonoBehaviour
{
    public void SceneChange(string scene)
    {
        Manager.instance.LoadNextScene(scene);
    }

    public void Exit()
    {
        Manager.instance.Exit();
    }
}
