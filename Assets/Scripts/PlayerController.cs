using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerController : MonoBehaviour
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

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        canJump = false;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Flip(horizontal);
        animator.SetFloat("Speed", characterController.velocity.magnitude);
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(-vertical, 0, horizontal);
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
                //FindObjectOfType<AI>().GetComponent<Animator>().SetTrigger("Death");
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

            if(Input.GetKey(KeyCode.C))
            {
                animator.SetTrigger("CrouchAttack");
            }
            if (Input.GetKeyUp(KeyCode.C))
            {
                animator.ResetTrigger("CrouchAttack");
            }
        }
            moveDirection.y -= 9.8f * Time.deltaTime;
            characterController.Move(moveDirection * Speed * Time.deltaTime);
            animator.SetBool("Jump", canJump);
    }

   /* private void Flip(float horizontal)
    {
        if (horizontal > 0 && !isFacingRight || horizontal < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 Scale = transform.localScale;
            Scale.z *= -1;
            transform.localScale = Scale;
        }
        */

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
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            animator.SetTrigger("UppercutHit");
        }
    }
}

