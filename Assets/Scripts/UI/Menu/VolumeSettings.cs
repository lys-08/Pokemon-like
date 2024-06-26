using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private TextMeshProUGUI masterValue;
    [SerializeField] private TextMeshProUGUI musicValue;
    [SerializeField] private TextMeshProUGUI sfxValue;

    float Remap(float value, float min1, float max1, float min2, float max2) 
{
    return min2 + (value - min1) * (max2 - min2) / (max1 - min1);
}
    

    #region Unity Events Methods

    private void Awake()
    {
        /*
         * TODO : c'est là, à chaque GetFloat il trouve pas
         */
        
        mixer.GetFloat("Master", out float mainVolume);
        mixer.GetFloat("Music", out float musicVolume);
        mixer.GetFloat("SFX", out float sfxVolume);
        
        masterSlider.value = Mathf.Exp(mainVolume / 20f);
        // masterSlider.value = Remap(mainVolume, -80f, 0f, 0f, 1f);
        musicSlider.value = Mathf.Exp(musicVolume / 20);
        // musicSlider.value = Remap(musicVolume, -80f, 0f, 0f, 1f);
        sfxSlider.value = Mathf.Exp(sfxVolume / 20);
        // sfxSlider.value = Remap(sfxVolume, -80f, 0f, 0f, 1f);
    }

    private void Start()
    {

        // if(PlayerPrefs.HasKey("masterVolume") && PlayerPrefs.HasKey("musicVolume") && PlayerPrefs.HasKey("sfxVolume")) LoadVolume();
        // else
        // {
        //     masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        //     musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        //     sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        // }

        masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    #endregion
    
    
    public void OnMasterVolumeChanged(float value)
    {
        float volume = Mathf.Log10(value) * 20f;
        mixer.SetFloat("Master", volume);
        PlayerPrefs.SetFloat("masterVolume", volume);

        masterValue.text = ((int)value).ToString();
    }
    
    public void OnMusicVolumeChanged(float value)
    {
        float volume = Mathf.Log10(value) * 20f;
        mixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("musicVolume", volume);
        
        musicValue.text = ((int)value).ToString();
    }
    
    public void OnSFXVolumeChanged(float value)
    {
        float volume = Mathf.Log10(value) * 20f;
        mixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("sfxVolume", volume);
        
        sfxValue.text = ((int)value).ToString();
    }

    public void LoadVolume()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        
        masterValue.text = masterSlider.value.ToString();
        musicValue.text = musicSlider.value.ToString();
        sfxValue.text = sfxSlider.value.ToString();
    }
}

