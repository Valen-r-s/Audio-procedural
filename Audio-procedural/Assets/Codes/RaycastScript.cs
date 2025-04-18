using UnityEngine;
using System.Collections.Generic;

public class RaycastInteractor : MonoBehaviour
{
    public float rayDistance = 5f;
    public float rayCooldown = 0.5f; // Tiempo de enfriamiento entre raycasts
    private float rayTimer = 0f;

    public LayerMask targetLayer;
    public Camera playerCamera;

    public List<GameObject> interactableObjects = new List<GameObject>();
    public List<GameObject> spawnableObjects = new List<GameObject>();
    public List<int> requiredCardIndex = new List<int>();

    private bool canCastRay = false;
    private bool hasCardEquipped = false;
    private int equippedCardIndex = -1;
    private bool isAiming = false;

    public GameObject CardCont;
    private Animator cardAnimator;
    private bool isChangingCard = false; // Evita apuntar mientras se cambia de carta
    void Start()
    {
        cardAnimator = CardCont.GetComponent<Animator>();
    }

    public void SetIsChangingCard(bool value)
    {
        isChangingCard = value;
    }

    void Update()
    {
        rayTimer -= Time.deltaTime;

        // No cambiar tarjeta si está apuntando o cambiando
        if (!isChangingCard)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) EquipCard(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) EquipCard(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) EquipCard(2);
            if (Input.GetKeyDown(KeyCode.Alpha4)) EquipCard(3);
        }

        // Click sostenido → activar animación (si no se está cambiando carta)
        if (Input.GetMouseButtonDown(0) && hasCardEquipped && !isChangingCard)
        {
            cardAnimator.SetInteger("EstadoCarta", 2); // 2 = Apuntar
        }

        // Raycast solo cuando se está apuntando
        if (Input.GetMouseButton(0) && isAiming && canCastRay && rayTimer <= 0f)
        {
            ShootRay();
            rayTimer = rayCooldown;
        }

        // Al soltar click → volver a estado normal (si no está cambiando)
        if (Input.GetMouseButtonUp(0) && !isChangingCard)
        {
            cardAnimator.SetInteger("EstadoCarta", 0); // 0 = Normal
        }
    }


    void EquipCard(int cardIndex)
    {
        if (equippedCardIndex != cardIndex)
        {
            isAiming = false; // Cancelar apuntado
            isChangingCard = true;
            equippedCardIndex = cardIndex;
            cardAnimator.SetInteger("EstadoCarta", 1); // 1 = Cambiar carta
            Debug.Log("Tarjeta equipada: " + (cardIndex + 1));
        }
    }


    void ShootRay()
    {
        RaycastHit hit;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out hit, rayDistance, targetLayer))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Debug.Log("Raycast impactó: " + hit.collider.name);

                int index = interactableObjects.IndexOf(hit.collider.gameObject);
                if (index != -1 && index < spawnableObjects.Count)
                {
                    if (equippedCardIndex == requiredCardIndex[index])
                    {
                        spawnableObjects[index].SetActive(true);
                        Debug.Log("Objeto activado");
                    }
                    else
                    {
                        Debug.Log("Tarjeta incorrecta para este objeto.");
                    }
                }
            }
        }
    }

    public int GetEquippedCardIndex()
    {
        return equippedCardIndex;
    }

    public void ActivarEquipamientoDeTarjetas()
    {
        hasCardEquipped = true;
        equippedCardIndex = 0;
        CardCont.SetActive(true);
        Debug.Log("Ahora puedes equipar y usar tarjetas.");
    }

    // Estos métodos se llaman desde eventos en la animación
    public void SetIsAiming(bool value)
    {
        isAiming = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RaycastZone"))
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
