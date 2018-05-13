using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : IController
{
    public float lookRadius = 5;

    public Transform target;
    NavMeshAgent agent;
    Animator _animator;

    bool followEnemy = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null)
        {
            if (followEnemy)
            {
                agent.isStopped = false;
                agent.SetDestination(target.position);
                _animator.SetBool("isWalking", true);
                float distance = Vector3.Distance(target.position, transform.position);
                if (distance <= agent.stoppingDistance)
                {
                    _animator.SetBool("isWalking", false);
                    _animator.SetBool("isAttacking", true);
                    LookAtTarget();
                }
                else if (distance > lookRadius)
                {
                    _animator.SetBool("isAttacking", false);
                    followEnemy = false;
                }
            }
            else
            {
                agent.isStopped = true;
                _animator.SetBool("isWalking", false);
            }
        }
    }

    public void StopAttacking()
    {
        followEnemy = false;
        if (_animator.GetBool("isAttacking"))
        {
            _animator.SetBool("isAttacking", false);
        }
    }

    IEnumerator KILL()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
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
