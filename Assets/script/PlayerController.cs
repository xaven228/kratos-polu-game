using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Movement Settings
    [Header("Movement Settings")]
    public float speed = 5f;
    public float rotationSpeed = 5f;
    private const float GRAVITY = 20f;
    public float jumpForce = 8f;
    private const float MIN_JUMP_FORCE = 4f;
    public float climbSpeed = 3f;
    private float currentJumpForce;

    private float jumpInputBuffer = 0.1f;
    private float jumpInputTimer = 0f;
    #endregion

    #region Camera Settings
    [Header("Camera Settings")]
    public Vector3 cameraOffset = new Vector3(0, 5, -10);
    public float minCameraDistance = 2f;
    public float maxCameraDistance = 10f;
    public float cameraSmoothSpeed = 10f;
    #endregion

    #region Eye Settings
    [Header("Eye Settings")]
    public Transform eyes; // GameObject для глаз
    #endregion

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private Transform cameraTransform;
    private float currentCameraDistance;
    private float verticalRotation = 0f;
    private Transform cameraPivot;
    private bool isGrounded;
    private bool isClimbing = false;
    private float coyoteTime = 0.2f;
    private float coyoteTimer = 0f;
    private Vector3 thirdPersonCameraPosition;
    private float targetRotation = 0f; // Целевой угол поворота персонажа
    private float currentRotation = 0f; // Текущий угол поворота персонажа

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        cameraPivot = new GameObject("CameraPivot").transform;
        cameraPivot.position = transform.position + Vector3.up * (controller.height / 2);
        currentCameraDistance = cameraOffset.magnitude;
        Cursor.lockState = CursorLockMode.Locked;

        thirdPersonCameraPosition = cameraOffset;

        if (eyes == null)
        {
            Debug.LogError("Eyes GameObject не назначен в инспекторе!");
        }
    }

    void Update()
    {
        MovePlayer();
        HandleCamera();
        RotatePlayer(); // Плавный поворот персонажа
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = cameraPivot.forward * vertical + cameraPivot.right * horizontal;
        direction.y = 0f;

        if (direction.magnitude > 0.1f)
        {
            direction.Normalize();
        }

        moveDirection.x = direction.x * speed;
        moveDirection.z = direction.z * speed;

        if (isClimbing)
        {
            float climbInput = Input.GetAxis("Vertical");
            moveDirection.y = climbInput * climbSpeed;
        }
        else
        {
            CheckGroundStatus();

            jumpInputTimer -= Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                jumpInputTimer = jumpInputBuffer;
            }

            if (coyoteTimer > 0 && jumpInputTimer > 0)
            {
                currentJumpForce = jumpForce;
                moveDirection.y = currentJumpForce;
                coyoteTimer = 0f;
                jumpInputTimer = 0f;
            }
            else
            {
                currentJumpForce = MIN_JUMP_FORCE;
            }
            if (Input.GetButtonUp("Jump") && moveDirection.y > MIN_JUMP_FORCE)
            {
                moveDirection.y = MIN_JUMP_FORCE;
            }

            if (!isGrounded)
            {
                moveDirection.y -= GRAVITY * Time.deltaTime;
            }
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    private void HandleCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        cameraPivot.position = transform.position + Vector3.up * (controller.height / 2);
        cameraPivot.Rotate(Vector3.up * mouseX, Space.World);
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraPivot.localRotation = Quaternion.Euler(verticalRotation, cameraPivot.localEulerAngles.y, 0f);

        // Поворот тела персонажа
        transform.rotation = Quaternion.Euler(0, cameraPivot.eulerAngles.y, 0);

        Vector3 desiredPosition = cameraPivot.position - cameraPivot.forward * currentCameraDistance + Vector3.up * cameraOffset.y;

        RaycastHit hit;
        if (Physics.Linecast(cameraPivot.position, desiredPosition, out hit))
        {
            desiredPosition = hit.point + cameraPivot.forward * 0.1f;
        }

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredPosition, cameraSmoothSpeed * Time.deltaTime);
        cameraTransform.LookAt(cameraPivot.position);

        // Поворот глаз
        if (eyes != null)
        {
            eyes.rotation = cameraTransform.rotation; // Устанавливаем вращение глаз равным вращению камеры
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentCameraDistance = Mathf.Clamp(currentCameraDistance - scroll, minCameraDistance, maxCameraDistance);
    }

    private void RotatePlayer()
    {
        // Получаем угол поворота камеры
        float cameraRotation = cameraPivot.eulerAngles.y;

        // Вычисляем кратчайший угол поворота к целевому углу
        float deltaAngle = Mathf.DeltaAngle(currentRotation, cameraRotation);

        // Плавно интерполируем текущий угол поворота к целевому углу
        currentRotation = Mathf.Lerp(currentRotation, currentRotation + deltaAngle, Time.deltaTime * rotationSpeed);

        // Применяем поворот к персонажу
        transform.rotation = Quaternion.Euler(0, currentRotation, 0);
    }

    private void FreezeRotation()
    {
        // Больше не нужно, так как RotatePlayer() делает это
    }

    private void CheckGroundStatus()
    {
        float rayLength = controller.height / 2 + 0.1f;
        isGrounded = Physics.SphereCast(transform.position, controller.radius, Vector3.down, out RaycastHit hit, rayLength);

        if (isGrounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
            moveDirection.y = 0f;
            Vector3 ladderForward = other.transform.forward;
            ladderForward.y = 0f;
            transform.forward = ladderForward.normalized;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }
}