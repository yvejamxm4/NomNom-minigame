using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controladorvolumen : MonoBehaviour
{
    public Slider volumeSlider;
    public GameObject closeButton;
    void Start()
    {
        volumeSlider.gameObject.SetActive(false);
        closeButton.SetActive(false);

        volumeSlider.value = PlayerPrefs.GetFloat("GameVolume", 1f);
        AudioListener.volume = volumeSlider.value;

        volumeSlider.onValueChanged.AddListener(ChangeVolume);

    }

    public void ToggleVolumeSlider()
    {
        bool isActive = volumeSlider.gameObject.activeSelf;
        volumeSlider.gameObject.SetActive(!isActive);
        closeButton.SetActive(!isActive);
    }

    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("GameVolume", volume);
    }
   
}
