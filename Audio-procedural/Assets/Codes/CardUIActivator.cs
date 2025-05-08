using UnityEngine;

public class CardUIActivator : MonoBehaviour
{
    public RaycastInteractor raycastInteractor;

   
    private void Start()
    {
        raycastInteractor.ActivarEquipamientoDeTarjetas();
    }
    
}
