using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5.0f;
    public float sprintSpeed = 8.0f;
    public float rotationSpeed = 10.0f;
    public float jumpHeight = 1.2f;
    public float gravity = -9.81f;

    [Header("Stamina Settings")]
    public float maxStamina = 50f;
    public float staminaDrainAmount = 20f;
    public float normalRegenRate = 15f;
    public float exhaustedRegenRate = 30f;
    public float exhaustedDelay = 0.02f;
    private float currentStamina;
    private float timeSinceExhausted;
    private bool isExhausted;

    [Header("Camera")]
    public Transform cameraRoot;
    public float cameraPitch = 0.0f;
    public float lookSensitivity = 1.0f;
    public float upperLookLimit = 70f;
    public float lowerLookLimit = -62f;

    [Header("UI")]
    public Image staminaBar;

    private CharacterController _controller;
    private StarterAssetsInputs _input;
    private Vector3 _velocity;
    private bool _grounded;
    private bool _wasGroundedLastFrame;

    private const float _terminalVelocity = 53.0f;
    private float _verticalVelocity;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<StarterAssetsInputs>();
        currentStamina = maxStamina;

        // Find stamina bar if not assigned
        if (staminaBar == null)
        {
            staminaBar = GameObject.Find("StaminaBar").GetComponent<Image>();
        }
    }

    private void Update()
    {
        GroundCheck();
        HandleGravityAndJump();
        HandleStamina();
        Move();
        TriggerPlayerEvents();
        UpdateStaminaUI();
    }

    private void LateUpdate()
    {
        Look();
    }

    private void GroundCheck()
    {
        _grounded = _controller.isGrounded;

        if (_grounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = -2f;
        }
    }

    private void HandleGravityAndJump()
    {
        if (_grounded && _input.jumpPressed)
        {
            _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            PlayerEvents.TriggerJump();
        }

        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }
    }

    private void HandleStamina()
    {
        // Handle sprinting stamina drain
        if (_input.sprint && _input.move.magnitude > 0.1f && _grounded && !isExhausted)
        {
            if (currentStamina > 0)
            {
                currentStamina -= staminaDrainAmount * Time.deltaTime;

                // Prevent stamina from going below 0
                if (currentStamina <= 0)
                {
                    currentStamina = 0;
                    isExhausted = true;
                    timeSinceExhausted = 0f;
                }
            }
        }
        // Handle stamina regeneration
        else
        {
            if (isExhausted)
            {
                timeSinceExhausted += Time.deltaTime;
                if (timeSinceExhausted >= exhaustedDelay)
                {
                    isExhausted = false;
                }
            }
            else
            {
                float regenRate = (currentStamina <= 0) ? exhaustedRegenRate : normalRegenRate;
                currentStamina += regenRate * Time.deltaTime;

                // Prevent stamina from exceeding max
                if (currentStamina > maxStamina)
                {
                    currentStamina = maxStamina;
                }
            }
        }
    }

    private void Move()
    {
        Vector3 inputDirection = new Vector3(_input.move.x, 0, _input.move.y).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            Vector3 moveDirection = transform.right * inputDirection.x + transform.forward * inputDirection.z;
            bool canSprint = _input.sprint && currentStamina > 0f && !isExhausted;
            float speed = canSprint ? sprintSpeed : moveSpeed;
            _controller.Move(moveDirection * speed * Time.deltaTime);
        }

        _controller.Move(new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);
    }

    private void Look()
    {
        if (_input.look.sqrMagnitude >= 0.01f)
        {
            float mouseX = _input.look.x * lookSensitivity;
            float mouseY = _input.look.y * lookSensitivity;

            cameraPitch -= mouseY;
            cameraPitch = Mathf.Clamp(cameraPitch, lowerLookLimit, upperLookLimit);
            cameraRoot.localRotation = Quaternion.Euler(cameraPitch, 0.0f, 0.0f);

            transform.Rotate(Vector3.up * mouseX);
        }
    }

    private void TriggerPlayerEvents()
    {
        bool isSprinting = _input.sprint && currentStamina > 0f && !isExhausted;
        PlayerEvents.TriggerMovement(_input.move, isSprinting, _grounded);

        if (_grounded && !_wasGroundedLastFrame)
        {
            PlayerEvents.TriggerLand();
        }
        _wasGroundedLastFrame = _grounded;
    }

    private void UpdateStaminaUI()
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = GetStaminaPercentage();
        }
    }

    public float GetStaminaPercentage()
    {
        return currentStamina / maxStamina;
    }
}