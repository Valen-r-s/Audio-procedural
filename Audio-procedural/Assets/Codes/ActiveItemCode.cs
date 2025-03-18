using UnityEngine;
using TMPro; // Necesario para TextMeshPro

public class InteractiveObject : MonoBehaviour
{
    public TextMeshProUGUI interactionText; // Texto en pantalla
    public GameObject objectToActivate; // Objeto que se activará al presionar "E"
    public GameObject objectToDesactivate; // Objeto que se desactivará
    public float raycastDistance = 5f; // Distancia máxima del Raycast
    public Camera playerCamera; // Cámara del jugador (asignar en el Inspector)

    private bool isPlayerInTrigger = false; // Si el jugador está en la zona

    private void Start()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false); // Ocultar texto al inicio
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAtMe())
        {
            ActivateObject();
        }
    }

    private bool IsPlayerLookingAtMe()
    {
        if (playerCamera == null)
        {
            Debug.LogError("No se ha asignado la cámara del jugador.");
            return false;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red, 0.1f);

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            return hit.collider.gameObject == gameObject; // Detecta si está mirando este objeto
        }

        return false;
    }

    private void ActivateObject()
    {
        Debug.Log("Interacción exitosa: Activando objeto.");
        if (objectToActivate != null) objectToActivate.SetActive(true);
        if (objectToDesactivate != null) objectToDesactivate.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(true); // Mostrar texto
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false); // Ocultar texto
            }
        }
    }
}
