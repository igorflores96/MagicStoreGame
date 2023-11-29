using UnityEngine;

public class GamePause : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseCanvas; // Reference to the Pause Panel GameObject
    public GameObject playerObject; // Reference to the player object with the MovementPlayer script

    private MonoBehaviour movementPlayerScript; // Reference to the MovementPlayer script

    void Start()
    {
        // Make sure to disable the pause panel at the start so that it's not displayed initially
        pauseCanvas.SetActive(false);

        // Get the MovementPlayer script from the player object
        movementPlayerScript = playerObject.GetComponent<MonoBehaviour>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // Pause the game's time
            pauseCanvas.SetActive(true); // Activate the pause panel

            // Disable the MovementPlayer script
            if (movementPlayerScript != null)
            {
                movementPlayerScript.enabled = false;
            }
        }
        else
        {
            Time.timeScale = 1; // Resume the game's time
            pauseCanvas.SetActive(false); // Deactivate the pause panel

            // Enable the MovementPlayer script
            if (movementPlayerScript != null)
            {
                movementPlayerScript.enabled = true;
            }
        }
    }
}
