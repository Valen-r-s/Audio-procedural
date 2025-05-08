using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(CharacterController))]
public class FMODReverbFromTag : MonoBehaviour
{
    public string parameterName = "ReverberacionSuperficie";

    public float rayLength = 1.5f;
    public LayerMask surfaceLayer;

    private CharacterController characterController;
    private float currentValue = 0f;
    private float targetValue = 0f;
    public float transitionTime = 0.2f;
    private float timer = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        RuntimeManager.StudioSystem.setParameterByName(parameterName, currentValue);
    }

    void Update()
    {
        DetectSurface();

        if (Mathf.Abs(currentValue - targetValue) > 0.01f)
        {
            timer += Time.deltaTime / transitionTime;
            currentValue = Mathf.Lerp(currentValue, targetValue, timer);
            RuntimeManager.StudioSystem.setParameterByName(parameterName, currentValue);
        }
        else
        {
            timer = 0f;
        }
    }

    void DetectSurface()
    {
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.down * (characterController.height / 2 - characterController.skinWidth);
        float sphereRadius = 0.2f;

        if (Physics.SphereCast(origin, sphereRadius, Vector3.down, out hit, rayLength, surfaceLayer))
        {
            string tag = hit.collider.tag;

            // Si el tag es "0.0", se considera High Reverb → parámetro = 1.0
            // Si el tag es "1.0", se considera sin reverb → parámetro = 0.0
            if (tag == "0.0")
            {
                SetParameter(1f); // Reverb alta
            }
            else if (tag == "1.0")
            {
                SetParameter(0f); // Sin reverb
            }

            Debug.Log($"Piso con tag '{tag}' detectado. Parámetro = {targetValue}");
        }
    }

    void SetParameter(float value)
    {
        targetValue = value;
        timer = 0f;
    }
}
