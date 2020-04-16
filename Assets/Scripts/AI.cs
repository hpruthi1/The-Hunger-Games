using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class AI : MonoBehaviour
{
    public Animator animator;
    private NavMeshAgent agent;
    public Transform Player;
    public float DistanceLeft;
    public float Speed;
    public bool animationCompleted = true;

    Dictionary<string,float> animations = new Dictionary<string,float>();

    // Start is called before the first frame update
    void Start()
    {
        animator.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        animations.Add("Box",2.333f);
        animations.Add("Uppercut",1.333f);
        //animations.Add("Box");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    private void OnTriggerStay(Collider other)
    {
        transform.LookAt(Player);
        if (Vector3.Distance(gameObject.transform.position, other.transform.position) < DistanceLeft)
        {
            agent.isStopped = true;
            //animator.SetTrigger("Attack");
            if (animationCompleted)
            {
                var index = Random.Range(0, animations.Count);
                animator.SetTrigger(animations.Keys.ToArray()[index]);
                Invoke("onAnimationCompleted", animations.Values.ToArray()[index]);
                animationCompleted = false;
            }
        }
        else
        {
            //animator.ResetTrigger("Attack");
            agent.SetDestination(other.transform.position);
            agent.isStopped = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        agent.isStopped = true;
    }

    void onAnimationCompleted()
    {
        animationCompleted = true;
    }
}
