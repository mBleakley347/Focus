using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_AudioManager : MonoBehaviour
{
    public static SCR_AudioManager instanceAM;
    [SerializeField] public AudioSource musicSouce;
    [SerializeField] public AudioSource voiceSouce;
    [SerializeField] public AudioSource voiceSouce2;
    public AudioClip[] Smoking;

    private void Awake()
    {
        if (instanceAM == null)
        {
            instanceAM = this;
        } else if (instanceAM != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateAudioSettings();
    }

    public void UpdateAudioSettings()
    {
        float music = PlayerPrefs.GetFloat("musicVolume");
        float voice = PlayerPrefs.GetFloat("voiceVolume");
        music *= PlayerPrefs.GetFloat("masterVolume");
        voice *= PlayerPrefs.GetFloat("masterVolume");

        musicSouce.volume = music;
        voiceSouce.volume = voice;
        voiceSouce2.volume = voice;
    }
}
