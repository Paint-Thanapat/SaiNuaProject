using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02B : Enemy01A
{
    public GameObject objectToSpawnOnDeath;
    public float durationObject = 30f;
    void Update()
    {
        if (!_health.isDead)
        {
            MoveToTarget();
        }
        else if (_navMeshAgent.enabled)
        {
            _navMeshAgent.enabled = false;

            GameObject objectClone = Instantiate(objectToSpawnOnDeath, transform.position, objectToSpawnOnDeath.transform.rotation);
            Destroy(objectClone, durationObject);
        }

    }
}
