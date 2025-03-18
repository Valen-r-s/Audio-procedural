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
        {
            playerVelocity.y = -2f;
        }

        HandleCameraRotation();

        HandlePlayerMovement();
   
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
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