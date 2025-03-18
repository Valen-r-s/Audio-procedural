using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PanelsSceneChanger : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;

    public void StartSequence(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(PanelSequence(sceneIndex));
        }
        else
        {
            Debug.LogError("Invalid scene index: " + sceneIndex);
        }
    }

    private IEnumerator PanelSequence(int sceneIndex)
    {
        if (panel1 != null)
        {
            panel1.SetActive(true);
        }

        yield return new WaitForSeconds(3);
        
        if (panel1 != null)
        {
            panel1.SetActive(false);
        }

        if (panel2 != null)
        {
            panel2.SetActive(true);
        }

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene(sceneIndex);
    }
}