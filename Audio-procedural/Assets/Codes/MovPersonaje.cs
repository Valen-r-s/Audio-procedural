using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPersonaje : MonoBehaviour
{
    public float Velocity = 100f; // Mouse look sensitivity
    public float MoveSpeed = 5f; // Movement speed
    float RotationX = 0;

    public Transform Player; // Reference to the player object

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Handle camera rotation
        HandleCameraRotation();

        // Handle player movement
        HandlePlayerMovement();
    }

    void HandleCameraRotation()
    {
        float MouseX = Input.GetAxis("Mouse X") * Velocity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * Velocity * Time.deltaTime;

        RotationX -= MouseY;
        RotationX = Mathf.Clamp(RotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(RotationX, 0f, 0f);
        Player.Rotate(Vector3.up * MouseX);
    }

    void HandlePlayerMovement()
    {
        // Get input for movement
        float moveX = Input.GetAxis("Horizontal"); 
        float moveZ = Input.GetAxis("Vertical");  

        // Calculate movement direction relative to the player's rotation
        Vector3 moveDirection = (Player.right * moveX + Player.forward * moveZ).normalized;

        // Move the player
        Player.position += moveDirection * MoveSpeed * Time.deltaTime;
    }
}