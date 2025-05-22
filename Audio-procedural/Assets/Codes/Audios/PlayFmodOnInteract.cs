using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PlayFmodOnInteract : MonoBehaviour
{
    private StudioEventEmitter emitter;

    private void Start()
    {
        emitter = GetComponent<StudioEventEmitter>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlaySound();
        }
    }

    public void PlaySound()
    {
        if (emitter != null)
        {
            emitter.Play();
        }
        else
        {
            Debug.LogWarning("No se encontró el StudioEventEmitter.");
        }
    }
}
