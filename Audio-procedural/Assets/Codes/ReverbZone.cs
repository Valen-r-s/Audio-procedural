using UnityEngine;
using UnityEngine.UI; // Necesario para usar Sliders en el editor

[RequireComponent(typeof(Collider))]
public class ReverbZone : MonoBehaviour
{
    [Range(0f, 1f)] // Esto crea un Slider en el Inspector
    public float reverbValue = 0.5f; // Valor de reverb específico para esta zona
    public FootstepSound footstepSound;

    private void OnTriggerEnter(Collider other)
    {
        if (footstepSound != null)
        {
            footstepSound.SetTargetReverb(reverbValue);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        

        if (footstepSound != null)
        {
            footstepSound.SetTargetReverb(0f);
        }
    }
}
