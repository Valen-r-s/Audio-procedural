using UnityEngine;
using TMPro;

public class CardPickup : MonoBehaviour
{
    public GameObject Slot;
    public int cardIndex; // 1, 2 o 3
    public RaycastInteractor raycastInteractor;
    public Camera playerCamera;
    public float raycastDistance = 5f;
    public TextMeshProUGUI interactionText;

    private bool isPlayerInTrigger = false;

    void Start()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false); // Ocultar al inicio
        }
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E) && IsPlayerLookingAtMe())
        {
            CollectCard();
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
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.yellow, 0.1f);

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            return hit.collider.gameObject == gameObject;
        }

        return false;
    }

    private void CollectCard()
    {
        raycastInteractor.UnlockCard(cardIndex);
        Debug.Log("Tarjeta " + (cardIndex + 1) + " recogida.");
        Slot.SetActive(true);
        GetComponent<PlayFmodOnInteract>()?.PlaySound();

        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(true); // Mostrar al entrar
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
                interactionText.gameObject.SetActive(false); // Ocultar al salir
            }
        }
    }
}
