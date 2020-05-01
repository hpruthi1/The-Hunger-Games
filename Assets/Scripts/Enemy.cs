using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public Image EnemyHealthimage;
    public HealthSystem healthSystem;

    // Update is called once per frame
    void Update()
    {
        EnemyHealthimage.fillAmount = healthSystem.Health / 100;
        healthSystem.Health = Mathf.Clamp(healthSystem.Health, 0, 100);

        if(healthSystem.Health <= 0)
        {
            FindObjectOfType<CountDown>().timerIsActive = false;
            GameObject.FindGameObjectWithTag("EnemyFist").GetComponent<BoxCollider>().enabled = false;
            //Destroy(gameObject);
            animator.ResetTrigger("Uppercut");
            animator.ResetTrigger("Box");
            animator.ResetTrigger("Hit");
            //animator.SetFloat("Speed", 0f);
            FindObjectOfType<AI>().agent.isStopped = true;
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finger"))
        {
            gameObject.GetComponent<HealthSystem>().healthDecrease(10);
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            Debug.Log("Damage");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Leg"))
        {
            Vector3 pos = GameObject.FindGameObjectWithTag("Bot").transform.position;
            Debug.Log(pos.x);
            pos.x +=1.7f;
            GameObject.FindGameObjectWithTag("Bot").transform.position = pos;
            GameObject.FindGameObjectWithTag("Bot").GetComponent<AI>().agent.isStopped = true;
            GameObject.FindGameObjectWithTag("Bot").GetComponent<AI>().agent.ResetPath();

            gameObject.GetComponent<HealthSystem>().healthDecrease(20);
            animator.SetTrigger("Hit");
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        }
    }
}
