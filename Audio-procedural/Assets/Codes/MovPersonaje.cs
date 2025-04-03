using UnityEngine;

public class MovPersonaje : MonoBehaviour
{
    public float Sensitivity = 2f;
    public float MoveSpeed = 5f;
    public float gravity = -9.81f;

    private float RotationX = 0;
    private Vector3 playerVelocity;
    private bool isGrounded;

    public Transform Player;
    private CharacterController controller;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = Player.GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f; // Rápido reset a la gravedad, no 0 para evitar problemas al aterrizar

        HandleCameraRotation();
        HandlePlayerMovement();

        // Aplicar gravedad después de la gestión del movimiento
        playerVelocity.y += gravity * Time.deltaTime;

        // Movimiento final aplicado una vez por frame
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void HandleCameraRotation()
    {
        float MouseX = Input.GetAxis("Mouse X") * Sensitivity;
        float MouseY = Input.GetAxis("Mouse Y") * Sensitivity;

        RotationX = Mathf.Clamp(RotationX - MouseY, -90f, 90f);

        transform.localRotation = Quaternion.Euler(RotationX, 0f, 0f);
        Player.Rotate(Vector3.up * MouseX);
    }

    void HandlePlayerMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        if (moveX != 0 || moveZ != 0)
        {
            Vector3 moveDirection = (Player.right * moveX + Player.forward * moveZ).normalized;

            // Solo actualizar las componentes X y Z, manteniendo la Y para la gravedad
            Vector3 horizontalVelocity = moveDirection * MoveSpeed;
            playerVelocity.x = horizontalVelocity.x;
            playerVelocity.z = horizontalVelocity.z;
        }
        else
        {
            playerVelocity.x = 0;
            playerVelocity.z = 0;
        }
    }

}
