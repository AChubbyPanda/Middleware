using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour
{
    NavMeshAgent nav;
    public GameObject player;
    Vector3 anchor;
    Animator anim;
    string state;

    // Use this for initialization
    void Start ()
    {
        nav = GetComponent<NavMeshAgent>();
        anchor = transform.position;
        anim = GetComponent<Animator>();
        state = "Movement";
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (state == "Movement")
        {
            Move();
        }

        if (state == "Attack")
        {

        }
	}

    public float moveSpeed = 6f;

    void Move()
    {
        Vector3 target = player.transform.position;
        nav.stoppingDistance = 2.5f;

        Vector3 velocity = target * moveSpeed * Time.deltaTime;
        //what direction   how far      per second

        // this changes from idel to movement when chasing the player
        float percentSpeed = velocity.magnitude / (moveSpeed * Time.deltaTime);
        anim.SetFloat("movePercent", percentSpeed);

        if (Vector3.Distance(transform.position, target) > 7)
        {
            target = anchor;
            //This allowas the mob to return to its starting point as it would have stopped 2.5 units away due to the stopping distance.
            nav.stoppingDistance = 0;
        }
        else
        {
            ChangeState("Attack");
        }

        anim.SetFloat("movePercent", nav.velocity.magnitude / nav.speed);

        nav.SetDestination(target);
    }

    void ChangeState ( string stateName)
    {
        state = stateName;
        anim.SetTrigger(stateName);
    }

    void ReturnToMovement()
    {
        ChangeState("Movement");
    }
}
