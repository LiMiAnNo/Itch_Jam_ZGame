using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [Header("UI Components")]
    public Slider volumeSlider;

    [Header("Audio Settings")]
    public AudioMixer audioMixer; 
    public string volumeParameter = "MasterVolume"; 

    private void Start()
    {
        // Load saved volume level or default to 0.75
        float savedVolume = PlayerPrefs.GetFloat("VolumeLevel", 0.75f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        // listener
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        // Convert slider value (0.0001 to 1) to decibels (-80dB to 0dB)
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(volumeParameter, dB);

        // Save the volume setting
        PlayerPrefs.SetFloat("VolumeLevel", value);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }
}
