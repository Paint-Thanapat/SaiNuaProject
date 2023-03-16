using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01A : Enemy
{
    public float attackRadius;
    public GameObject particleOnAttack;

    void Update()
    {
        if (!_health.isDead)
        {
            MoveToTarget();
        }
        else if (_navMeshAgent.enabled)
        {
            _navMeshAgent.enabled = false;
        }
    }

    public void Attack()
    {
        GameManager.Instance.DamageArea(transform.position, attackDamage, attackRadius, 6);

        GameObject particleClone = Instantiate(particleOnAttack, transform.position, particleOnAttack.transform.rotation);
        Destroy(particleClone, 5f);
    }
}
