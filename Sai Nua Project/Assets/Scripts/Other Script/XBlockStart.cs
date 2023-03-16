using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XBlockStart : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.xBlockHolder = this.gameObject;
    }

}
