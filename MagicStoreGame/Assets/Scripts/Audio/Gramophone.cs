using UnityEngine;

public class Gramophone : MonoBehaviour
{
    private AudioManager audioManager;
    private int currentMusicIndex = 0;

    void Start()
    {
        audioManager = AudioManager.instance;
    }

    void Update()
    {
        //DEBUG
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    PlayNextMusic();
        //}
    }

    void PlayNextMusic()
    {
        bool foundNextMusic = false;

        for (int i = 0; i < audioManager.sounds.Length; i++)
        {
            currentMusicIndex++;

            if (currentMusicIndex >= audioManager.sounds.Length)
            {
                currentMusicIndex = 0; // Volta para o início da lista
            }

            Sound nextMusic = audioManager.sounds[currentMusicIndex];

            if (nextMusic.isMusic)
            {
                audioManager.StopAllMusic(); // Para todas as músicas atuais
                audioManager.Play(nextMusic.name); // Toca a próxima música
                foundNextMusic = true;
                break;
            }
        }
    }
}
