using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSceneChange : MonoBehaviour
{
    private int nextScene;
    [SerializeField] private float time;
    // Start is called before the first frame update
    void Start()
    {
        nextScene = Manager.instance.homeLevel + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0)
        {
            Manager.instance.homeLevel = nextScene;
            Manager.instance.HomeScene();
        }
        time -= Time.deltaTime;
    }
}
