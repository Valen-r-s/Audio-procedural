using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[RequireComponent(typeof(CharacterController))]
public class FootstepSound : MonoBehaviour
{
    public EventReference footstepEvent;
    private EventInstance footstepInstance;
    private CharacterController characterController;
    private bool isPlaying;

    [Header("Raycast Settings")]
    public float rayLength = 1.5f;
    public LayerMask materialLayer; 

    [Header("Reverb Settings")]
    public float reverbTransitionTime = 0.2f;
    private float currentReverb = 0f;
    private float targetReverb = 0f;
    private float transitionTimer = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        footstepInstance = RuntimeManager.CreateInstance(footstepEvent);
        RuntimeManager.StudioSystem.setParameterByName("Pasos reverb global reverb", currentReverb);
    }

    void Update()
    {
        if (IsMoving() && characterController.isGrounded)
        {
            DetectSurface();
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

        // Transición suave del reverb
        if (Mathf.Abs(currentReverb - targetReverb) > 0.01f)
        {
            transitionTimer += Time.deltaTime / reverbTransitionTime;
            currentReverb = Mathf.Lerp(currentReverb, targetReverb, transitionTimer);
            RuntimeManager.StudioSystem.setParameterByName("Pasos reverb global reverb", currentReverb);
        }
        else
        {
            transitionTimer = 0f;
        }
    }

    private bool IsMoving()
    {
        return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
    }

    public void SetTargetReverb(float newReverbValue)
    {
        targetReverb = Mathf.Clamp(newReverbValue, 0f, 1f);
        transitionTimer = 0f;
    }

    void DetectSurface()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.down * (characterController.height / 2 - characterController.skinWidth);
        float sphereRadius = 0.2f;

        Debug.DrawRay(origin, Vector3.down * rayLength, Color.red);

        if (Physics.SphereCast(origin, sphereRadius, Vector3.down, out hit, rayLength, materialLayer))
        {
            Debug.DrawLine(origin, hit.point, Color.green);
            Debug.DrawRay(hit.point, hit.normal * 0.3f, Color.yellow);

            string surfaceType = hit.collider.tag;
            float fmodValue = 0f;

            Debug.Log("Colisionando con " + surfaceType);

            switch (surfaceType)
            {
                case "Concreto":
                    fmodValue = 0f;
                    break;
                case "Madera":
                    fmodValue = 1f;
                    break;
                case "Metal":
                    fmodValue = 2f;
                    break;
            }

            footstepInstance.setParameterByName("Pasos", fmodValue);
        }
    }

    private void OnDestroy()
    {
        footstepInstance.release();
    }
}
