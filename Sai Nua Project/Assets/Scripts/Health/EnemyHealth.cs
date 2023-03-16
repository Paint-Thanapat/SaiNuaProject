using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyHealth : Health
{
    public virtual void ResetEnemy()
    {
        currentHealth = maxHealth;

        bodyModel.SetActive(true);
        deadModel.SetActive(false);

        isDead = false;

        col.enabled = true;
    }


    public override void Die()
    {
        base.Die();

        Vector3 direction = GameManager.Instance.playerCharacter.transform.position - transform.position;

        GameManager.Instance.ExplosionForce(transform.position + (direction.normalized * 4), 50, 4);

        GameManager.Instance.mainCamera.StartCameraShake();

        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().enabled = false;
        }

        UISummary.Instance.AddEnemyKill();
    }
}
