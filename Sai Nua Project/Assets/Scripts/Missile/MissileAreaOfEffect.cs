using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAreaOfEffect : Missile
{
    public float radius;

    public override void Explosion()
    {
        base.Explosion();

        // - health
        Collider[] collidersToHealth = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in collidersToHealth)
        {
            Health health = nearbyObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            Explosion();
        }
    }

}
