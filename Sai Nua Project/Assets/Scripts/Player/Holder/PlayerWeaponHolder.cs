using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHolder : MonoBehaviour, IPlayerElement
{
    public WeaponItem currentWeaponItem;

    private WeaponConfig _weaponConfig;
    public List<WeaponAttachment> weaponAttachments;
    public Transform castPoint;
    private bool _isFiring;
    private IWeapon _weapon;
    private bool _isDecorated;

    [HideInInspector] public float bonusDamageMultiplier = 1f;
    [HideInInspector] public float bonusAttackRange = 0f;
    [HideInInspector] public float bonusFireRate = 0f;

    void Start()
    {
        currentWeaponItem = GetComponentInChildren<WeaponItem>();
        currentWeaponItem.SetEquip(true);
        _weapon = new Weapon(currentWeaponItem.weaponConfig);

        GameManager.Instance.UIGameplay.SetInfoText(_weapon);
        GameManager.Instance.UIGameplay.SetBonusInfoText(this);
    }

    public void ToggleFire()
    {
        _isFiring = !_isFiring;

        if (_isFiring)
        {
            StartCoroutine(FireWeapon());
        }
    }

    IEnumerator FireWeapon()
    {
        while (_isFiring)
        {
            float calculateRate = _weapon.Rate + bonusFireRate;
            float firingRate = 1.0f / calculateRate;

            yield return new WaitForSeconds(firingRate);
            //Debug.Log("Fire");
            GameObject missileClone = Instantiate(currentWeaponItem.weaponConfig.weaponMissile, castPoint.position, castPoint.rotation);

            Missile missile = missileClone.GetComponent<Missile>();

            missile.damage = _weapon.Damage * bonusDamageMultiplier;
            missile.criticalRate = _weapon.CriticalRate;
            missile.range = _weapon.Range + bonusAttackRange;

            missile.GetComponent<Rigidbody>().AddForce(castPoint.forward * _weapon.ProjectileSpeed, ForceMode.VelocityChange);
        }
    }

    public void Reset()
    {
        _weapon = new Weapon(currentWeaponItem.weaponConfig);
        GameManager.Instance.UIGameplay.SetInfoText(_weapon);
        _isDecorated = false;
    }

    public void Decorate()
    {
        for (int i = 0; i < weaponAttachments.Count; i++)
        {
            if (weaponAttachments[i])
            {
                _weapon = new WeaponDecorator(_weapon, weaponAttachments[i]);
            }
        }
        GameManager.Instance.UIGameplay.SetInfoText(_weapon);
        _isDecorated = !_isDecorated;
    }

    public void AddAttachment(WeaponAttachment attachment)
    {
        weaponAttachments.Add(attachment);
    }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void Deline(IVisitor visitor)
    {
        visitor.UnVisit(this);
    }
}
