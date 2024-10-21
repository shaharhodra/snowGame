using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject startMenuUI;          // Reference to the Start Menu UI
    public GameObject pauseMenuUI;          // Reference to the Pause Menu UI
    public GameObject characterUICanvas;    // Reference to the Character UI Canvas
    public Transform characterCameraPosition; // Position the camera should move to
    public float transitionSpeed = 2f;      // Speed of the camera transition
    private bool moveToCharacter = false;   // Flag to trigger the camera movement

    // Reference to the Main Camera
    private Camera mainCamera;

    void Start()
    {
        // Ensure the Character UI and Pause Menu are hidden initially
        characterUICanvas.SetActive(false);
        pauseMenuUI.SetActive(false);

        // Get the Main Camera
        mainCamera = Camera.main;

        // Set the time scale back to normal in case it was paused
        Time.timeScale = 1;
    }

    void Update()
    {
        // Smoothly move the camera to the character's position
        if (moveToCharacter)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, characterCameraPosition.position, Time.deltaTime * transitionSpeed);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, characterCameraPosition.rotation, Time.deltaTime * transitionSpeed);
        }

        // Handle pause input (e.g., pressing Esc or P key)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    // Called when the Start button is pressed
    public void OnStartButtonPress()
    {
        // Start the camera transition to the character
        moveToCharacter = true;

        // Show the Character UI and hide the Start Menu UI
        characterUICanvas.SetActive(true);
        startMenuUI.SetActive(false);
    }

    // Called when the Exit button is pressed
    public void OnExitButtonPress()
    {
        // Exit the game (note: this will not work in the editor, only in a built game)
        Application.Quit();
    }

    // Called when the Restart button is pressed
    public void OnRestartButtonPress()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Toggle the pause menu on/off
    public void TogglePauseMenu()
    {
        if (pauseMenuUI.activeSelf)
        {
            // Resume the game
            Time.timeScale = 1;
            pauseMenuUI.SetActive(false);
        }
        else
        {
            // Pause the game
            Time.timeScale = 0;
            pauseMenuUI.SetActive(true);
        }
    }
}

