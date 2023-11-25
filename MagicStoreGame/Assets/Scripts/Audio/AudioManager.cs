using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

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

    void Start ()
    {
        Play("TesteMusic1"); // Toca a música no inicio do jogo
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

            if (s.isMusic) { StopAllMusic(); } //Caso tenhaa a boleana 'isMusic' true, desativa a musica atual 
            
            s.source.Play(); //Da play no audio
    }

    void StopAllMusic()
    {
        foreach (Sound s in sounds)
        {
            if (s.isMusic && s.source.isPlaying) { s.source.Stop(); }
        }
    }
}

//FindObjectOfType<AudioManager>().Play("AudioName");