using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI_Movement : MonoBehaviour
{
    public Transform player;
    public float AttackDistance;
    private NavMeshAgent agent;
    private Animator m_Animator;

    public LayerMask whatIsGround, whatIsPlayer;

    Vector3 WalkPoint;
    bool walkPointSet;
    public float walkPointRange;

    bool alreadyAttack;
    public float timeBetweenAttacks;

    public float sightRange, attackRange;
    bool playerInSightRange, playerInAttackRange;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) Chasing();
        if (playerInSightRange && playerInAttackRange) Attacking();
        //attack animation + follow player
        /*m_Distance = Vector3.Distance(agent.transform.position, player.position);
        
        if (m_Distance < AttackDistance)
        {
            agent.isStopped = true;
            //m_Animator.SetBool("Attack", true);
        }
        else
        {
            agent.isStopped = false;
            //m_Animator.SetBool("Attack", false); 
            agent.destination = player.position;
        }

        //movement animation
        if (agent.velocity.magnitude != 0f)
        {
            //m_Animator.SetBool("Running", true);
        }
        else
        {
            //m_Animator.SetBool("Ruinning", false);
        }
        */
        /*void OnAnimatorMove()
        {
            if (m_Animator.GetBool("Running"))
            {
                agent.speed = (m_Animator.deltaPosition / Time.deltaTime).magnitude;
            }
            if (m_Animator.GetBool("Attack") == false)
            {
                agent.speed = (m_Animator.deltaPosition / Time.deltaTime).magnitude;
            }
        } */
        void Patroling()
        {
            if(!walkPointSet)
            {
                float randomZ = Random.Range(-walkPointRange, walkPointRange);
                float randomX = Random.Range(-walkPointRange, walkPointRange);

                WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

                if (Physics.Raycast(WalkPoint, -transform.up, 2f, whatIsGround)) walkPointSet = true;
            }
            if (walkPointSet) agent.SetDestination(WalkPoint);

            Vector3 distanecToWalkPoint = transform.position - WalkPoint;

            if (distanecToWalkPoint.magnitude < 1f) walkPointSet = false;


        }
        void Chasing()
        {
            agent.SetDestination(player.position);
        }
        void Attacking()
        {
            agent.SetDestination(transform.position);

            transform.LookAt(player);
        }

    }
}
