using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class AI : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    //public Transform Player;
    public float DistanceLeft;
    public float Speed;
    public bool animationCompleted = true;
    bool playerDead;

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
        playerDead = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().IsPlayerDead;
    }

    private void OnTriggerStay(Collider other)
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);

        if (!playerDead)
        {
            if (Vector3.Distance(gameObject.transform.position, other.transform.position) < DistanceLeft)
            {
                agent.isStopped = true;
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
                agent.SetDestination(other.transform.position);
                agent.isStopped = false;
            }
        }

        if (playerDead)
        {
            agent.isStopped = true;
            agent.ResetPath();
            animator.SetFloat("Speed", 0f);
            animator.ResetTrigger("Box");
            animator.ResetTrigger("Uppercut");
            FindObjectOfType<CountDown>().timerIsActive = false;
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
