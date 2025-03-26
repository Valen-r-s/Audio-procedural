using UnityEngine;
using TMPro; // Necesario para TextMeshPro
using System.Collections.Generic;

public class InteractiveObject : MonoBehaviour
{
    public TextMeshProUGUI interactionText; // Texto en pantalla
    public List<GameObject> objectsToActivate = new List<GameObject>(); // Lista de objetos a activar
    public List<GameObject> objectsToDeactivate = new List<GameObject>(); // Lista de objetos a desactivar
    public float raycastDistance = 5f; // Distancia máxima del Raycast
    public Camera playerCamera; // Cámara del jugador (asignar en el Inspector)
    private BoxCollider boxCollider; // Referencia al BoxCollider del objeto

    private bool isPlayerInTrigger = false; // Si el jugador está en la zona

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>(); // Obtener el BoxCollider
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false); // Ocultar texto al inicio
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAtMe())
        {
            ActivateObjects();
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

    private void ActivateObjects()
    {
        Debug.Log("Interacción exitosa: Activando y desactivando objetos.");

        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null) obj.SetActive(true);
        }

        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null) obj.SetActive(false);
        }

        if (boxCollider != null)
        {
            boxCollider.enabled = false; // Desactivar el BoxCollider después de la interacción
        }
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
