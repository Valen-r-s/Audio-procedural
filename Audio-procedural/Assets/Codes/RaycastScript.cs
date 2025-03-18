using UnityEngine;
using System.Collections.Generic;

public class RaycastInteractor : MonoBehaviour
{
    public float rayDistance = 5f; // Distancia del raycast
    public LayerMask targetLayer; // Capa de los objetos interactuables
    public Camera playerCamera; // Referencia a la c�mara del jugador

    public List<GameObject> interactableObjects = new List<GameObject>(); // Lista de objetos invisibles
    public List<GameObject> spawnableObjects = new List<GameObject>(); // Lista de objetos a aparecer

    private bool canCastRay = false; // Variable que activa el Raycast solo en ciertas zonas

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canCastRay) // Solo si est� en una zona v�lida
        {
            ShootRay();
        }
    }

    void ShootRay()
    {
        RaycastHit hit;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        // Dibuja el Raycast en la escena para depuraci�n
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out hit, rayDistance, targetLayer))
        {
            if (hit.collider.CompareTag("Interactable")) // Solo interact�a con objetos marcados como "Interactable"
            {
                Debug.Log("Raycast impact�: " + hit.collider.name);

                // Buscar en la lista el objeto interactuado
                int index = interactableObjects.IndexOf(hit.collider.gameObject);
                if (index != -1 && index < spawnableObjects.Count)
                {
                    spawnableObjects[index].SetActive(true);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RaycastZone")) // Solo activa el Raycast en zonas con esta etiqueta
        {
            canCastRay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RaycastZone"))
        {
            canCastRay = false;
        }
    }
}
