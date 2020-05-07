using UnityEngine;
using EZCameraShake;

public class Uppercut : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("UppercutHit");
            other.gameObject.GetComponent<HealthSystem>().healthDecrease(5);
            CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);

        }
    }
}
