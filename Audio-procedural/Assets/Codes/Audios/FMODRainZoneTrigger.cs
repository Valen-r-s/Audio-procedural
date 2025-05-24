using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMODRainZoneTrigger : MonoBehaviour
{
    public StudioEventEmitter rainEmitter;
    private int insideLluviaZones = 0;

    private void Start()
    {
        if (rainEmitter == null)
        {
            Debug.LogError("No se asignó el StudioEventEmitter.");
            return;
        }

        // Iniciar evento si no está sonando
        if (!rainEmitter.IsPlaying())
        {
            rainEmitter.Play();
        }

        // Asegurarse de que el parámetro comience en 0
        rainEmitter.SetParameter("LLUVIA Domo", 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lluvia"))
        {
            insideLluviaZones++;
            UpdateRainParameter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lluvia"))
        {
            insideLluviaZones = Mathf.Max(0, insideLluviaZones - 1);
            UpdateRainParameter();
        }
    }

    private void UpdateRainParameter()
    {
        if (rainEmitter != null)
        {
            rainEmitter.SetParameter("LLUVIA Domo", insideLluviaZones > 0 ? 1f : 0f);
        }
    }
}
