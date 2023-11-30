using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private float volumeChangePercentage = 0.05f; // Porcentagem para aumentar/diminuir o volume

    public static AudioManager instance;

    public Slider volumeSlider; 
    public Slider sfxSlider; 


    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("TesteMusic1"); // Toca a música no início do jogo
    }

    void AdjustMusicVolume(bool increase)
    {
        foreach (Sound s in sounds)
        {
            if (s.isMusic)
            {
                if (increase)
                {
                    s.volume = Mathf.Clamp01(s.volume + (s.volume * volumeChangePercentage)); // Aumenta o volume
                }
                else
                {
                    s.volume = Mathf.Clamp01(s.volume - (s.volume * volumeChangePercentage)); // Diminui o volume
                }

                s.source.volume = s.volume; // Atualiza o volume do AudioSource
            }
        }
    }

    public void OnMusicSliderChanged()
    {
        foreach (Sound s in sounds)
        {
            if (s.isMusic)
            {
                s.volume = Mathf.Clamp01(volumeSlider.value); // Atualiza o volume baseado no valor do slider
                s.source.volume = s.volume; // Atualiza o volume do AudioSource
            }
        }
    }

    void AdjustSFXVolume(bool increase)
    {
        foreach (Sound s in sounds)
        {
            if (s.isMusic == false)
            {
                if (increase)
                {
                    s.volume = Mathf.Clamp01(s.volume + (s.volume * volumeChangePercentage)); // Aumenta o volume
                }
                else
                {
                    s.volume = Mathf.Clamp01(s.volume - (s.volume * volumeChangePercentage)); // Diminui o volume
                }

                s.source.volume = s.volume; // Atualiza o volume do AudioSource
            }
        }
    }

    public void OnSFXSliderChanged()
    {
        foreach (Sound s in sounds)
        {
            if (s.isMusic == false)
            {
                s.volume = Mathf.Clamp01(volumeSlider.value); // Atualiza o volume baseado no valor do slider
                s.source.volume = s.volume; // Atualiza o volume do AudioSource
            }
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
            if (s.isMusic)
            {
                StopAllMusic();
            }

            s.source.Play();
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    public void StopAllMusic()
    {
        foreach (Sound s in sounds)
        {
            if (s.isMusic && s.source.isPlaying)
            {
                s.source.Stop();
            }
        }
    }
}


//FindObjectOfType<AudioManager>().Play("AudioName");