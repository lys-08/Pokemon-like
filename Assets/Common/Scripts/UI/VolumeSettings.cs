using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private float volume_ = 0f;


    public void SetMasterVolume()
    {
        volume_ = Mathf.Log(masterSlider.value) * 20f;
        mixer.SetFloat("Master", volume_);
        PlayerPrefs.SetFloat("masterVolume", volume_);
    }
    
    public void SetMusicVolume()
    {
        volume_ = Mathf.Log(musicSlider.value) * 20f;
        mixer.SetFloat("Music", volume_);
        PlayerPrefs.SetFloat("musicVolume", volume_);
    }
    
    public void SetSFXVolume()
    {
        volume_ = Mathf.Log(sfxSlider.value) * 20f;
        mixer.SetFloat("SFX", volume_);
        PlayerPrefs.SetFloat("sfxVolume", volume_);
    }

    public void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey("masterVolume") && PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("sfxVolume")) LoadVolume();
        else SetMusicVolume();
    }
}
