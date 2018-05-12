using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
   public float lookRadius = 4;
    Transform target;
    NavMeshAgent agent;
    Animator _animator;


    void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

    }

    void Update () {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= lookRadius)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            _animator.SetBool("isWalking", true);

            if (distance <= agent.stoppingDistance)
            {
                _animator.SetBool("isWalking", false);
                _animator.SetBool("isAttacking", true);
                LookAtTarget();
            }
            else
            {
                _animator.SetBool("isAttacking", false);
            }
        }
        else {
            agent.isStopped=true;
            _animator.SetBool("isWalking", false);
        }
    }

    void LookAtTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 100);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
