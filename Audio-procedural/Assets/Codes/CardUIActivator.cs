using UnityEngine;

public class CardUIActivator : MonoBehaviour
{
    public RaycastInteractor raycastInteractor;

    public void ActivarTarjetas()
    {
        raycastInteractor.ActivarEquipamientoDeTarjetas();
    }

    private void OnEnable()
    {
        ActivarTarjetas(); // Se llama cuando el UI se activa
    }
}
