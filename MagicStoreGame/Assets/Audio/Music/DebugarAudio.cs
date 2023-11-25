using UnityEngine;

public class DebugarAudio : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            FindObjectOfType<AudioManager>().Play("TesteMusic2");
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            FindObjectOfType<AudioManager>().Play("TesteSFX1");
        }
    }
}