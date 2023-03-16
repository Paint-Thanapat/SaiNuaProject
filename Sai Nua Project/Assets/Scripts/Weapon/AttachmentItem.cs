using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentItem : MonoBehaviour
{
    private Animator _animator;
    public WeaponAttachment weaponAttachment;
    public GameObject particleOnUnequip;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat("offset", Random.Range(0f, 1f));
    }
}
