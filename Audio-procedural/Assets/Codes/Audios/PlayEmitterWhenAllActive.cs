using UnityEngine;
using System.Collections.Generic;
using FMODUnity;

public class PlayEmitterWhenAllActive : MonoBehaviour
{
    [Header("Objetos a monitorear")]
    public List<GameObject> objetosMonitoreados = new List<GameObject>();

    [Header("Event Emitter en este objeto")]
    public StudioEventEmitter eventEmitter;

    [Header("Luz a modificar")]
    public Light targetLight;
    public float nuevaIntensidad = 3f;

    private bool sonidoYaReproducido = false;

    void Update()
    {
        if (!sonidoYaReproducido && TodosEstanActivos())
        {
            if (eventEmitter != null)
            {
                eventEmitter.Play();
                Debug.Log("✅ Todos los objetos activos. Sonido reproducido.");
            }
            else
            {
                Debug.LogWarning("⚠️ No hay StudioEventEmitter asignado.");
            }

            if (targetLight != null)
            {
                targetLight.intensity = nuevaIntensidad;
                Debug.Log("💡 Intensidad de luz modificada.");
            }

            sonidoYaReproducido = true;
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
