using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[RequireComponent(typeof(CharacterController))]
public class FootstepSound : MonoBehaviour
{
    public EventReference footstepEvent; // Arrastra tu evento de FMOD aquí
    private EventInstance footstepInstance;
    private CharacterController characterController;
    private bool isPlaying;

    [Header("Reverb Settings")]
    public float reverbTransitionTime = 0.2f; // Tiempo en segundos que tarda la transición
    private float currentReverb = 0f; // Valor actual de la reverberación
    private float targetReverb = 0f; // Valor al que se quiere llegar
    private float transitionTimer = 0f; // Controla el tiempo transcurrido en la transición

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        footstepInstance = RuntimeManager.CreateInstance(footstepEvent);
        footstepInstance.setParameterByName("pasos reverb", currentReverb);
    }

    void Update()
    {
        if (IsMoving() && characterController.isGrounded)
        {
            if (!isPlaying)
            {
                footstepInstance.start();
                isPlaying = true;
            }
        }
        else if (isPlaying)
        {
            footstepInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            isPlaying = false;
        }

        // Transición suave del reverb usando tiempo
        if (Mathf.Abs(currentReverb - targetReverb) > 0.01f)
        {
            transitionTimer += Time.deltaTime / reverbTransitionTime; // Progreso normalizado entre 0 y 1
            currentReverb = Mathf.Lerp(currentReverb, targetReverb, transitionTimer);
            footstepInstance.setParameterByName("pasos reverb", currentReverb);
        }
        else
        {
            transitionTimer = 0f; // Reinicia el temporizador cuando llega al valor objetivo
        }
    }

    private bool IsMoving()
    {
        return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
    }

    public void SetTargetReverb(float newReverbValue)
    {
        targetReverb = Mathf.Clamp(newReverbValue, 0f, 1f);
        transitionTimer = 0f; // Reinicia la transición cuando se establece un nuevo objetivo
    }

    private void OnDestroy()
    {
        footstepInstance.release();
    }
}
