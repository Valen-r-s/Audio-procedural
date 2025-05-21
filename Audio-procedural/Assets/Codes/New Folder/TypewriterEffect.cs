using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject panel;                     
    public TextMeshProUGUI dialogueText;         
    public MonoBehaviour cameraController;      

    [Header("Configuración del texto")]
    [TextArea(3, 10)]
    public string fullText;                     
    public float typingSpeed = 0.05f;           

    void Start()
    {
        PauseMenu.CanPaused = false;
        if (panel != null && panel.activeSelf)
        {
            // Desactivar movimiento de cámara
            if (cameraController != null)
                cameraController.enabled = false;

            StartCoroutine(TypeText());
        }
    }

    IEnumerator TypeText()
    {
        dialogueText.text = "";

        foreach (char c in fullText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Esperar un poco antes de cerrar
        yield return new WaitForSeconds(1f);

        // Reactivar cámara
        if (cameraController != null)
            cameraController.enabled = true;

        // Ocultar el panel
        panel.SetActive(false);
        PauseMenu.CanPaused = true;
    }
}
