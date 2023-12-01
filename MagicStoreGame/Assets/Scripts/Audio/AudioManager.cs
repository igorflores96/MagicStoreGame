using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private float volumeChangePercentage = 0.05f; // Porcentagem para aumentar/diminuir o volume
    private int currentMusicIndex = 0; // Índice da música atual

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
        PlayMusic(0); // Toca a primeira música no início do jogo
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
            if (s.isMusic)
            {
                StopAllMusic();
                PlayMusic(Array.IndexOf(sounds, s)); // Toca a música específica
            }
            else
            {
                s.source.Play();
            }
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s != null)
        {
            if (s.source.isPlaying)
            {
                s.source.Stop();
            }
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

    void PlayMusic(int index)
    {
        currentMusicIndex = index;
        sounds[currentMusicIndex].source.Play();
        sounds[currentMusicIndex].source.loop = false;
        sounds[currentMusicIndex].source.SetScheduledEndTime(AudioSettings.dspTime + sounds[currentMusicIndex].clip.length);
        Invoke("PlayNextMusic", sounds[currentMusicIndex].clip.length);
    }

    void PlayNextMusic()
    {
        currentMusicIndex = (currentMusicIndex + 1) % sounds.Length;
        PlayMusic(currentMusicIndex);
    }
}




//FindObjectOfType<AudioManager>().Play("AudioName");