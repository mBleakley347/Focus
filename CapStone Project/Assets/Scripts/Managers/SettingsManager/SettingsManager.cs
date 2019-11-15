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
    [SerializeField] private Toggle viewbobBool;
    [SerializeField] private Toggle yaxisBool;

    // Start is called before the first frame update
    void Start()
    {
        voiceVolumeSlider.value = PlayerPrefs.GetFloat("voiceVolume", 1);
        masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume", 1);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume", 1);
        subtitleBool.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Subtitles", 0));
        viewbobBool.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Viewbob", 1));
        yaxisBool.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("InvertYAxis", 0));
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
        PlayerPrefs.SetInt("Viewbob", Convert.ToInt32(viewbobBool.isOn));
        PlayerPrefs.SetInt("InvertYAxis", Convert.ToInt32(yaxisBool.isOn));
        Manager.instance.MenuScene();
        SCR_AudioManager.instanceAM.UpdateAudioSettings();
    }
}
