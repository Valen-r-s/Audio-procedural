using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPersonaje : MonoBehaviour
{
    public float Sensitivity = 2f; // Sensibilidad ajustable desde el Inspector
    public float MoveSpeed = 5f; // Velocidad de movimiento
    private float RotationX = 0;
    public Transform Player; // Referencia al jugador
    private CharacterController controller; // CharacterController para el movimiento

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = Player.GetComponent<CharacterController>(); // Obtener el CharacterController
    }

    void Update()
    {
        HandleCameraRotation();
        HandlePlayerMovement();
    }

    void HandleCameraRotation()
    {
        float MouseX = Input.GetAxis("Mouse X") * Sensitivity;
        float MouseY = Input.GetAxis("Mouse Y") * Sensitivity;

        RotationX -= MouseY;
        RotationX = Mathf.Clamp(RotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(RotationX, 0f, 0f);
        Player.Rotate(Vector3.up * MouseX);
    }

    void HandlePlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = (Player.right * moveX + Player.forward * moveZ).normalized;
        controller.Move(moveDirection * MoveSpeed * Time.deltaTime);
    }
}
