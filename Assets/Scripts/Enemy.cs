using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finger"))
        {
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            Debug.Log("Damage");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Leg"))
        {
            Debug.Log("Leg Damage");
            animator.SetTrigger("Hit");
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
        }
    }
}
