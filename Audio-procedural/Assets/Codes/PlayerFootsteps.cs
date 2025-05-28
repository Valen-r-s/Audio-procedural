using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class FootstepSound : MonoBehaviour
{
    [Header("FMOD")]
    public GameObject footstepEmitterObject; // Objeto hijo que tiene el StudioEventEmitter
    private StudioEventEmitter emitter; // El componente que reproduce los pasos

    private CharacterController characterController;
    private bool isGrounded;

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

        if (footstepEmitterObject == null)
        {
            Debug.LogError("No se asignó el objeto con el StudioEventEmitter.");
            return;
        }

        emitter = footstepEmitterObject.GetComponent<StudioEventEmitter>();

        if (emitter == null)
        {
            Debug.LogError("El objeto no contiene un StudioEventEmitter.");
            return;
        }

        footstepEmitterObject.SetActive(false); // Asegurarse de que esté apagado al inicio

        // Inicializar reverb global
        RuntimeManager.StudioSystem.setParameterByName("Reverb pasos", currentReverb);
    }

    void Update()
    {
        DetectSurface(); // SIEMPRE verifica el suelo primero

        if (IsMoving() && isGrounded && !PauseMenu.isPaused && PauseMenu.CanPaused)
        {
            if (!footstepEmitterObject.activeSelf)
            {
                footstepEmitterObject.SetActive(true);
            }
        }
        else if (footstepEmitterObject.activeSelf)
        {
            footstepEmitterObject.SetActive(false);
        }

        // Transición suave de reverb
        if (Mathf.Abs(currentReverb - targetReverb) > 0.01f)
        {
            transitionTimer += Time.deltaTime / reverbTransitionTime;
            currentReverb = Mathf.Lerp(currentReverb, targetReverb, transitionTimer);
            RuntimeManager.StudioSystem.setParameterByName("Reverb pasos", currentReverb);
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


        if (Physics.SphereCast(origin, sphereRadius, Vector3.down, out hit, rayLength, materialLayer))
        {
            isGrounded = true;

#if UNITY_EDITOR
            Debug.DrawLine(origin, origin + Vector3.down * rayLength, Color.red);

            // Solo si impacta, dibuja una esfera en el punto de colisión
            if (hit.collider != null)
            {
                DebugDrawSphere(hit.point, sphereRadius, Color.green);
            }
#endif




            string surfaceType = hit.collider.tag;
            float fmodValue = 0f;

            switch (surfaceType)
            {
                case "Concreto": fmodValue = 0f; break;
                case "Madera": fmodValue = 1f; break;
                case "Metal": fmodValue = 2f; break;
            }

            emitter.SetParameter("Pasos", fmodValue);
        }
        else
        {
            isGrounded = false;
        }
    }

    void DebugDrawSphere(Vector3 center, float radius, Color color, int segments = 12)
    {
        float angleStep = 360f / segments;

        // Círculo horizontal
        for (int i = 0; i < segments; i++)
        {
            float angle1 = Mathf.Deg2Rad * i * angleStep;
            float angle2 = Mathf.Deg2Rad * (i + 1) * angleStep;

            Vector3 point1 = center + new Vector3(Mathf.Cos(angle1), 0, Mathf.Sin(angle1)) * radius;
            Vector3 point2 = center + new Vector3(Mathf.Cos(angle2), 0, Mathf.Sin(angle2)) * radius;
            Debug.DrawLine(point1, point2, color);
        }

        // Círculo vertical (X)
        for (int i = 0; i < segments; i++)
        {
            float angle1 = Mathf.Deg2Rad * i * angleStep;
            float angle2 = Mathf.Deg2Rad * (i + 1) * angleStep;

            Vector3 point1 = center + new Vector3(Mathf.Cos(angle1), Mathf.Sin(angle1), 0) * radius;
            Vector3 point2 = center + new Vector3(Mathf.Cos(angle2), Mathf.Sin(angle2), 0) * radius;
            Debug.DrawLine(point1, point2, color);
        }

        // Círculo vertical (Z)
        for (int i = 0; i < segments; i++)
        {
            float angle1 = Mathf.Deg2Rad * i * angleStep;
            float angle2 = Mathf.Deg2Rad * (i + 1) * angleStep;

            Vector3 point1 = center + new Vector3(0, Mathf.Cos(angle1), Mathf.Sin(angle1)) * radius;
            Vector3 point2 = center + new Vector3(0, Mathf.Cos(angle2), Mathf.Sin(angle2)) * radius;
            Debug.DrawLine(point1, point2, color);
        }
    }



}
