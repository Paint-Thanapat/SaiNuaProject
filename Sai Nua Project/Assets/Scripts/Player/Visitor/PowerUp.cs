using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUp")]
public class PowerUp : ScriptableObject, IVisitor
{
    public string powerUpName;
    public GameObject powerUpPrefab;
    public string powerUpDescription;

    public float powerUpDuration = 5f;

    [Range(0f, 100f)]
    public float increaseHealth;

    [Range(1f, 5f)]
    public float damageMultiplier = 1f;

    [Range(0f, 50f)]
    public float increaseWeaponRange;

    [Range(0, 50f)]
    public float increaseWeaponRate;


    public void Visit(Health health)
    {
        float newMaxHealth = health.maxHealth + increaseHealth;
        float newCurrentHealth = health.currentHealth + increaseHealth;

        health.maxHealth = newMaxHealth;
        health.currentHealth = newCurrentHealth;

        GameManager.Instance.UIGameplay.SetPlayerHealth(health);
    }
    public void UnVisit(Health health)
    {
        float newMaxHealth = health.maxHealth - increaseHealth;

        health.maxHealth = newMaxHealth;

        if (health.currentHealth > health.maxHealth)
        {
            health.currentHealth = health.maxHealth;
        }

        GameManager.Instance.UIGameplay.SetPlayerHealth(health);
    }
    public void Visit(PlayerWeaponHolder playerWeaponHolder)
    {
        playerWeaponHolder.bonusDamageMultiplier = damageMultiplier;

        playerWeaponHolder.bonusAttackRange += increaseWeaponRange;
        playerWeaponHolder.bonusFireRate += increaseWeaponRate;

        GameManager.Instance.UIGameplay.SetBonusInfoText(playerWeaponHolder);
    }
    public void UnVisit(PlayerWeaponHolder playerWeaponHolder)
    {
        playerWeaponHolder.bonusDamageMultiplier = 1f;

        playerWeaponHolder.bonusAttackRange -= increaseWeaponRange;
        playerWeaponHolder.bonusFireRate -= increaseWeaponRate;

        GameManager.Instance.UIGameplay.SetBonusInfoText(playerWeaponHolder);
    }
}
