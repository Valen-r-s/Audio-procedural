using UnityEngine;
using System.Collections.Generic;
using FMODUnity;

public class PlayEmitterWhenAllActive : MonoBehaviour
{
    [Header("Objetos a monitorear")]
    public List<GameObject> objetosMonitoreados = new List<GameObject>();

    [Header("Event Emitter en este objeto")]
    public StudioEventEmitter eventEmitter;

    private bool sonidoYaReproducido = false;

    void Update()
    {
        if (!sonidoYaReproducido && TodosEstanActivos())
        {
            if (eventEmitter != null)
            {
                eventEmitter.Play();
                sonidoYaReproducido = true;
                Debug.Log("✅ Todos los objetos activos. Sonido reproducido.");
            }
            else
            {
                Debug.LogWarning("⚠️ No hay StudioEventEmitter asignado.");
            }
        }
    }

    private bool TodosEstanActivos()
    {
        foreach (GameObject obj in objetosMonitoreados)
        {
            if (obj == null || !obj.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }
}
