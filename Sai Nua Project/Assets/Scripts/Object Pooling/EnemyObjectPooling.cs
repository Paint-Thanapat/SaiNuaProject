using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyObjectPooling : MonoBehaviour
{
    public GameObject enemyToPool;
    public int maxPoolSize = 10;
    public int stackDefaultCapacity = 10;

    private IObjectPool<EnemyPool> enemyPool;

    public IObjectPool<EnemyPool> Pool
    {
        get
        {
            if (enemyPool == null)
            {
                enemyPool = new ObjectPool<EnemyPool>(CreatedPoolItem,
                                                  OnTakeFormPool,
                                                  OnReturnToPool,
                                                  OnDestroyPool,
                                                  true,
                                                  stackDefaultCapacity,
                                                  maxPoolSize);
            }

            return enemyPool;
        }
        set
        {

        }
    }



    private EnemyPool CreatedPoolItem()
    {
        GameObject go = Instantiate(enemyToPool);

        EnemyPool obj = go.GetComponent<EnemyPool>();

        obj.enemyPool = Pool;

        return obj;
    }

    void OnTakeFormPool(EnemyPool enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    void OnReturnToPool(EnemyPool enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyPool(EnemyPool enemy)
    {
        Destroy(enemy.gameObject);
    }

    public void Spawn(Transform pointToSpawn)
    {
        var enemy = Pool.Get();

        enemy.transform.position = pointToSpawn.position;

        GameManager.Instance.waveManager.AddEnemyThisWave(enemy.GetComponent<EnemyHealth>());
    }
}
