using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerInput.MainActions input;

    CharacterController controller;
    Animator animator;

    public AudioClip dashSound; // Dash sound effect
    private AudioSource audioSource; // AudioSource for playing sounds

    [Header("Controller")]
    public float moveSpeed = 5;
    public float gravity = -9.8f;
    public float jumpHeight = 1.2f;

    Vector3 _PlayerVelocity;

    bool isGrounded = true;

    [Header("Dash")]
    public float dashDistance = 10f; // Distance covered during dash
    public float dashSpeed = 50f; // Speed of the dash
    public float dashCooldown = 5f; // Cooldown time for dash
    public Image dashCooldownImage; // UI image for cooldown indicator
    private bool canDash = true; // Tracks whether dash is available

    [Header("Camera")]
    public Camera cam;
    public float sensitivity;

    float xRotation = 0f;

    [Header("Experience System")]
    public int xpPerOrb = 10; // Amount of experience gained per orb
    private int currentExperience = 0; // Tracks the player's total experience
    public TextMeshProUGUI xpText; // Reference to the XP TextMeshProUGUI


    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        playerInput = new PlayerInput();
        input = playerInput.Main;
        AssignInputs();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UpdateXPText(); // Initialize XP display
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        weaponSwitching = FindObjectOfType<WeaponSwitching>();

        if (input.Attack.IsPressed())
        {
            Attack();
        }

        if (input.Dash.triggered && canDash)
        {
            StartDash();
        }

        SetAnimations();
        UpdateDashCooldown();
    }

    void FixedUpdate()
    {
        MoveInput(input.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        LookInput(input.Look.ReadValue<Vector2>());
    }

    void MoveInput(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
        if (isGrounded && _PlayerVelocity.y < 0)
        {
            _PlayerVelocity.y = -2f;
        }
        _PlayerVelocity.y += gravity * Time.deltaTime;

        controller.Move(_PlayerVelocity * Time.deltaTime);
    }

    void LookInput(Vector3 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime * sensitivity);
        xRotation = Mathf.Clamp(xRotation, -80, 80);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime * sensitivity));
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Jump()
    {
        if (isGrounded)
            _PlayerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }

    void AssignInputs()
    {
        input.Jump.performed += ctx => Jump();
        input.Attack.started += ctx => Attack();
        input.Dash.performed += ctx => StartDash(); // Assign the dash input
    }

    // Dash Mechanic
    void StartDash()
    {
        if (!canDash) return;

        PlayDashSound();

        Vector2 moveInput = input.Movement.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        if (moveDirection == Vector3.zero)
        {
            moveDirection = transform.forward;
        }
        else
        {
            moveDirection = transform.TransformDirection(moveDirection).normalized;
        }

        StartCoroutine(Dash(moveDirection));
        canDash = false;
    }

    void PlayDashSound()
    {
        if (dashSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(dashSound);
        }
    }

    IEnumerator Dash(Vector3 direction)
    {
        float dashDuration = dashDistance / dashSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            controller.Move(direction * dashSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(DashCooldown());
    }

    IEnumerator DashCooldown()
    {
        float cooldownTimer = 0f;

        while (cooldownTimer < dashCooldown)
        {
            cooldownTimer += Time.deltaTime;
            UpdateDashCooldown(cooldownTimer / dashCooldown);
            yield return null;
        }

        canDash = true;
        UpdateDashCooldown(1f);
    }

    void UpdateDashCooldown(float fillAmount = 1f)
    {
        if (dashCooldownImage != null)
        {
            dashCooldownImage.fillAmount = fillAmount;
        }
    }

    // Animations
    string currentAnimationState;

    public void ChangeAnimationState(string newState)
    {
        if (currentAnimationState == newState) return;

        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    void SetAnimations()
    {
        if (!attacking)
        {
            ChangeAnimationState(weaponSwitching.IDLE);
        }
    }

    // Attacking Behaviour
    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    public AudioClip swordSwing;
    public AudioClip hitSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;
    public WeaponSwitching weaponSwitching;

    public void Attack()
    {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), weaponSwitching.weaponSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(swordSwing);

        if (attackCount == 0)
        {
            ChangeAnimationState(weaponSwitching.ATTACK1);
            attackCount++;
        }
        else
        {
            ChangeAnimationState(weaponSwitching.ATTACK2);
            attackCount = 0;
        }
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            Debug.Log("Raycast hit: " + hit.transform.name);

            if (hit.transform.TryGetComponent<Actor>(out Actor T))
            {
                T.TakeDamage(weaponSwitching.weaponDamage);
            }

            if (hit.transform.TryGetComponent<Enemy>(out Enemy enemy))
            {
                Debug.Log("Damaging enemy: " + hit.transform.name);
                enemy.TakeDamage(attackDamage);
            }
        }
        else
        {
            Debug.Log("Raycast did not hit any target.");
        }
    }

    public void AddExperience(int amount)
    {
        currentExperience += amount;
        Debug.Log("Experience added: " + amount + ". Total XP: " + currentExperience);
        UpdateXPText(); // Update the displayed XP
    }

    void UpdateXPText()
    {
        if (xpText != null)
        {
            xpText.text = "Score: " + currentExperience;
        }
    }
}
