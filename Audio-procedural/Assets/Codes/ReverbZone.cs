using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ReverbZone : MonoBehaviour
{
    [Range(0f, 1f)]
    public float reverbValue = 0.5f;
    public FootstepSound footstepSound;

    private void OnTriggerEnter(Collider other)
    {
        if (footstepSound != null && other.CompareTag("Player"))
        {
            footstepSound.SetTargetReverb(reverbValue);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (footstepSound != null && other.CompareTag("Player"))
        {
            footstepSound.SetTargetReverb(0f);
        }
    }
}
