using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private Slider sfxSlider;
    [SerializeField] private AudioSource sfxSource;

    private const string MusicVolumePrefKey = "MusicVolume";
    private const string SFXVolumePrefKey = "SFXcVolume";

    private void Start()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumePrefKey, 1f);
        musicSlider.value = savedMusicVolume;
        musicSource.volume = savedMusicVolume;

        musicSlider.onValueChanged.AddListener(OnMusicSliderValueChanged);

        float savedSFXVolume = PlayerPrefs.GetFloat(SFXVolumePrefKey, 1f);
        musicSlider.value = savedSFXVolume;
        musicSource.volume = savedSFXVolume;

        musicSlider.onValueChanged.AddListener(OnSFXSliderValueChanged);
    }

    private void OnMusicSliderValueChanged(float value)
    {
        musicSource.volume = value;

        PlayerPrefs.SetFloat(MusicVolumePrefKey, value);
        PlayerPrefs.Save();
    }

    private void OnSFXSliderValueChanged(float value)
    {
        sfxSource.volume = value;

        PlayerPrefs.SetFloat(SFXVolumePrefKey, value);
        PlayerPrefs.Save();
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }


}
