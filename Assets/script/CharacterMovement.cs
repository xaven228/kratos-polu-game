using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform headTransform;
    public Transform bodyTransform;
    public CharacterController controller;
    public Animation characterAnimation;
    
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
    private float moveMagnitude;

    void Start()
    {
        characterAnimation = GetComponent<Animation>();
        // Скрываем курсор при старте игры
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleGroundCheck();
        HandleMovement();
        HandleJump();
        HandleCameraRotation();
        HandleAnimations();
    }

    void HandleGroundCheck()
    {
        isGrounded = false;
        // Луч вниз от позиции проверки земли
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, groundDistance + 0.1f, groundMask))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            if (slopeAngle <= 45f) // Учитываем только плоские поверхности
            {
                isGrounded = true;
            }
        }
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = cameraTransform.forward * moveZ + cameraTransform.right * moveX;
        moveDirection.y = 0;
        moveMagnitude = moveDirection.magnitude;
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
        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
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
        // Поворот всего объекта (включая тело)
        transform.Rotate(Vector3.up * mouseX);
        // Поворот тела с задержкой
        float bodyTargetRotation = cameraTransform.eulerAngles.y;
        bodyTransform.rotation = Quaternion.Lerp(bodyTransform.rotation, Quaternion.Euler(0f, bodyTargetRotation, 0f), Time.deltaTime * bodyTurnSpeed);
    }

    void HandleAnimations()
    {
        if (moveMagnitude > 0)
        {
            if (!characterAnimation.IsPlaying("walk-1"))
                characterAnimation.Play("walk-1");
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}