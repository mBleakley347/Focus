using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

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
    public AudioClip MenuMusic;
    public AudioClip MainMusic;
    public AudioClip[] Atmos;
    public Text Mainmenushiz;
    public Text Mainmenushiz2;
    public Image Mainmenushiz3;
    public int RandomTrack;
    public bool Done;

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
        }
        else if (instance != this)
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
        Cursor.lockState = CursorLockMode.Locked;
        LoadNextScene(currentScene);
    }

    public void MenuScen()
    {
        Cursor.lockState = CursorLockMode.Confined;
        menuUp = true;
        LoadNextScene(homeScene);
        menu.SetActive(true);

    }

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        menuUp = false;
        menu.GetComponentInChildren<Text>().CrossFadeAlpha(0.0f, 2.0f, false);
        Mainmenushiz.CrossFadeAlpha(0.0f, 2.0f, false);
        Mainmenushiz2.CrossFadeAlpha(0.0f, 2.0f, false);
        Mainmenushiz3.CrossFadeAlpha(0.0f, 2.0f, false);

        StartCoroutine(Manager.FadeOut());
        


        //menu.SetActive(false);
    } 
    // Start is called before the first frame update
    void Start()
    {
        RandomTrack = UnityEngine.Random.Range(0, Atmos.Length);
        Cursor.lockState = CursorLockMode.Confined;
        SCR_AudioManager.instanceAM.musicSouce.clip = MenuMusic;
        SCR_AudioManager.instanceAM.atmosSouce.clip = Atmos[RandomTrack];
        SCR_AudioManager.instanceAM.atmosSouce.Play();
        SCR_AudioManager.instanceAM.musicSouce.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.active || settingsOpen) return;
            if (!escapeMenu.active)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.Confined;
                AudioListener.pause = true;
                escapeMenu.SetActive(true);
                paused = true;
            }
            else
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
                Cursor.lockState = CursorLockMode.Locked;
                escapeMenu.SetActive(false);
                paused = false;
            }
        }
        if (!menuUp)
        {

        }
        if (SCR_AudioManager.instanceAM.voiceSouce.isPlaying == false)
        {
            RandomTrack = UnityEngine.Random.Range(0, Atmos.Length);
            SCR_AudioManager.instanceAM.atmosSouce.clip = Atmos[RandomTrack];
            SCR_AudioManager.instanceAM.atmosSouce.Play();
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

    public static IEnumerator FadeOut()
    {
        Debug.Log("yoyo");
        float startVolume = SCR_AudioManager.instanceAM.musicSouce.volume;

        while (SCR_AudioManager.instanceAM.musicSouce.volume > 0)
        {
            SCR_AudioManager.instanceAM.musicSouce.volume -= startVolume * Time.deltaTime / 5.0f;

            yield return null;
        }

        SCR_AudioManager.instanceAM.musicSouce.Stop();
        SCR_AudioManager.instanceAM.musicSouce.volume = startVolume;
       
        Manager.instance.menu.SetActive(false);
        Manager.instance.Playmusic();

    }

    public void Playmusic()
    {
        SCR_AudioManager.instanceAM.musicSouce.clip = MainMusic;
        SCR_AudioManager.instanceAM.musicSouce.Play();
    }

}