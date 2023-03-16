using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TreasureHealth : EnemyHealth
{
    public override void Die()
    {
        base.Die();

        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    public override void ResetEnemy()
    {
        base.ResetEnemy();

        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
