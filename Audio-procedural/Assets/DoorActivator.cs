using UnityEngine;

public class DoorActivator : MonoBehaviour
{
    public Animator doorAnimator; // El Animator de la puerta
    public string boolParameterName = "DoorState"; // Nombre de la variable booleana en el Animator
    public bool Rain;
    public float TimeFadeRain;
    public FMODRainZoneTrigger RainZone;
    private void Start()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetBool(boolParameterName, true); // Cambia la variable booleana a true
            Debug.Log("Puerta activada con éxito.");
        }
        else
        {
            Debug.LogWarning("¡No se ha asignado ningún Animator de puerta!");
        }


        if (Rain)
        {
            RainZone.FadeOutAndStopRain(TimeFadeRain);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetBool(boolParameterName, false); // Cambia la variable booleana a true
            Debug.Log("Puerta activada con éxito.");
        }
        else
        {
            Debug.LogWarning("¡No se ha asignado ningún Animator de puerta!");
        }
    }
}
