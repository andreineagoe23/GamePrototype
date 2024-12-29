using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerInput.MainActions input;

    CharacterController controller;
    Animator animator;

    public AudioClip dashSound;           // Dash sound effect
    private AudioSource audioSource;      // AudioSource for playing sounds


    [Header("Controller")]
    public float moveSpeed = 5;
    public float gravity = -9.8f;
    public float jumpHeight = 1.2f;

    Vector3 _PlayerVelocity;

    bool isGrounded = true;

    [Header("Dash")]
    public float dashDistance = 10f;       // Distance covered during dash
    public float dashSpeed = 50f;         // Speed of the dash
    public float dashCooldown = 5f;       // Cooldown time for dash
    public Image dashCooldownImage;       // UI image for cooldown indicator
    private bool canDash = true;          // Tracks whether dash is available

    [Header("Camera")]
    public Camera cam;
    public float sensitivity;

    float xRotation = 0f;

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
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        weaponSwitching = FindObjectOfType<WeaponSwitching>();

        // Repeat Inputs
        if (input.Attack.IsPressed())
        { Attack(); }

        if (input.Dash.triggered && canDash)
        { StartDash(); }

        SetAnimations();
<<<<<<< HEAD
        UpdateDashCooldown();
=======

        // // Handle the slash effect
        // if (attacking == true)
        // {
        //     hitEffect.SetActive(true);
        // }
        // else
        // {
        //     hitEffect.SetActive(false);
        // }

        // Reset the attacking flag
        // attacking = false;
>>>>>>> origin/EnemySpawns
    }

    void FixedUpdate()
    { MoveInput(input.Movement.ReadValue<Vector2>()); }

    void LateUpdate()
    { LookInput(input.Look.ReadValue<Vector2>()); }

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
    { input.Enable(); }

    void OnDisable()
    { input.Disable(); }

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

    // ---------- //
    // DASH MECHANIC //
    // ---------- //

    void StartDash()
    {
        if (!canDash) return;

        PlayDashSound();

        // Get the movement direction based on input
        Vector2 moveInput = input.Movement.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

        // If there's no movement input, dash in the forward direction
        if (moveDirection == Vector3.zero)
        {
            moveDirection = transform.forward;
        }
        else
        {
            // Adjust to world space direction
            moveDirection = transform.TransformDirection(moveDirection).normalized;
        }

        // Perform the dash
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
            // Move the player in the dash direction
            controller.Move(direction * dashSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Trigger cooldown
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

        // Dash is ready again
        canDash = true;
        UpdateDashCooldown(1f); // Fully refill the cooldown bar
    }

    void UpdateDashCooldown(float fillAmount = 1f)
    {
        if (dashCooldownImage != null)
        {
            dashCooldownImage.fillAmount = fillAmount;
        }
    }

    // ---------- //
    // ANIMATIONS //
    // ---------- //


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

    // ------------------- //
    // ATTACKING BEHAVIOUR //
    // ------------------- //

    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    public GameObject hitEffect;
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
            HitTarget(hit.point);

<<<<<<< HEAD
            if (hit.transform.TryGetComponent<Actor>(out Actor T))
            {
                T.TakeDamage(weaponSwitching.weaponDamage);
            }
=======
            if (hit.transform.TryGetComponent<Enemy>(out Enemy T))
            { T.TakeDamage(attackDamage); }
>>>>>>> origin/EnemySpawns
        }

    }

    void HitTarget(Vector3 pos)
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(hitSound);

        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);
    }
}
