using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private bool active = false;
    public string scene;

    private void Start()
    {
        if (active) Manager.instance.ChangeFocus(gameObject);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && active)
        {
            Manager.instance.LoadNextScene(scene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            active = true;
            Manager.instance.ChangeFocus(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            active = false;
            Manager.instance.ChangeFocus(null);
        }
    }
}
