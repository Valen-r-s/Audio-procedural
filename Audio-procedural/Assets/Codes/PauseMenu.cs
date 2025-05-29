using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;

    public MonoBehaviour playerCameraController;

    public static bool isPaused  = false;
    public static bool CanPaused = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && CanPaused == true)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuPanel.SetActive(true);

        if (playerCameraController != null)
        {
            playerCameraController.enabled = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuPanel.SetActive(false);

        if (playerCameraController != null)
        {
            playerCameraController.enabled = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isPaused = false;
    }
}