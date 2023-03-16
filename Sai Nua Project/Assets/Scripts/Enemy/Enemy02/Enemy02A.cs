using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02A : Enemy
{
    public float attackRadius;
    public bool isExplosion;
    public GameObject particleOnExplosion;

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

    public void Explosion()
    {
        if (!isExplosion)
        {
            isExplosion = true;

            GameManager.Instance.DamageArea(transform.position, attackDamage, attackRadius);

            GameObject objectClone = Instantiate(particleOnExplosion, transform.position, particleOnExplosion.transform.rotation);
            Destroy(objectClone, 5f);

            _health.Die();
        }
    }

    public override void ResetEnemy()
    {
        base.ResetEnemy();
        isExplosion = false;
    }
}
