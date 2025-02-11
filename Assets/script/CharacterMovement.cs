using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform headTransform;
    public Transform bodyTransform;
    public CharacterController controller;
    
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 2f;
    public float bodyTurnSpeed = 2f; // Скорость поворота тела медленнее головы

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    [Header("Camera Limits")]
    public float maxVerticalAngle = 60f; // Ограничен угол наклона камеры
    public float minVerticalAngle = -60f;
    public float maxHorizontalAngle = 90f;
    
    [Header("Debug")]
    public bool isGrounded;

    private Vector3 velocity;
    private float verticalRotation = 0f;

    void Update()
    {
        HandleGroundCheck();
        HandleMovement();
        HandleJump();
        HandleCameraRotation();
    }

void HandleGroundCheck()
{
    // Используем несколько точек проверки
    Vector3[] checkPoints = new Vector3[]
    {
        groundCheck.position,
        groundCheck.position + new Vector3(0.5f, 0, 0),
        groundCheck.position + new Vector3(-0.5f, 0, 0),
        groundCheck.position + new Vector3(0, 0, 0.5f),
        groundCheck.position + new Vector3(0, 0, -0.5f)
    };

    isGrounded = false;
    foreach (var point in checkPoints)
    {
        if (Physics.CheckSphere(point, groundDistance, groundMask))
        {
            isGrounded = true;
            break;
        }
    }
}

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        
        Vector3 moveDirection = cameraTransform.forward * moveZ + cameraTransform.right * moveX;
        moveDirection.y = 0;
        controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        
        velocity.y += gravity * Time.deltaTime;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        headTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        
        transform.Rotate(Vector3.up * mouseX);
        
        float bodyTargetRotation = cameraTransform.eulerAngles.y;
        bodyTransform.rotation = Quaternion.Lerp(bodyTransform.rotation, Quaternion.Euler(0f, bodyTargetRotation, 0f), Time.deltaTime * bodyTurnSpeed);
    }
}
