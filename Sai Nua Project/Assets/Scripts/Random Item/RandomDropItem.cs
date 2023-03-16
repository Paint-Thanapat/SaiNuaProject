using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDropItem : MonoBehaviour
{
    public GameObject[] itemToRandomDrop;

    public void RandomDrop(Vector3 pointToDrop)
    {
        if (itemToRandomDrop.Length == 0)
            return;

        int random = Random.Range(0, itemToRandomDrop.Length);

        GameObject itemClone = Instantiate(itemToRandomDrop[random], pointToDrop, itemToRandomDrop[random].transform.rotation);
    }
}
