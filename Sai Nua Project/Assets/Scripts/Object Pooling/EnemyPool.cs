using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    public IObjectPool<EnemyPool> enemyPool;

    private Enemy _enemy;
    private EnemyHealth _enemyHealth;

    public bool isReturning;
    void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (_enemyHealth.isDead)
        {
            if (!isReturning)
            {
                isReturning = true;

                Invoke(nameof(ReturnToPool), 3f);
            }
        }
    }

    void OnDisable()
    {
        if (_enemy)
            _enemy.ResetEnemy();

        if (_enemyHealth)
            _enemyHealth.ResetEnemy();

        isReturning = false;
    }

    void ReturnToPool()
    {
        enemyPool.Release(this);
    }
}
