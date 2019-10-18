using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider voiceVolumeSlider;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Toggle subtitleBool;

    // Start is called before the first frame update
    void Start()
    {
        voiceVolumeSlider.value = PlayerPrefs.GetFloat("voiceVolume");
        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        subtitleBool.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Subtitles"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Menu()
    {
        //apply changes upon exiting to Main menu
        PlayerPrefs.SetFloat("voiceVolume", voiceVolumeSlider.value);
        PlayerPrefs.SetFloat("masterVolume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetInt("Subtitles", Convert.ToInt32(subtitleBool.isOn));
        Manager.instance.MenuScene();
        SCR_AudioManager.instanceAM.UpdateAudioSettings();
    }
}
