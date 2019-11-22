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
    public String homeScene;
    public CursorLockMode cursorMode;
    public bool paused = false;
    public bool menuUp = true;
    public bool textUp;
    public bool puzzleOn = false;
    public bool settingsOpen = false;
    
    [SerializeField] private GameObject currentFocus;
    [SerializeField] private GameObject focusText;
    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject menu;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().name;
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
        paused = false;
        Time.timeScale = 1;
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
    
    public void MenuScen()
    {
        menuUp = true;
        LoadNextScene(homeScene);
        menu.SetActive(true);
    }

    public void StartGame()
    {
        menuUp = false;
        menu.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.active) return;
            if (!escapeMenu.active)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.Confined;
                escapeMenu.SetActive(true);
                paused = true;
            }
            else
            {
                Time.timeScale = 1;
                Cursor.lockState = cursorMode;
                escapeMenu.SetActive(false);
                paused = false;
            }
        }
        if (!menuUp)
        {
            MoveCamera(Camera.main);
        }
    }

    private void MoveCamera(Camera camera)
    {

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

    public void ChangeCursorMode(bool locked)
    {
        Cursor.lockState = CursorLockMode.None;
        if (!locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
            cursorMode = CursorLockMode.Confined;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
           cursorMode = CursorLockMode.Locked;
        }
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void HelpText(string text)
    {
        if (!textUp)
        {
            focusText.SetActive(true);
            focusText.GetComponent<Text>().text = text;
            textUp = true;
        }
        else
        {
            focusText.SetActive(false);
            textUp = false;
        }
    }

    public void Settings()
    {
        settingsOpen = !settingsOpen;
        if (paused)
        {
            escapeMenu.SetActive(!escapeMenu.active);
        }
        if (menuUp)
        {
            menu.SetActive(!menu.active);
        }
        if (settingsOpen)
        {
            settingsMenu.SetActive(true);
        }
        else
        {
            settingsMenu.SetActive(false);
        }
    }  
}
