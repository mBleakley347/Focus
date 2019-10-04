using System;
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
    public CursorLockMode cursorMode;
    
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
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentScene != homeScene[0]) escapeMenu.SetActive(!escapeMenu.active);
            if (escapeMenu.active)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Debug.Log("worked");
            }
            else
            {
                Cursor.lockState = cursorMode;
            }
            Debug.Log(Manager.instance.cursorMode);
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
}
