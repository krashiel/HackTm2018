using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 4;
    Transform target;
    NavMeshAgent agent;
    Animator _animator;
    Rigidbody _rigidbody;
    bool shouldRotate;


    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            _animator.SetBool("isWalking", true);
            shouldRotate = true;

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
        else
        {
            agent.isStopped = true;
            _animator.SetBool("isWalking", false);
        }

        if (shouldRotate)
        {
            rotateOnTarget();
        }
    }

    void rotateOnTarget()
    {
        var newTargetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        var distance = newTargetPos - transform.position;
        if (distance.magnitude < 0.1f)
        {
            return;
        }
        Quaternion lookOnLook = Quaternion.LookRotation(distance);
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, Time.deltaTime * agent.angularSpeed);
        StartCoroutine(StopRotating());
    }

    IEnumerator StopRotating()
    {
        yield return new WaitForSeconds(0.1f);
        shouldRotate = false;
        StopCoroutine(StopRotating());
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
