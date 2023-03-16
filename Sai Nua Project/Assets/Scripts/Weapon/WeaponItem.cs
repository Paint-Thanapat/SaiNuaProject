using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public WeaponConfig weaponConfig;

    public bool isEquip;

    private Collider _collider;
    private Rigidbody _rigibody;
    private RotateScript _rotateScript;
    public GameObject particleOnUnequip;
    void Awake()
    {
        _collider = GetComponent<Collider>();
        _rigibody = GetComponent<Rigidbody>();
        _rotateScript = GetComponent<RotateScript>();

        SetEquip(false);
    }

    public void SetEquip(bool equip)
    {
        isEquip = equip;

        _collider.enabled = !equip;
        _rigibody.isKinematic = equip;
        _rotateScript.enabled = !equip;

        particleOnUnequip.SetActive(!equip);
    }
}
