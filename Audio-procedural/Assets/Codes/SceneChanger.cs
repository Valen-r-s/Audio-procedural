using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Method to change scenes by scene index
    public void ChangeScene(int sceneIndex)
    {
        // Ensure the scene index is within the valid range
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("Invalid scene index: " + sceneIndex);
        }
    }
}