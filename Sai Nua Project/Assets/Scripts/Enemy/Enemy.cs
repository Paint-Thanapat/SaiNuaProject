using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Observer
{
    public GameObject target;

    [HideInInspector] public NavMeshAgent _navMeshAgent;
    [HideInInspector] public Animator _animator;
    [HideInInspector] public Health _health;

    private bool _isAttacking;

    public float attackDamage;

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
    }

    void OnEnable()
    {
        Notify(GameManager.Instance.playerCharacter.GetComponent<PlayerInteractController>());
    }

    public virtual void MoveToTarget()
    {
        if (target)
        {
            if (!_health.isDead)
            {
                if (_navMeshAgent.enabled)
                {
                    float distance = Vector3.Distance(transform.position, target.transform.position);

                    if (distance <= _navMeshAgent.stoppingDistance)
                    {
                        if (!_isAttacking)
                        {
                            _isAttacking = true;
                            _animator.SetTrigger("isAttack");
                        }
                    }
                    else if (!_isAttacking)
                    {
                        _navMeshAgent.SetDestination(target.transform.position);
                    }
                }
            }
        }
    }

    public virtual void ResetAttack()
    {
        _isAttacking = false;
    }

    public override void Notify(Subject subject)
    {
        if (!target)
        {
            target = subject.gameObject;
        }
    }

    public virtual void ResetEnemy()
    {
        target = null;
        _isAttacking = false;
        _navMeshAgent.enabled = true;
        _animator.enabled = true;
    }
}
