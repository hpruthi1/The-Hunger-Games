using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using EZCameraShake;

public class PlayerController : NetworkBehaviour
{
    private Vector3 moveDirection = Vector3.zero;
    public float Speed;
    public float JumpSpeed = 20.0f;
    public GameObject HitEffect;
    public GameObject EnergyBoostEffect;
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
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        isFacingRight = true;
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

            if (healthSystem.Health <= 0)
            {
                IsPlayerDead = true;
                if (Practice)
                {
                    FindObjectOfType<PlayerSpawner>().Panel.SetActive(true);
                }

                else
                {
                    SceneManager.LoadScene("End");
                }
            }

            if (IsPlayerDead)
            {
                FindObjectOfType<CountDown>().timerIsActive = false;
                animator.SetTrigger("Die");
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
                        BrutalPunch();
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
                        Kick();
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
                        Hook();
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
                        Crouch();
                    }
                    else
                    {
                        CmdCrouch();
                    }
                }

                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (!IsPlayerDead)
                    {
                        if (gameObject.GetComponent<HealthSystem>().Health <= 50f)
                        {
                            if (Practice)
                            {
                                OnHealthBoost();
                                gameObject.GetComponent<HealthSystem>().healthincrease(30);
                            }
                        }
                        else
                        {
                            CmdOnHealthBoost();
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    animator.SetTrigger("Fire");
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
        gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
        gameObject.GetComponent<CharacterController>().center = new Vector3(0.15f, 0.59f, 0.42f);
        gameObject.GetComponent<CharacterController>().radius = 0.55f;
        gameObject.GetComponent<CharacterController>().height = 0.07f;
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponentInChildren<BoxCollider>().enabled = true;
        gameObject.GetComponent<CharacterController>().center = new Vector3(0, 0.92f, 0);
        gameObject.GetComponent<CharacterController>().radius = 0.24f;
        gameObject.GetComponent<CharacterController>().height = 1.63f;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        if (other.gameObject.CompareTag("Finger"))
        {
            gameObject.GetComponent<HealthSystem>().healthDecrease(5);
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            if (!Practice)
            {
                animator.SetTrigger("UppercutHit");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.CompareTag("Leg"))
        {
            gameObject.GetComponent<HealthSystem>().healthDecrease(20);
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            if (!Practice)
            {
                animator.SetTrigger("UppercutHit");
            }
        }
    }

    [Command]
    void CmdWalk()
    {
        RpcWalk();
    }

    [ClientRpc]
    void RpcWalk()
    {
        Walk();
    }

    void Walk()
    {
        animator.SetFloat("Speed", characterController.velocity.magnitude);
    }

    [Command]
    void CmdKick()
    {
        RpcKick();
    }

    [Command]
    void CmdBrutalPunch()
    {
        RpcBrutalPunch();
    }

    [Command]
    void CmdCrouch()
    {
        RpcCrouch();
    }

    [Command]
    void CmdHook()
    {
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
        if (Practice)
        {
            Instantiate(HitEffect, transform.position, Quaternion.identity);
        }
        else
        {
            HitEffect = FindObjectOfType<NetworkManager>().spawnPrefabs[3];
            Instantiate(HitEffect, transform.position, Quaternion.identity);
        }
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

    void OnHealthBoost()
    {
        if (Practice)
        {
            Instantiate(EnergyBoostEffect, transform.position, Quaternion.identity);
        }
        else
        {
            EnergyBoostEffect = FindObjectOfType<NetworkManager>().spawnPrefabs[4];
            Instantiate(EnergyBoostEffect, transform.position, Quaternion.identity);
        }
    }

    [ClientRpc]
    void RpcOnHealthBoost()
    {
        OnHealthBoost();
    }

    [Command]
    void CmdOnHealthBoost()
    {
        RpcOnHealthBoost();
    }
}

