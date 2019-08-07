﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager instance = null;
    public String currentScene;
    public String previousScene;
    public int homeLevel;
    public String[] homeScene;
    
    [SerializeField] private GameObject currentFocus;
    [SerializeField] private GameObject focusText;

    [SerializeField] private GameObject escapeMenu;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadNextScene(String nextScene)
    {
        if (nextScene != null)
        {
            escapeMenu.SetActive(false);
            ChangeFocus(null);
            if (currentScene != currentScene) previousScene = currentScene;
            currentScene = nextScene;
            SceneManager.LoadScene(currentScene);
        }
    }

    public void ResetScene()
    {
        LoadNextScene(currentScene);
    }

    public void HomeScene()
    {
        LoadNextScene(homeScene[homeLevel]);
    }
    
    public void MenuScene()
    {
        homeLevel = 0;
        LoadNextScene(homeScene[homeLevel]);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escapeMenu.SetActive(!escapeMenu.active);
        }
    }

    public void ChangeFocus(GameObject newObject)
    {
        if (newObject != null)
        {
            currentFocus = newObject;
            focusText.SetActive(true);
            focusText.GetComponent<Text>().text = "Press E to Load" + currentFocus.name;
        }
        else
        {
            currentFocus = null;
            focusText.SetActive(false);
        }
    }
}
