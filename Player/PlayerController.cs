using GreyWolf;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Input Tweak values")]

    [SerializeField] float reloadTime = .5f;


    [Header("Inputs")]
    [SerializeField] Joystick aimingJoystick;
    [SerializeField] Joystick movementJoystick;
    [SerializeField] Button shootButton;
    [SerializeField] ParticleSystem shootingParticles;
    [SerializeField] ParticleSystem dustParticles;
    [SerializeField] ParticleSystem fireParticles;

    // * Non-tweakable variables
    ServiceLocator _service;

    Rigidbody rb;

    bool isReadyToAttack = true;
    float reloadTimer = 0;

    [SerializeField] float fireTime = 1f;
    float fireTimer = 0f;


    // * Animator 
    [SerializeField] Animator p_Animator;
    bool isRunning = false;
    bool isAiming = false;
    bool isOnFire = false;
    public bool IsOnFire { get { return isOnFire; } }

    enum playerStatus { Movement, Aiming, None };
    playerStatus status;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _service = FindObjectOfType<ServiceLocator>(); 
    }

    private void FixedUpdate() => PlayerMovement();

    public void Update()
    {
        AttackTimer();
        OnFire();
    }


    void OnParticleCollision(GameObject other)
    {
        if (other.transform.parent.tag == "Enemy")
        {
            other.transform.parent.GetComponent<IStats>().Attack();
        }
    }

    // * IStats Interface methods
    public void TakeDamage(float amount) => _service.characterStats.Health = -amount;
    public void SetOnFire() => isOnFire = true;
    public void ResetFireTimer() => fireTimer = 0f;
    // ---------------------------



    // Methods ------------------------

    private void PlayerMovement() //Make player move, aim + make the joystick useable
    {

        Vector2 movement = movementJoystick.Direction;
        Vector2 aiming = aimingJoystick.Direction;


        Vector3 movementDirection = new Vector3(movement.x, 0, movement.y);
        movementDirection.Normalize();

        Vector3 shootingLookRotation = new Vector3(aiming.x, 0, aiming.y);
        shootingLookRotation.Normalize();

        PlayerStatus(shootingLookRotation, movementDirection);

        // Play animation
        p_Animator.SetBool("isRunning", isRunning);
        p_Animator.SetBool("isAiming", isAiming);

        // Dust Particles while running
        var emissionModule = dustParticles.emission;

        switch (status)
        {
            case playerStatus.Movement:
                movementDirection = MovementInput(movementDirection) * _service.characterStats.MovementSpeed * Time.deltaTime;
                rb.velocity = movementDirection;
                emissionModule.enabled = true;
                break;
            case playerStatus.Aiming:
                AimingInput(shootingLookRotation);
                emissionModule.enabled = false;
                break;
            default:
                emissionModule.enabled = false;
                StopPlayerForces();
                break;
        }
    }

    private void PlayerStatus(Vector3 shootingLookRotation, Vector3 movementDirection) // Aiming or in movement
    {
        if (shootingLookRotation.magnitude > 0)
        {
            isAiming = true;
            isRunning = false;

            status = playerStatus.Aiming;
            JoystickUI(
                shootButton,
                movementJoystick,
                aimingJoystick,
                0,
                false,
                true,
                true);
        }
        else if (movementDirection.magnitude > 0)
        {
            isAiming = false;
            isRunning = true;

            status = playerStatus.Movement;
            JoystickUI(
                shootButton,
                movementJoystick,
                aimingJoystick,
                1,
                true,
                false,
                false);
        }
        else
        {
            isAiming = false;
            isRunning = false;

            status = playerStatus.None;
            JoystickUI(
                shootButton,
                movementJoystick,
                aimingJoystick,
                0,
                true,
                true,
                false);
        }
    }

    private void JoystickUI(
        Button shootButton,
        Joystick movement,
        Joystick aiming,
        int handleRange, //To reset joysticks handle
        bool movementJoystick, //// 
        bool aimingJoystick,     // When to show inputs
        bool shootBtn)         //// 
    {
        movement.HandleRange = handleRange;
        movement.gameObject.SetActive(movementJoystick);
        aiming.gameObject.SetActive(aimingJoystick);
        shootButton.gameObject.SetActive(shootBtn);
    }
    private void AimingInput(Vector3 shootingLookRotation)
    {
        Quaternion rotation = Quaternion.LookRotation(shootingLookRotation, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
    }

    private Vector3 MovementInput(Vector3 movementDirection)
    {
        Quaternion rotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);

        return movementDirection;
    }
    private void StopPlayerForces()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void AttackTimer()
    {
        if (!isReadyToAttack)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadTime)
            {
                isReadyToAttack = true;
            }
        }
    }

    private void OnFire()
    {
        var emissionModule = fireParticles.emission;
        emissionModule.enabled = isOnFire;
        if (fireTimer == 0f || fireTimer <= fireTime)
        {
            fireTimer += Time.deltaTime;
        }
        if (fireTimer >= fireTime) isOnFire = false;
    }


    // Public Methods -------------------------
    public void ShootParticle()
    {
        if (isReadyToAttack)
        {
            isReadyToAttack = false;
            reloadTimer = 0;
            shootingParticles.Play();
            p_Animator.SetTrigger("Shoot");
        }
    }

    public void SetArrowCount(int arrowShootingCount)
    {
        var main = shootingParticles.emission;

        main.burstCount = arrowShootingCount;
    } 


}