using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIInterface : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;
    Movement movement;
    Animator bTree;
    Damagable health;
    private  bool targetPlayer;
    [HideInInspector]
    public GameObject player;
    public bool movingTowards;
    public int atacks;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bTree = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        movement = GetComponent<Movement>();
        health = GetComponent<Damagable>();
        agent.updatePosition = false;
        atacks = 0;

        movement.lookAt = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(agent != null && movement != null)
        {
            atacks %= 3;
            movement.moveVec = Vector3.zero;
            if (targetPlayer)
            {
                movement.moveVec = agent.desiredVelocity.normalized;
                agent.SetDestination(player.transform.position);
                agent.nextPosition = transform.position;
            }
            else if (movingTowards)
            {
                movement.moveVec = agent.desiredVelocity.normalized;
                agent.nextPosition = transform.position;

                if (!agent.pathPending && agent.remainingDistance < 0.1f)
                {
                    movingTowards = false;
                }
            }
            //Updates
            PlayerDistance();
            Health();
        }
    }
    public void TargetPlayer()
    {
        if (agent != null && movement != null)
        {
            movingTowards = false;
            targetPlayer = true;
        }
    }
    public void UnTarget()
    {
        targetPlayer = false;
    }
    public void MoveTo(Vector3 destination)
    {
        if (agent != null && movement != null)
        {
            agent.nextPosition = transform.position;
            targetPlayer = false;
            agent.SetDestination(destination);
            movingTowards = true;
        }
    }
    public void Attack()
    {
        if (agent != null && movement != null)
        {
            movement.lightAttack = true;
            atacks += 1;
            bTree.SetInteger("Attacks", atacks);
        }
    }
    public void Jump()
    {
        if (agent != null && movement != null)
        {
            movement.jump = true;
        }
    }
    public void LongJump()
    {
        if (agent != null && movement != null)
        {
            movement.jump = true;
            movement.longJump = true;
        }
    }
    public void Dash()
    {
        if (agent != null && movement != null)
        {
            movement.jump = true;
            movement.dash = true;
        }
    }
    public void RotateTowards(Transform target)
    {
        if (agent != null && movement != null)
        {
            movement.lookAt = target;
            Invoke("RotateEnd", 0.1f);
        }
    }
    private void RotateEnd()
    {
        if (agent != null && movement != null)
        {
            movement.lookAt = null;
        }
    }
    //update
    private void PlayerDistance()
    {
        bTree.SetFloat("PlayerDistance", Vector3.Distance(player.transform.position, transform.position));
    }
    private void Health()
    {
        if (health != null)
        {
            bTree.SetFloat("Hp", health.hp / health.maxHp);
        }
    }
}
