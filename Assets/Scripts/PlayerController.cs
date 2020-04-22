using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public bool Practice = false;

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

    private void Start()
    {
        if (Application.loadedLevelName == "Practice")
        {
            Practice = true;
        }
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
                    if (Practice)
                    {
                        animator.SetTrigger("BrutalPunch");
                    }
                    else
                    {
                        CmdBrutalPunch();
                    }
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (Practice)
                    {
                        canAttack = true;
                        animator.SetTrigger("Kick");
                    }
                    else
                    {
                        CmdKick();
                    }
                }

                if (Input.GetKeyDown(KeyCode.T))
                {
                    if (Practice)
                    {
                        animator.SetTrigger("Hook");
                    }
                    else
                    {
                        CmdHook();
                    }
                }

                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    if (Practice)
                    {
                        StartCoroutine(ChangeCollider());
                        animator.SetTrigger("CanCrouch");
                    }
                    else
                    {
                        CmdCrouch();
                    }
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

    [Command]
    void CmdKick()
    {
        //Kick();
        RpcKick();
    }

    [Command]
    void CmdBrutalPunch()
    {
        //BrutalPunch();
        RpcBrutalPunch();
    }

    [Command]
    void CmdCrouch()
    {
        //Crouch();
        RpcCrouch();
    }

    [Command]
    void CmdHook()
    {
        //Hook();
        RpcHook();
    }

    void Kick()
    {
        canAttack = true;
        animator.SetTrigger("Kick");
    }

    [ClientRpc]
    void RpcKick()
    {
        Kick();
    }

    void BrutalPunch()
    {
        animator.SetTrigger("BrutalPunch");
    }

    [ClientRpc]
    void RpcBrutalPunch()
    {
        BrutalPunch();
    }

    void Crouch()
    {
        StartCoroutine(ChangeCollider());
        animator.SetTrigger("CanCrouch");
    }

    [ClientRpc]
    void RpcCrouch()
    {
        Crouch();
    }

    void Hook()
    {
        animator.SetTrigger("Hook");
    }

    [ClientRpc]
    void RpcHook()
    {
        Hook();
    }
}

