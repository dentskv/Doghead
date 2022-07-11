using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Slider volumeSlider;

    void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("volume");
        audioSource.Play();
    }

    private void Update()
    {
        if (volumeSlider != null)
        {
            audioSource.volume = volumeSlider.value;
            PlayerPrefs.SetFloat("volume", audioSource.volume);
        }
    }
}
