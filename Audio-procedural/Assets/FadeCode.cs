using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage; // Imagen negra sobre el canvas
    public float fadeDuration = 1f;
    public MonoBehaviour cameraController;
    public GameObject[] objectsToActivateAfterFadeOut; // Objetos a activar al terminar el FadeOut
    public bool bockCam = false;

    private void Start()
    {
        fadeImage.gameObject.SetActive(true);
        PauseMenu.CanPaused = false;
        StartCoroutine(FadeOut());
        if (cameraController != null && bockCam == true)
            cameraController.enabled = false;
    }

    public void FadeToScene()
    {
        PauseMenu.CanPaused = false;
        StartCoroutine(FadeIn());
        
    }

    private IEnumerator FadeOut()
    {
        float t = 0f;
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(false); // Ocultar panel de fade

        PauseMenu.CanPaused = true;

        // Activar objetos después del fade out
        foreach (GameObject obj in objectsToActivateAfterFadeOut)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        if (cameraController != null)
            cameraController.enabled = true;
        
    }

    private IEnumerator FadeIn()
    {
        
        fadeImage.gameObject.SetActive(true);
        float t = 0f;
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        if (cameraController != null)
            cameraController.enabled = false;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;

        // Cambiar de escena después del fade in
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
