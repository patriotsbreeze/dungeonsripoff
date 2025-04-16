using UnityEngine;
using UnityEngine.AI;

public class AI_NavigationScript : MonoBehaviour
{
    public Transform player;
    public float AttackDistance;
    private NavMeshAgent agent;
    private Animator m_Animator;
    private float m_Distance;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
    }

    void Update()
    {
        //attack animation + follow player
        m_Distance = Vector3.Distance(agent.transform.position, player.position);
        if (m_Distance < AttackDistance)
        {
            agent.isStopped = true;
            m_Animator.SetBool("Attack",true);
        }
        else
        {
            agent.isStopped = false;
            m_Animator.SetBool("Running",false);
            agent.destination = player.position;
        }

        //movement animation
        if (agent.velocity.magnitude != 0f)
        {
            m_Animator.SetBool("Running",true);
        }
        else
        {
            m_Animator.SetBool("Running",false);
        }

        void OnAnimatorMove()
        {
            if (m_Animator.GetBool("Running"))
            {
                agent.speed = (m_Animator.deltaPosition / Time.deltaTime).magnitude;
            }
            if (m_Animator.GetBool("Attack") == false)
                {
                    agent.speed = (m_Animator.deltaPosition / Time.deltaTime).magnitude;
                }
        }

    }
}
