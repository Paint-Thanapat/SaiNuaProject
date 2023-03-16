using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Interface")]
    public int currentWave;
    public int enemyLeftInWave;

    [Header("Wave Setting")]
    public float timeToClearWave;
    public float addTimeToClearEachWave;
    private float _countingTimeToClear;
    public float spawnRate;
    public int firstEnemyAmount;
    public int addEnemyAmountEachWave;
    public EnemySpawn[] enemySpawns;

    public EnemyObjectPooling treasureMinionPool;

    public SpawnPoint enemySpawnPoint;
    public SpawnPoint treasureMinionSpawnPoint;

    [Header("Wave Debug")]
    public List<EnemyHealth> enemyHealths;
    public int enemyInCurrentWave;
    public bool startCountingTime;

    void Start()
    {
        GameManager.Instance.waveManager = this;


    }

    public IEnumerator StartSpawn(float time)
    {
        yield return new WaitForSeconds(time);

        treasureMinionPool.Spawn(treasureMinionSpawnPoint.RandomSpawnPoint());

        StartCoroutine(StartNewWave());

        UISummary.Instance.StartGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            StartCoroutine(StartNewWave());
        }

        if (startCountingTime)
        {
            _countingTimeToClear -= Time.deltaTime;

            UINotify.Instance.SetShowTimeRemaining(_countingTimeToClear);

            if (_countingTimeToClear <= 0)
            {
                treasureMinionPool.Spawn(treasureMinionSpawnPoint.RandomSpawnPoint());
                StartCoroutine(StartNewWave());
            }

            for (int i = 0; i < enemyHealths.Count; i++)
            {
                if (enemyHealths[i].isDead || !enemyHealths[i].gameObject.activeInHierarchy)
                {
                    enemyHealths[i].maxHealth *= 1.1f;
                    enemyHealths.Remove(enemyHealths[i]);
                }
            }

            if (enemyHealths.Count <= 0)
            {
                StartCoroutine(ClearRoomBeforeTimeUp());
            }
        }

    }

    IEnumerator ClearRoomBeforeTimeUp()
    {
        startCountingTime = false;

        treasureMinionPool.Spawn(treasureMinionSpawnPoint.RandomSpawnPoint());
        treasureMinionPool.Spawn(treasureMinionSpawnPoint.RandomSpawnPoint());

        UISummary.Instance.AddWaveFullClear();

        UINotify.Instance.ShowClearWave();

        yield return new WaitForSeconds(6f);

        StartCoroutine(StartNewWave());
    }

    public void AddEnemyThisWave(EnemyHealth enemyHealth)
    {
        enemyHealths.Add(enemyHealth);
    }

    IEnumerator StartNewWave()
    {
        if (GameManager.Instance.playerCharacter.GetComponent<Health>().isDead)
            StopAllCoroutines();

        //Set Current Wave and Counting

        if (currentWave == 0)
        {
            enemyInCurrentWave = firstEnemyAmount;

            _countingTimeToClear = timeToClearWave;
        }
        else
        {
            enemyInCurrentWave += addEnemyAmountEachWave;

            _countingTimeToClear = timeToClearWave + (addTimeToClearEachWave * currentWave);
        }

        currentWave++;

        UINotify.Instance.ShowWaveCount(currentWave);

        startCountingTime = false;

        //Set Enemy Can Pool

        List<EnemyObjectPooling> enemyCanPool = new List<EnemyObjectPooling>();

        for (int i = 0; i < enemySpawns.Length; i++)
        {
            if (currentWave >= enemySpawns[i].startSpawnOnWave)
            {
                enemyCanPool.Add(enemySpawns[i].enemyPool);
            }
        }

        //Set Counting Amount Enemy To Spawn

        int countingSpawn = enemyInCurrentWave;

        //Reset Enemy Counting
        enemyHealths = new List<EnemyHealth>();

        UISummary.Instance.AddSurviveWave(currentWave);

        //Start Spawning

        while (countingSpawn > 0)
        {
            if (GameManager.Instance.playerCharacter.GetComponent<Health>().isDead)
                StopAllCoroutines();

            enemyCanPool[Random.Range(0, enemyCanPool.Count)].Spawn(enemySpawnPoint.RandomSpawnPoint());

            countingSpawn--;

            yield return new WaitForSeconds(spawnRate);
        }

        startCountingTime = true;
    }
}




[System.Serializable]
public class EnemySpawn
{
    public EnemyObjectPooling enemyPool;
    public int startSpawnOnWave;
}