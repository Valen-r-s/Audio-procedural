using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class NextLvlController : MonoBehaviour
{
    public List<GameObject> requiredObjects = new List<GameObject>(); // Lista de objetos que deben activarse
    public List<TextMeshProUGUI> interactionText = new List<TextMeshProUGUI>();// Lista de textos
    private int TextPos;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (interactionText != null)
            {
                interactionText[TextPos].gameObject.SetActive(false); // Ocultar texto
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Detecta al jugador
        {
            if (AllObjectsActivated())
            {
                Debug.Log("Todos los objetos están activados. Pasando al siguiente nivel...");
                TextPos = 1;
                interactionText[TextPos].gameObject.SetActive(true);
                LoadNextLevel();
            }
            else
            {
                TextPos = 0;
                interactionText[TextPos].gameObject.SetActive(true);
                Debug.Log("Aún hay objetos sin activar.");
            }
        }
    }

    private bool AllObjectsActivated()
    {
        foreach (GameObject obj in requiredObjects)
        {
            if (obj != null && !obj.activeSelf) // Si un objeto está inactivo, retorna falso
            {
                return false;
            }
        }
        return true;
    }

    private void LoadNextLevel()
    {
        // Aquí puedes cargar la siguiente escena o realizar cualquier acción
        Debug.Log("Cargando el siguiente nivel...");
        Invoke("ScenceChange", 2f); 
    }

    void ScenceChange()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
