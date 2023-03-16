using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSingleTarget : Missile
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            Explosion();
            if (other.gameObject.GetComponent<Health>())
            {
                float random = Random.Range(0f, 100f);
                if (criticalRate >= random)
                {
                    damage *= 2.5f;
                    GameObject particleClone = Instantiate(criticalParticle, transform.position, criticalParticle.transform.rotation);
                    Destroy(particleClone, 5f);
                }

                other.gameObject.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
}
