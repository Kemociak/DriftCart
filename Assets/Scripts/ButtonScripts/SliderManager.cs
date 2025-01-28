using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer audioMixer;
    private const string VolumePref = "MasterVolume";

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumePref, 1f); 
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20); // Skala logarytmiczna
        PlayerPrefs.SetFloat(VolumePref, volume);
        PlayerPrefs.Save();
    }
}
