using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para cargar la escena

public class RestartSceneOnTrigger : MonoBehaviour
{
    // Este método se llama cuando otro collider entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra al trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Reiniciar la escena actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
