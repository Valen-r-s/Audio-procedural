using UnityEngine;
using FMODUnity;

public class PlayerFootsteps : MonoBehaviour
{
    private StudioEventEmitter emitter;
    private Vector3 lastPosition;
    private CharacterController characterController;

    void Start()
    {
        emitter = GetComponent<StudioEventEmitter>();
        characterController = GetComponent<CharacterController>();

        lastPosition = transform.position;
    }

    void Update()
    {
        bool isMoving = transform.position != lastPosition;

        if (isMoving)
        {
            if (!emitter.IsPlaying())
            {
                emitter.Play();
            }
        }
        else
        {
            if (emitter.IsPlaying())
            {
                emitter.Stop();
            }
        }

        lastPosition = transform.position;
    }
}
