using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : IWeapon
{
    private readonly WeaponConfig _config;
    public Weapon(WeaponConfig weaponConfig)
    {
        _config = weaponConfig;
    }

    public float Damage
    {
        get { return _config.Damage; }
    }
    public float CriticalRate
    {
        get { return _config.CriticalRate; }
    }
    public float ProjectileSpeed
    {
        get { return _config.ProjectileSpeed; }
    }
    public float Range
    {
        get { return _config.Range; }
    }
    public float Rate
    {
        get { return _config.Rate; }
    }
}
