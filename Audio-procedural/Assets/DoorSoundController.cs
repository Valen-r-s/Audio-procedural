using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class DoorSoundController : MonoBehaviour
{
    public EventReference doorEvent; // Asigna aquí tu evento de FMOD desde el inspector.
    private EventInstance doorEventInstance;
    private bool isPlaying = false;

    private void Start()
    {
        // Crear la instancia del evento de sonido.
        doorEventInstance = RuntimeManager.CreateInstance(doorEvent);
    }

    public void PlayDoorSound(float doorState)
    {
        doorEventInstance.setParameterByName("Door", doorState);

        if (!isPlaying) // Evitar que se reproduzca varias veces al mismo tiempo
        {
            doorEventInstance.start();
            isPlaying = true;
        }
    }

    public void StopDoorSound()
    {
        doorEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        isPlaying = false;
    }

    private void OnDestroy()
    {
        doorEventInstance.release();
    }
}
