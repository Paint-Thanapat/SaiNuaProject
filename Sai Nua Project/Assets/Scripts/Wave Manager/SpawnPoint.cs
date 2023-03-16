using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject spawnParticle;
    public Transform[] spawnPoints;

    void Awake()
    {
        spawnPoints = gameObject.GetComponentsInChildren<Transform>();
    }

    public Transform RandomSpawnPoint()
    {
        int randomPoint = Random.Range(1, spawnPoints.Length);

        GameObject particleClone = Instantiate(spawnParticle, spawnPoints[randomPoint].position, spawnParticle.transform.rotation);
        Destroy(particleClone, 5f);

        return spawnPoints[randomPoint];
    }
}
