using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadModel : MonoBehaviour
{
    Vector3[] deadModelPoints;

    void OnEnable()
    {
        deadModelPoints = new Vector3[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            deadModelPoints[transform.childCount - 1] = gameObject.transform.GetChild(i).GetComponent<Transform>().position;
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<Transform>().position = deadModelPoints[transform.childCount - 1];
        }
    }
}
