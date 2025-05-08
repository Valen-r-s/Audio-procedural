using UnityEngine;

public class CardPickup : MonoBehaviour
{
    public int cardIndex; // 1, 2 o 3 (ya que la 0 está desbloqueada al inicio)
    public RaycastInteractor raycastInteractor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            raycastInteractor.UnlockCard(cardIndex);
            Debug.Log("Tarjeta " + (cardIndex + 1) + " recogida.");
            Destroy(gameObject); // Elimina el pickup del mundo
        }
    }
}
