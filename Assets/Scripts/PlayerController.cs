using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using EZCameraShake;

public class PlayerController : NetworkBehaviour
{
    private Vector3 movePosition = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero, moveForward;
    public float Speed;
    public float JumpSpeed = 20.0f;
    private bool canJump = false;
    private bool canCrouch = false;
    public bool isFacingRight;
    private bool canAttack = false;
    private CharacterController characterController;
    private Animator animator;
    public float WalkSpeed = 10.0f;
    public bool IsPlayerDead = false;
    public HealthSystem healthSystem;
    public Image PlayerHealthimage;
    public bool LocalplayerEnabled = false;

    private void Awake()
    {
        if (LocalplayerEnabled)
        {
            Invoke("PlayerActive", .1f);
        }
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        isFacingRight = true;

        PlayerHealthimage = GameObject.FindGameObjectWithTag("Health").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isLocalPlayer || LocalplayerEnabled ) 
        {
            PlayerHealthimage.fillAmount = healthSystem.Health / 100;
            canJump = false;
            float horizontal = Input.GetAxis("Horizontal");
            //float vertical = Input.GetAxis("Vertical");

            if (healthSystem.Health <= 0)
            {
                IsPlayerDead = true;
            }

            if (IsPlayerDead)
            {
                //StartCoroutine(Death());
            }

            animator.SetFloat("Speed", characterController.velocity.magnitude);
            if (characterController.isGrounded)
            {
                moveDirection = new Vector3(0, 0, horizontal);
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= Speed;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    canJump = true;
                    moveDirection.y = JumpSpeed;
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    animator.SetTrigger("BrutalPunch");
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    canAttack = true;
                    animator.SetTrigger("Kick");
                }

                if (Input.GetKeyDown(KeyCode.T))
                {
                    animator.SetTrigger("Hook");
                }

                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    StartCoroutine(ChangeCollider());
                    animator.SetTrigger("CanCrouch");
                }

                if (Input.GetKey(KeyCode.C))
                {
                    animator.SetTrigger("CrouchAttack");
                }
                if (Input.GetKeyUp(KeyCode.C))
                {
                    animator.ResetTrigger("CrouchAttack");
                }
            }

            if (!IsPlayerDead)
            {
                moveDirection.y -= 9.8f * Time.deltaTime;
                characterController.Move(moveDirection * Speed * Time.deltaTime);
            }
            animator.SetBool("Jump", canJump);
        }
    }

    IEnumerator ChangeCollider()
    {
        gameObject.GetComponent<CharacterController>().center = new Vector3(0.15f, 0.59f, 0.42f);
        gameObject.GetComponent<CharacterController>().radius = 0.55f;
        gameObject.GetComponent<CharacterController>().height = 0.07f;
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<CharacterController>().center = new Vector3(0, 0.92f, 0);
        gameObject.GetComponent<CharacterController>().radius = 0.24f;
        gameObject.GetComponent<CharacterController>().height = 1.63f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyFist"))
        {
            gameObject.GetComponent<HealthSystem>().healthDecrease(5);
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            animator.SetTrigger("UppercutHit");
        }
    }

    public void PlayerActive()
    {
        gameObject.SetActive(true);
    }
}

